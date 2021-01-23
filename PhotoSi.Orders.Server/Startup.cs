using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhotoSi.Sales.Demo.Setup;
using PhotoSi.Sales.Orders.Setup;
using PhotoSi.Sales.Sales.Data.Context;
using PhotoSi.Sales.Sales.Setup;
using PhotoSi.Sales.Services.ApiDocumentation;

namespace PhotoSi.Sales
{
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
				.AddPhotoSiSales(_configuration.GetConnectionString("Sales"))
				.AddPhotoSiOrders()
				.AddPhotoSiDemo();
			
			services
				.AddControllers();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SalesDbContext salesDbContext)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			salesDbContext.Database.EnsureCreated();

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
}
