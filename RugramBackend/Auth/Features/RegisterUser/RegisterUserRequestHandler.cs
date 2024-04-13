using Auth.Data;
using Auth.Data.Models;
using Infrastructure.MediatR.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using static Auth.Features.UserAuthHelper;
using static ProfileMicroservice;


namespace Auth.Features.RegisterUser;

public class RegisterUserRequestHandler(
		AppDbContext dbContext,
		IConfiguration configuration,
		IDistributedCache cache,
		ProfileMicroserviceClient profileClient)
	: IGrpcRequestHandler<RegisterUserRequest, RegisterUserResponse>
{
	public async Task<GrpcResult<RegisterUserResponse>> Handle(
		RegisterUserRequest request,
		CancellationToken cancellationToken)
	{
		var userWithThisEmailExist = await dbContext.Users.AsNoTracking()
			.AnyAsync(user => user.Email == request.Email, cancellationToken: cancellationToken);

		if (userWithThisEmailExist) return StatusCodes.Status409Conflict;

		var hashedToken = request.MailConfirmationToken.HashSha256();
		var mailConfirmationToken = await dbContext.MailConfirmationTokens.AsNoTracking()
			.FirstOrDefaultAsync(token => token.Email == request.Email &&
			                              token.Value == hashedToken, cancellationToken);

		if (mailConfirmationToken == null) return StatusCodes.Status404NotFound;
		
		if (mailConfirmationToken.ValidTo < DateTime.UtcNow) return StatusCodes.Status403Forbidden;
		
		var user = new User
		{
			Email = request.Email,
			Password = request.Password.HashSha256(),
			RefreshTokens = new List<RefreshToken>(),
			Role = Role.User,
		};

		var result = CreateRefreshToken(configuration, user.Id);
		var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
		
		user.RefreshTokens.Add(result.RefreshToken);

		dbContext.Users.Add(user);
		dbContext.RefreshTokens.Add(result.RefreshToken);
		await dbContext.SaveChangesAsync(cancellationToken);

		try
		{
			var createProfileTask = profileClient.CreateProfileAsync(
				new CreateProfileGrpcRequest()
				{
					ProfileId = user.Id.ToString(),
					ProfileName = request.ProfileName
				});

			await PutInCacheRefreshTokenAsync(
				configuration,
				cache,
				result.RefreshToken.Value,
				result.RefreshToken.ValidTo,
				user.Id,
				cancellationToken);

			var createProfileResponse = await createProfileTask;

			if (createProfileResponse.HttpStatusCode != 200)
			{
				await transaction.RollbackAsync(cancellationToken);
				return createProfileResponse.HttpStatusCode;
			}

			await transaction.CommitAsync(cancellationToken);
		}
		catch (Exception)
		{
			await transaction.RollbackAsync(cancellationToken);
			throw;
		}
		
		
		var jwtToken = GenerateJwtToken(configuration, user.Id, user.Role);

		return new RegisterUserResponse(
			result.UnhashedTokenValue,
			jwtToken);
	}
}