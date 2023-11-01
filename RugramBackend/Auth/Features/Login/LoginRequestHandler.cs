using Infrastructure.MediatR.Contracts;

namespace Auth.Features.Login;

public class LoginRequestHandler : IGrpcRequestHandler<LoginRequest, LoginResponse>
{
    public Task<GrpcResult<LoginResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}