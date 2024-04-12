using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Auth.Data;
using Auth.Data.Models;
using Infrastructure.MediatR.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using static Auth.Features.UserAuthHelper;

namespace Auth.Features.UpdateJwtToken;

public class UpdateJwtTokenRequestHandler(
		AppDbContext dbContext,
		IDistributedCache cache,
		IConfiguration configuration)
	: IGrpcRequestHandler<UpdateJwtTokenRequest, UpdateJwtTokenResponse>
{
	public async Task<GrpcResult<UpdateJwtTokenResponse>> Handle(
		UpdateJwtTokenRequest request,
		CancellationToken cancellationToken)
	{
		var handler = new JwtSecurityTokenHandler();

		if (!handler.CanReadToken(request.OldJwtToken)) return 400;

		var claims = handler.ReadJwtToken(request.OldJwtToken).Claims.ToList();
		var roleClaimContains = claims.Any(claim => claim.Type == nameof(ClaimTypes.Role));
		var idClaimContains = claims.Any(claim => claim.Type == nameof(ClaimTypes.NameIdentifier));

		if (!roleClaimContains || !idClaimContains) return 400;

		var roleClaim = claims.First(claim => claim.Type == nameof(ClaimTypes.Role));
		var userIdClaim = claims.First(claim => claim.Type == nameof(ClaimTypes.NameIdentifier));

		var role = Enum.Parse<Role>(roleClaim!.Value);
		if (!Guid.TryParse(userIdClaim.Value, out var userId)) return 400;

		var hashedRefreshToken = request.RefreshToken.HashSha256();
		var tokenFromCache = await cache.GetStringAsync(userId.ToString(), cancellationToken);

		if (tokenFromCache == null || hashedRefreshToken != tokenFromCache)
		{
			var tokenFromDb = await dbContext.RefreshTokens.AsNoTracking()
				.Where(token => token.UserId == userId &&
				                token.Value == hashedRefreshToken &&
				                token.ValidTo > DateTime.UtcNow)
				.Select(token => new { token.Value, token.ValidTo })
				.FirstOrDefaultAsync(cancellationToken);

			if (tokenFromDb == null) return 404;
			if (tokenFromDb.ValidTo < DateTime.UtcNow) return 403;

			await PutInCacheRefreshToken(
				configuration,
				cache,
				tokenFromDb.Value,
				tokenFromDb.ValidTo,
				userId,
				cancellationToken);
		}


		var jwtToken = GenerateJwtToken(configuration, userId, role);

		return new UpdateJwtTokenResponse(jwtToken);
	}
}