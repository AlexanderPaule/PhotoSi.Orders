using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhotoSi.Orders.Server.Demo.Setup;
using PhotoSi.Orders.Server.Orders.Data.Context;
using PhotoSi.Orders.Server.Orders.Setup;
using PhotoSi.Orders.Server.Services.ApiDocumentation;

namespace PhotoSi.Orders.Server
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
				.AddPhotoSiOrders(_configuration.GetConnectionString("Sales"))
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
