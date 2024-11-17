using PhotoSi.Documentation;
using PhotoSi.Products.Data.Context;
using PhotoSi.Products.Utils.Demo.Setup;
using PhotoSi.Products.Setup;

namespace PhotoSi.Sales;

internal class Startup
{
	private readonly IConfiguration _configuration;

	public Startup(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public void ConfigureServices(IServiceCollection services)
	{
		services
			.AddApiDocumentation()
			.AddPhotoSiProductsAPI()
			.AddPhotoSiProductsCore(_configuration.GetConnectionString("Products")!)
			.AddPhotoSiDemo();
		
		services
			.AddControllers();
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ProductsDbContext productDbContext)
	{
		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}

		productDbContext.Database.EnsureCreated();

		app.UseHttpsRedirection();
		app.UseRouting();
		app.UseAuthorization();
		app.UseApiDocumentation();

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllers();
		});
	}
}