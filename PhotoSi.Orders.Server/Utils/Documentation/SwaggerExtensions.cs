using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;

namespace PhotoSi.Orders.Utils.Documentation;

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
			Title = "PhotoSi.Orders",
			Description = "Server API Documentation",
			Contact = new OpenApiContact
			{
				Name = "PhotoSi.Orders",
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