using PhotoSi.Addresses.Data.Context;
using PhotoSi.Addresses.Setup;
using PhotoSi.API.Demo.Setup;
using PhotoSi.API.Setup;
using PhotoSi.API.Utils.Documentation;
using PhotoSi.Orders.Data.Context;
using PhotoSi.Orders.Setup;
using PhotoSi.Products.Data.Context;
using PhotoSi.Products.Setup;
using PhotoSi.Users.Data.Context;
using PhotoSi.Users.Setup;

namespace PhotoSi.API;

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
			.AddPhotoSiOrders(_configuration.GetConnectionString("Orders"))
			.AddPhotoSiOrdersAPI()
			.AddPhotoSiAddresses(_configuration.GetConnectionString("Addresses")!)
			.AddPhotoSiAddressesAPI()
			.AddPhotoSiProducts(_configuration.GetConnectionString("Products")!)
			.AddPhotoSiProductsAPI()
			.AddPhotoSiUsers(_configuration.GetConnectionString("Users")!)
			.AddPhotoSiUsersAPI()
			.AddPhotoSiDemo();

		services
			.AddControllers();
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
		OrdersDbContext ordersDbContext, AddressesDbContext addressesDbContext, ProductsDbContext productsDbContext, UsersDbContext usersDbContext)
	{
		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}

		ordersDbContext.Database.EnsureCreated();
		addressesDbContext.Database.EnsureCreated();
		productsDbContext.Database.EnsureCreated();
		usersDbContext.Database.EnsureCreated();

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
