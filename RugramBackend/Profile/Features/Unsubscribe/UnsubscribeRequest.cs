using Infrastructure.MediatR.Contracts;

namespace Profile.Features.Unsubscribe;

public record UnsubscribeRequest
	(Guid SubscriberId, string NameOfProfileUnsubscribedTo) : IGrpcRequest;