﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using PhotoSi.Orders.Server.Orders.Controllers.Models;
using PhotoSi.Orders.Server.Orders.Controllers.Validation;
using PhotoSi.Orders.Server.Sales.Core;
using PhotoSi.Orders.Server.Sales.Core.Models;

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
					new OrderedProductModel { Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D"), CustomOptions = new List<OptionModel>() },
					new OrderedProductModel { Id = new Guid("4B5174E4-37B7-44EE-A8A2-EE920C6FAB9E"), CustomOptions = new List<OptionModel>() }
				}
			};
			
			var products = new List<Product>
			{
				new Product { Id = orderModel.Products.First().Id, Category = new Category { Id = orderModel.Category.Id }, Options = new List<Option>() },
				new Product { Id = orderModel.Products.Last().Id, Category = new Category { Id = orderModel.Category.Id }, Options = new List<Option>() }
			};

			var checkGateway = new Mock<ICheckGateway>(MockBehavior.Strict);
			checkGateway
				.Setup(x => x.ExistsOrderAsync(orderModel.Id))
				.ReturnsAsync(false);
			checkGateway
				.Setup(x => x.GetProductsAsync(It.IsAny<IEnumerable<Guid>>()))
				.ReturnsAsync(RequestResult<Product, Guid>.New(products, products.Select(x => x.Id)));
			checkGateway
				.Setup(x => x.ExistsCategoryAsync(orderModel.Category.Id))
				.ReturnsAsync(true);
			
			var validator = new Validator(checkGateway.Object);

			
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
					new OrderedProductModel { Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D"), CustomOptions = new List<OptionModel>() },
					new OrderedProductModel { Id = new Guid("4B5174E4-37B7-44EE-A8A2-EE920C6FAB9E"), CustomOptions = new List<OptionModel>() }
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
					new OrderedProductModel { Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D"), CustomOptions = new List<OptionModel>() },
					new OrderedProductModel { Id = new Guid("4B5174E4-37B7-44EE-A8A2-EE920C6FAB9E"), CustomOptions = new List<OptionModel>() }
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
					new OrderedProductModel { Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D"), CustomOptions = new List<OptionModel>() },
					new OrderedProductModel { Id = new Guid(), CustomOptions = new List<OptionModel>() }
				}
			};
			var validator = new Validator(Mock.Of<ICheckGateway>(MockBehavior.Strict));

			var validationResult = await validator.ValidateAsync(orderModel);

			Assert.That(validationResult.IsValid, Is.False);
		}

		[Test]
		public async Task NotValidEmptyProductList()
		{
			var orderModel = new OrderModel
			{
				Id = new Guid("2B5174E4-37B7-44EE-A8A2-EE920C6FAB9C"),
				Category = new CategoryModel { Id = new Guid("885174E4-37B7-44EE-A8A2-EE920C6FAB9C") },
				Products = new List<OrderedProductModel>()
			};
			var validator = new Validator(Mock.Of<ICheckGateway>(MockBehavior.Strict));

			var validationResult = await validator.ValidateAsync(orderModel);

			Assert.That(validationResult.IsValid, Is.False);
		}

		[Test]
		public async Task NotValidEmptyOptionId()
		{
			var orderModel = new OrderModel
			{
				Id = new Guid("2B5174E4-37B7-44EE-A8A2-EE920C6FAB9C"),
				Category = new CategoryModel { Id = new Guid("885174E4-37B7-44EE-A8A2-EE920C6FAB9C") },
				Products = new[]
				{
					new OrderedProductModel
					{
						Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D"),
						CustomOptions = new [] { new OptionModel { Id = new Guid() } }
					}
				}
			};
			var validator = new Validator(Mock.Of<ICheckGateway>(MockBehavior.Strict));

			var validationResult = await validator.ValidateAsync(orderModel);

			Assert.That(validationResult.IsValid, Is.False);
		}

		[Test]
		public async Task NotValidOrderAlreadyExists()
		{
			var orderModel = new OrderModel
			{
				Id = new Guid("2B5174E4-37B7-44EE-A8A2-EE920C6FAB9C"),
				Category = new CategoryModel { Id = new Guid("885174E4-37B7-44EE-A8A2-EE920C6FAB9C") },
				Products = new[]
				{
					new OrderedProductModel { Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D"), CustomOptions = new List<OptionModel>() },
					new OrderedProductModel { Id = new Guid("4B5174E4-37B7-44EE-A8A2-EE920C6FAB9E"), CustomOptions = new List<OptionModel>() }
				}
			};
			
			var checkGateway = new Mock<ICheckGateway>(MockBehavior.Strict);
			checkGateway
				.Setup(x => x.ExistsOrderAsync(orderModel.Id))
				.ReturnsAsync(true);
			
			var validator = new Validator(checkGateway.Object);

			
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
					new OrderedProductModel { Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D"), CustomOptions = new List<OptionModel>() },
					new OrderedProductModel { Id = new Guid("4B5174E4-37B7-44EE-A8A2-EE920C6FAB9E"), CustomOptions = new List<OptionModel>() }
				}
			};
			
			var products = new List<Product>
			{
				new Product { Id = orderModel.Products.First().Id, Category = new Category { Id = orderModel.Category.Id } },
				new Product { Id = orderModel.Products.Last().Id, Category = new Category { Id = orderModel.Category.Id } }
			};

			var checkGateway = new Mock<ICheckGateway>(MockBehavior.Strict);
			checkGateway
				.Setup(x => x.ExistsOrderAsync(orderModel.Id))
				.ReturnsAsync(false);
			checkGateway
				.Setup(x => x.GetProductsAsync(It.IsAny<IEnumerable<Guid>>()))
				.ReturnsAsync(RequestResult<Product, Guid>.New(products, products.Select(x => x.Id)));
			checkGateway
				.Setup(x => x.ExistsCategoryAsync(orderModel.Category.Id))
				.ReturnsAsync(false);
			
			var validator = new Validator(checkGateway.Object);

			
			var validationResult = await validator.ValidateAsync(orderModel);

			
			Assert.That(validationResult.IsValid, Is.False);
		}

		[Test]
		public async Task NotValidCategoryMisalignment()
		{
			var orderModel = new OrderModel
			{
				Id = new Guid("2B5174E4-37B7-44EE-A8A2-EE920C6FAB9C"),
				Category = new CategoryModel { Id = new Guid("885174E4-37B7-44EE-A8A2-EE920C6FAB9C") },
				Products = new[]
				{
					new OrderedProductModel { Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D"), CustomOptions = new List<OptionModel>() },
					new OrderedProductModel { Id = new Guid("4B5174E4-37B7-44EE-A8A2-EE920C6FAB9E"), CustomOptions = new List<OptionModel>() }
				}
			};
			
			var products = new List<Product>
			{
				new Product { Id = orderModel.Products.First().Id, Category = new Category { Id = orderModel.Category.Id } },
				new Product { Id = orderModel.Products.Last().Id, Category = new Category { Id = new Guid("995174E4-37B7-44EE-A8A2-EE920C6FAB9C") } }
			};

			var checkGateway = new Mock<ICheckGateway>(MockBehavior.Strict);
			checkGateway
				.Setup(x => x.ExistsOrderAsync(orderModel.Id))
				.ReturnsAsync(false);
			checkGateway
				.Setup(x => x.GetProductsAsync(It.IsAny<IEnumerable<Guid>>()))
				.ReturnsAsync(RequestResult<Product, Guid>.New(products, products.Select(x => x.Id)));
			checkGateway
				.Setup(x => x.ExistsCategoryAsync(orderModel.Category.Id))
				.ReturnsAsync(true);
			var validator = new Validator(checkGateway.Object);

			
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
					new OrderedProductModel { Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D"), CustomOptions = new List<OptionModel>() },
					new OrderedProductModel { Id = new Guid("4B5174E4-37B7-44EE-A8A2-EE920C6FAB9E"), CustomOptions = new List<OptionModel>() }
				}
			};
			
			var products = new List<Product>
			{
				new Product { Id = orderModel.Products.First().Id, Category = new Category { Id = orderModel.Category.Id }, Options = new List<Option>() }
			};

			var checkGateway = new Mock<ICheckGateway>(MockBehavior.Strict);
			checkGateway
				.Setup(x => x.ExistsOrderAsync(orderModel.Id))
				.ReturnsAsync(false);
			checkGateway
				.Setup(x => x.GetProductsAsync(It.IsAny<IEnumerable<Guid>>()))
				.ReturnsAsync(RequestResult<Product, Guid>.New(products, products.Select(x => x.Id)));
			checkGateway
				.Setup(x => x.ExistsCategoryAsync(orderModel.Category.Id))
				.ReturnsAsync(true);
			
			var validator = new Validator(checkGateway.Object);

			
			var validationResult = await validator.ValidateAsync(orderModel);

			
			Assert.That(validationResult.IsValid, Is.False);
		}

		[Test]
		public async Task NotValidMissingOptions()
		{
			var orderModel = new OrderModel
			{
				Id = new Guid("2B5174E4-37B7-44EE-A8A2-EE920C6FAB9C"),
				Category = new CategoryModel { Id = new Guid("885174E4-37B7-44EE-A8A2-EE920C6FAB9C") },
				Products = new[]
				{
					new OrderedProductModel
					{
						Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D"), 
						CustomOptions = new []
						{
							new OptionModel { Id = new Guid("777174E4-37B7-44EE-A8A2-EE920C6FAB9D") },
							new OptionModel { Id = new Guid("888174E4-37B7-44EE-A8A2-EE920C6FAB9D") }
						}
					}
				}
			};

			var products = new List<Product>
			{
				new Product { Id = orderModel.Products.Single().Id, Category = new Category { Id = orderModel.Category.Id }, Options = new List<Option>() }
			};
			
			var checkGateway = new Mock<ICheckGateway>(MockBehavior.Strict);
			checkGateway
				.Setup(x => x.ExistsOrderAsync(orderModel.Id))
				.ReturnsAsync(false);
			checkGateway
				.Setup(x => x.GetProductsAsync(It.IsAny<IEnumerable<Guid>>()))
				.ReturnsAsync(RequestResult<Product, Guid>.New(products, products.Select(x => x.Id)));
			checkGateway
				.Setup(x => x.ExistsCategoryAsync(orderModel.Category.Id))
				.ReturnsAsync(true);
			
			var validator = new Validator(checkGateway.Object);

			
			var validationResult = await validator.ValidateAsync(orderModel);

			
			Assert.That(validationResult.IsValid, Is.False);
		}
	}
}