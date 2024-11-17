using PhotoSi.Users.Utils.Documentation;
using PhotoSi.Users.Utils.Demo.Setup;
using PhotoSi.Users.Setup;
using PhotoSi.Users.Data.Context;

namespace PhotoSi.Users;

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
			.AddPhotoSiProductsCore(_configuration.GetConnectionString("Users")!)
			.AddPhotoSiDemo();
		
		services
			.AddControllers();
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UsersDbContext productDbContext)
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
