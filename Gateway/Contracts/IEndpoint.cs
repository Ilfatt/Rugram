namespace Gateway.Contracts;

public interface IEndpoint
{
    public void AddRoute(WebApplication app);
}