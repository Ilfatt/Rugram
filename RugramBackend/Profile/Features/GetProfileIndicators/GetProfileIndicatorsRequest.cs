using Infrastructure.MediatR.Contracts;
using Profile.Features.GetProfileName;

namespace Profile.Features.GetProfileIndicators;

public record GetProfileIndicatorsRequest(Guid ProfileId) : IGrpcRequest<GetProfileIndicatorsResponse>;