using Infrastructure.MediatR.Contracts;
using Microsoft.EntityFrameworkCore;
using Profile.Data;
using Profile.Data.Models;

namespace Profile.Features.CreateProfile;

public class CreateProfileRequestHandler(AppDbContext appDbContext)
	: IGrpcRequestHandler<CreateProfileRequest, CreateProfileResponse>
{
	public async Task<GrpcResult<CreateProfileResponse>> Handle(
		CreateProfileRequest request,
		CancellationToken cancellationToken)
	{
		var existUserWithThisProfileName = await appDbContext.UserProfiles
			.AnyAsync(x => x.ProfileName == request.ProfileName, cancellationToken);

		if (existUserWithThisProfileName) return 409;

		var profile = new UserProfile(request.ProfileId, request.ProfileName);

		appDbContext.UserProfiles.Add(profile);
		await appDbContext.SaveChangesAsync(cancellationToken);

		return new CreateProfileResponse();
	}
}