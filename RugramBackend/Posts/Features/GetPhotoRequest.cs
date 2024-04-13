using Infrastructure.MediatR.Contracts;

namespace Posts.Features;

public record GetPhotoRequest(Guid ProfileId, Guid PhotoId) : IGrpcRequest<GetPhotoResponse>;
