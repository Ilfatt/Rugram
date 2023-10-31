namespace Contracts;

public class GrpcResult<TBody>
{
    private GrpcResult(int httpStatusCode)
    {
        HttpStatusCode = httpStatusCode;
    }

    private GrpcResult(TBody body)
    {
        Body = body;
        HttpStatusCode = 200;
    }

    public int HttpStatusCode { get; private init; }

    public TBody? Body { get; private init; }

    public static implicit operator GrpcResult<TBody>(TBody body)
    {
        return new GrpcResult<TBody>(body);
    }

    public static implicit operator GrpcResult<TBody>(int httpStatusCode)
    {
        return new GrpcResult<TBody>(httpStatusCode);
    }
}