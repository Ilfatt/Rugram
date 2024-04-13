using Infrastructure.MediatR.Contracts;

namespace Profile.Features.Subscribe;

public record SubscribeRequest(Guid SubscriberId, string NameOfProfileSubscribedTo) : IGrpcRequest;