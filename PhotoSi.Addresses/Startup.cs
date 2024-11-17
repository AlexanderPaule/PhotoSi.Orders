using PhotoSi.Addresses.Data.Context;
using PhotoSi.Addresses.Setup;
using PhotoSi.Addresses.Utils.Documentation;
using PhotoSi.Addresses.Utils.Demo.Setup;

namespace PhotoSi.Addresses;

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
			.AddPhotoSiAddressesAPI()
			.AddPhotoSiAddressesCore(_configuration.GetConnectionString("Addresses")!)
			.AddPhotoSiDemo();

		services
			.AddControllers();
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AddressesDbContext productDbContext)
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
