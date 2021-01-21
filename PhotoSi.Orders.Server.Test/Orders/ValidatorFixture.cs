using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using PhotoSi.Orders.Server.Orders.Controllers.Models;
using PhotoSi.Orders.Server.Orders.Controllers.Validation;
using PhotoSi.Orders.Server.Orders.Core;
using PhotoSi.Orders.Server.Orders.Core.Models;

namespace PhotoSi.Orders.Server.Test.Orders
{
	[TestFixture]
	internal class ValidatorFixture
	{
		[Test]
		public async Task ValidOrder()
		{
			var orderModel = new OrderModel
			{
				Id = new Guid("2B5174E4-37B7-44EE-A8A2-EE920C6FAB9C"),
				Category = new CategoryModel { Id = new Guid("885174E4-37B7-44EE-A8A2-EE920C6FAB9C") },
				Products = new []
				{
					new OrderedProductModel { Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D") },
					new OrderedProductModel { Id = new Guid("4B5174E4-37B7-44EE-A8A2-EE920C6FAB9E") }
				}
			};
			var products = new List<Product>
			{
				new Product { Id = orderModel.Products.First().Id, Category = new CategoryModel { Id = orderModel.Category.Id } },
				new Product { Id = orderModel.Products.Last().Id, Category = new CategoryModel { Id = orderModel.Category.Id } }
			};
			var productsStorage = new Mock<ICheckGateway>(MockBehavior.Strict);
			productsStorage
				.Setup(x => x.GetProducts(It.IsAny<IEnumerable<Guid>>()))
				.ReturnsAsync(products);
			var validator = new Validator(productsStorage.Object);

			var validationResult = await validator.ValidateAsync(orderModel);
			
			Assert.That(validationResult.IsValid, Is.True, validationResult.GetErrorMessage);
		}

		[Test]
		public async Task NotValidEmptyId()
		{
			var orderModel = new OrderModel
			{
				Id = new Guid(),
				Category = new CategoryModel { Id = new Guid("885174E4-37B7-44EE-A8A2-EE920C6FAB9C") },
				Products = new []
				{
					new OrderedProductModel { Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D") },
					new OrderedProductModel { Id = new Guid("4B5174E4-37B7-44EE-A8A2-EE920C6FAB9E") }
				}
			};
			var validator = new Validator(Mock.Of<ICheckGateway>(MockBehavior.Strict));

			var validationResult = await validator.ValidateAsync(orderModel);
			
			Assert.That(validationResult.IsValid, Is.False);
		}

		[Test]
		public async Task NotValidEmptyCategoryId()
		{
			var orderModel = new OrderModel
			{
				Id = new Guid("2B5174E4-37B7-44EE-A8A2-EE920C6FAB9C"),
				Category = new CategoryModel { Id = new Guid() },
				Products = new[]
				{
					new OrderedProductModel { Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D") },
					new OrderedProductModel { Id = new Guid("4B5174E4-37B7-44EE-A8A2-EE920C6FAB9E") }
				}
			};
			var validator = new Validator(Mock.Of<ICheckGateway>(MockBehavior.Strict));

			var validationResult = await validator.ValidateAsync(orderModel);

			Assert.That(validationResult.IsValid, Is.False);
		}

		[Test]
		public async Task NotValidEmptyProductId()
		{
			var orderModel = new OrderModel
			{
				Id = new Guid("2B5174E4-37B7-44EE-A8A2-EE920C6FAB9C"),
				Category = new CategoryModel { Id = new Guid("885174E4-37B7-44EE-A8A2-EE920C6FAB9C") },
				Products = new[]
				{
					new OrderedProductModel { Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D") },
					new OrderedProductModel { Id = new Guid() }
				}
			};
			var validator = new Validator(Mock.Of<ICheckGateway>(MockBehavior.Strict));

			var validationResult = await validator.ValidateAsync(orderModel);

			Assert.That(validationResult.IsValid, Is.False);
		}

		[Test]
		public async Task NotValidMissingCategory()
		{
			var orderModel = new OrderModel
			{
				Id = new Guid("2B5174E4-37B7-44EE-A8A2-EE920C6FAB9C"),
				Category = new CategoryModel { Id = new Guid("885174E4-37B7-44EE-A8A2-EE920C6FAB9C") },
				Products = new[]
				{
					new OrderedProductModel { Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D") },
					new OrderedProductModel { Id = new Guid("4B5174E4-37B7-44EE-A8A2-EE920C6FAB9E") }
				}
			};
			var products = new List<Product>
			{
				new Product { Id = orderModel.Products.First().Id, Category = new Category { Id = orderModel.Category.Id } },
				new Product { Id = orderModel.Products.Last().Id, Category = new Category { Id = new Guid("995174E4-37B7-44EE-A8A2-EE920C6FAB9C") } }
			};
			var productsStorage = new Mock<IProductsStorage>(MockBehavior.Strict);
			productsStorage
				.Setup(x => x.GetProducts(It.IsAny<IEnumerable<Guid>>()))
				.ReturnsAsync(products);
			var validator = new Validator(productsStorage.Object);

			var validationResult = await validator.ValidateAsync(orderModel);

			Assert.That(validationResult.IsValid, Is.False);
		}

		[Test]
		public async Task NotValidMissingProduct()
		{
			var orderModel = new OrderModel
			{
				Id = new Guid("2B5174E4-37B7-44EE-A8A2-EE920C6FAB9C"),
				Category = new CategoryModel { Id = new Guid("885174E4-37B7-44EE-A8A2-EE920C6FAB9C") },
				Products = new[]
				{
					new OrderedProductModel { Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D") },
					new OrderedProductModel { Id = new Guid("4B5174E4-37B7-44EE-A8A2-EE920C6FAB9E") }
				}
			};
			var products = new List<Product>
			{
				new Product { Id = orderModel.Products.First().Id, Category = new CategoryModel { Id = orderModel.Category.Id } }
			};
			var productsStorage = new Mock<ICheckGateway>(MockBehavior.Strict);
			productsStorage
				.Setup(x => x.GetProducts(It.IsAny<IEnumerable<Guid>>()))
				.ReturnsAsync(products);
			var validator = new Validator(productsStorage.Object);

			var validationResult = await validator.ValidateAsync(orderModel);

			Assert.That(validationResult.IsValid, Is.False);
		}
	}
}