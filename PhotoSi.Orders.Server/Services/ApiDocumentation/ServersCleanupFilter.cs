using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PhotoSi.Orders.Server.Services.ApiDocumentation
{
	internal class ServersCleanupFilter : IDocumentFilter
	{
		public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
		{
			swaggerDoc
				.Servers
				.Clear();
		}
	}
}