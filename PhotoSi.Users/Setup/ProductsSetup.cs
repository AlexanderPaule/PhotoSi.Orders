﻿using Microsoft.EntityFrameworkCore;
using PhotoSi.Users.Controllers;
using PhotoSi.Users.Controllers.Translation;
using PhotoSi.Users.Controllers.Validation;
using PhotoSi.Users.Core;
using PhotoSi.Users.Data;
using PhotoSi.Users.Data.Context;
using PhotoSi.Users.Data.Translation;

namespace PhotoSi.Users.Setup;

internal static class ProductsSetup
{
	public static IServiceCollection AddPhotoSiProductsCore(this IServiceCollection services, string dbConnectionString)
	{
		services.AddScoped<CoreEngine>();
		services.AddScoped<IUsersGateway>(x => x.GetService<CoreEngine>()!);
		services.AddScoped<IDemoGateway>(x => x.GetService<CoreEngine>()!);

		services.AddScoped<IUsersRepository, ProductsRepository>();
		services.AddScoped<IDbContextFactory>(x => new DbContextFactory(dbConnectionString));
		services.AddScoped<IDbLayerTranslator, DbLayerTranslator>();
		services.AddDbContext<UsersDbContext>(o => o.UseSqlServer(dbConnectionString));

		return services;
	}
	public static IServiceCollection AddPhotoSiProductsAPI(this IServiceCollection services)
	{
		services.AddScoped<IValidator, Validator>();
		services.AddScoped<IApiLayerTranslator, ApiLayerTranslator>();

		return services;
	}
}