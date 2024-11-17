using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhotoSi.Orders.Data.Context;
using PhotoSi.Orders.Setup;
using PhotoSi.Orders.Utils.Documentation;

namespace PhotoSi.Orders;

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
			.AddPhotoSiOrders(_configuration.GetConnectionString("Orders"));

		services
			.AddControllers();
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env, OrdersDbContext ordersDbContext)
	{
		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}

		ordersDbContext.Database.EnsureCreated();

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
