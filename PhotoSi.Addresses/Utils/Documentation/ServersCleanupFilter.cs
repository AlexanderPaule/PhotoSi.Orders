using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PhotoSi.Addresses.Utils.Documentation;

internal class ServersCleanupFilter : IDocumentFilter
{
	public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
	{
		swaggerDoc
			.Servers
			.Clear();
	}
}