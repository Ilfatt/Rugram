using Infrastructure.MediatR.Contracts;
using Profile.Features.GetProfileName;

namespace Profile.Features.GetProfilePhoto;

public record GetProfilePhotoRequest(Guid ProfileId) : IGrpcRequest<GetProfilePhotoResponse>;