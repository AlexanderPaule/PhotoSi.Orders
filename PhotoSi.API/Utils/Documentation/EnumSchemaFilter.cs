using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PhotoSi.API.Utils.Documentation;

internal class EnumSchemaFilter : ISchemaFilter
{
	public void Apply(OpenApiSchema model, SchemaFilterContext context)
	{
		if (!context.Type.IsEnum)
			return;

		model.Enum.Clear();
		Enum.GetNames(context.Type)
			.ToList()
			.ForEach(name => model.Enum.Add(new OpenApiString($"{Convert.ToInt64(Enum.Parse(context.Type, name))}:{name}")));
	}
}