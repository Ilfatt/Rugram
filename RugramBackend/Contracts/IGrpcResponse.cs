namespace Contracts;

public interface IGrpcResponse
{
    public int HttpStatusCode { get; init; }
}