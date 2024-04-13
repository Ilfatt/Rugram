using Infrastructure.MediatR.Contracts;
using Microsoft.EntityFrameworkCore;
using Profile.Data;
using Profile.Data.Models;

namespace Profile.Features.CreateProfile;

public class CreateProfileRequestHandler(AppDbContext appDbContext)
	: IGrpcRequestHandler<CreateProfileRequest>
{
	public async Task<GrpcResult> Handle(
		CreateProfileRequest request,
		CancellationToken cancellationToken)
	{
		var existUserWithThisProfileName = await appDbContext.UserProfiles
			.AnyAsync(x => x.ProfileName == request.ProfileName, cancellationToken);

		if (existUserWithThisProfileName) return StatusCodes.Status409Conflict;

		var profile = new UserProfile(request.ProfileId, request.ProfileName);

		appDbContext.UserProfiles.Add(profile);
		await appDbContext.SaveChangesAsync(cancellationToken);

		return StatusCodes.Status204NoContent;
	}
}