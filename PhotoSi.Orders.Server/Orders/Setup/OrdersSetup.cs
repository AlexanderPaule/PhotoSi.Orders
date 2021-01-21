﻿using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using PhotoSi.Orders.Server.Orders.Controllers.Models;
using PhotoSi.Orders.Server.Orders.Controllers.Translation;
using PhotoSi.Orders.Server.Orders.Controllers.Validation;
using PhotoSi.Orders.Server.Orders.Core.Models;

namespace PhotoSi.Orders.Server.Orders.Setup
{
	public static class OrdersSetup
	{
		public static IServiceCollection AddPhotoSiOrders(this IServiceCollection services)
		{
			services.AddScoped<IValidator, Validator>();
			services.AddScoped<IApiLayerTranslator, ApiLayerTranslator>();

			return services;
		}
	}

	public class ApiLayerTranslator : IApiLayerTranslator
	{
		public Order Translate(OrderModel source)
		{
			return new Order
			{
				Id = source.Id,
				Category = new Category
				{
					Id = source.Category.Id,
					Name = source.Category.Name,
					Description = source.Category.Description
				},
				Products = source.Products.Select(Translate)
			};
		}

		public OrderModel Translate(Order source)
		{
			return new OrderModel
			{
				Id = source.Id,
				Category = new CategoryModel
				{
					Id = source.Category.Id,
					Name = source.Category.Name,
					Description = source.Category.Description
				},
				Products = source.Products.Select(Translate)
			};
		}

		private static OrderedProduct Translate(OrderedProductModel source)
		{
			return new OrderedProduct
			{
				Id = source.Id,
				Options = source.Options.Select(Translate)
			};
		}

		private static Option Translate(OptionModel source)
		{
			return new Option
			{
				Name = source.Name,
				Description = source.Description
			};
		}

		private static OrderedProductModel Translate(OrderedProduct source)
		{
			return new OrderedProductModel
			{
				Id = source.Id,
				Options = source.Options.Select(Translate)
			};
		}

		private static OptionModel Translate(Option source)
		{
			return new OptionModel
			{
				Name = source.Name,
				Description = source.Description
			};
		}
	}
}
