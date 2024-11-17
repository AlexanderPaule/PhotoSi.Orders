using Microsoft.OpenApi.Models;

namespace PhotoSi.Documentation;

internal static class SwaggerExtensions
{
	private const string BackendDoc = "BackendApiDocumentation";

	public static IServiceCollection AddApiDocumentation(this IServiceCollection services)
	{
		services.AddSwaggerGen(config =>
		{
			config.SwaggerDoc(BackendDoc, CreateApiInfo());

			config.DocumentFilter<ServersCleanupFilter>();
			config.SchemaFilter<EnumSchemaFilter>();

			config.ResolveConflictingActions(apiDescriptions => apiDescriptions.Last());
		});

		return services;
	}

	private static OpenApiInfo CreateApiInfo()
	{
		return new OpenApiInfo
		{
			Title = "PhotoSi.Products",
			Description = "Server API Documentation",
			Contact = new OpenApiContact
			{
				Name = "PhotoSi.Products",
				Url = new Uri("https://github.com/AlexanderPaule/PhotoSi.Orders")
			}
		};
	}

	public static IApplicationBuilder UseApiDocumentation(this IApplicationBuilder app)
	{
		app.UseSwagger();
		app.UseSwaggerUI(c =>
		{
			c.SwaggerEndpoint($"/swagger/{BackendDoc}/swagger.json", BackendDoc);
		});

		return app;
	}
}