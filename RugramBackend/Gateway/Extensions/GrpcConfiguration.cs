namespace Gateway.Extensions;

public static class GrpcConfiguration
{
	/// <summary>
	/// Добавление Grpc клиентов в сервис провайдер 
	/// </summary>
	/// <param name="builder">IWebHostBuilder</param>
	public static void AddGrpcClients(this WebApplicationBuilder builder)
	{
		builder.Services.AddGrpcClient<AuthMicroservice.AuthMicroserviceClient>(conf =>
		{
			conf.Address = new Uri(builder.Configuration["Microservices:AuthAddress"]
			                       ?? throw new ApplicationException(
				                       "Enviroment variable Microservices:AuthAddress not found "));
		});
		
		builder.Services.AddGrpcClient<ProfileMicroservice.ProfileMicroserviceClient>(conf =>
		{
			conf.Address = new Uri(builder.Configuration["Microservices:ProfileAddress"]
			                       ?? throw new ApplicationException(
				                       "Enviroment variable Microservices:AuthAddress not found "));
		});
	}
}