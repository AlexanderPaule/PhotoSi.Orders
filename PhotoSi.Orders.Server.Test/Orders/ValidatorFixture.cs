//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Moq;
//using NUnit.Framework;
//using PhotoSi.Orders.Controllers.Models;
//using PhotoSi.Orders.Core;
//using PhotoSi.Orders.Core.Models;
//using PhotoSi.Sales.Orders.Models;
//using PhotoSi.Sales.Orders.Validation;

//namespace PhotoSi.Sales.Test.Orders
//{
//	[TestFixture]
//	internal class ValidatorFixture
//	{
//		[Test]
//		public async Task ValidOrder()
//		{
//			var existingOptions = new []
//			{
//				new OrderedOptionModel { Id = new Guid("B885C0E8-23B2-482C-8D59-8BD96EF58AA3") },
//				new OrderedOptionModel { Id = new Guid("05FC5BD3-BC16-4EAD-8A1E-1D92C2060D8E") }
//			};
//			var orderModel = new OrderModel
//			{
//				Id = new Guid("2B5174E4-37B7-44EE-A8A2-EE920C6FAB9C"),
//				OrderCategory = new OrderCategoryModel { Id = new Guid("885174E4-37B7-44EE-A8A2-EE920C6FAB9C") },
//				Products = new []
//				{
//					new OrderedProductModel { Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D"), CustomOptions = new List<OrderedOptionModel>() },
//					new OrderedProductModel { Id = new Guid("4B5174E4-37B7-44EE-A8A2-EE920C6FAB9E"), CustomOptions = existingOptions }
//				}
//			};
			
//			var products = new List<Product>
//			{
//				new Product
//				{
//					Id = orderModel.Products.First().Id,
//					Category = new Category { Id = orderModel.OrderCategory.Id },
//					Options = new List<Option>()
//				},
//				new Product
//				{
//					Id = orderModel.Products.Last().Id,
//					Category = new Category { Id = orderModel.OrderCategory.Id },
//					Options = new [] { new Option { Id = existingOptions.First().Id }, new Option { Id = existingOptions.Last().Id } }
//				}
//			};

//			var checkGateway = new Mock<ICheckGateway>(MockBehavior.Strict);
//			checkGateway
//				.Setup(x => x.ExistsOrderAsync(orderModel.Id))
//				.ReturnsAsync(false);
//			checkGateway
//				.Setup(x => x.GetProductsAsync(It.IsAny<IEnumerable<Guid>>()))
//				.ReturnsAsync(RequestResult<Product, Guid>.New(products, products.Select(x => x.Id)));
//			checkGateway
//				.Setup(x => x.ExistsCategoryAsync(orderModel.OrderCategory.Id))
//				.ReturnsAsync(true);
			
//			var validator = new Validator(checkGateway.Object);

			
//			var validationResult = await validator.ValidateAsync(orderModel);
			
			
//			Assert.That(validationResult.IsValid, Is.True, validationResult.GetErrorMessage);
//		}

//		[Test]
//		public async Task NotValidEmptyId()
//		{
//			var orderModel = new OrderModel
//			{
//				Id = new Guid(),
//				OrderCategory = new OrderCategoryModel { Id = new Guid("885174E4-37B7-44EE-A8A2-EE920C6FAB9C") },
//				Products = new []
//				{
//					new OrderedProductModel { Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D"), CustomOptions = new List<OrderedOptionModel>() },
//					new OrderedProductModel { Id = new Guid("4B5174E4-37B7-44EE-A8A2-EE920C6FAB9E"), CustomOptions = new List<OrderedOptionModel>() }
//				}
//			};
//			var validator = new Validator(Mock.Of<ICheckGateway>(MockBehavior.Strict));

//			var validationResult = await validator.ValidateAsync(orderModel);
			
//			Assert.That(validationResult.IsValid, Is.False);
//		}

//		[Test]
//		public async Task NotValidEmptyCategoryId()
//		{
//			var orderModel = new OrderModel
//			{
//				Id = new Guid("2B5174E4-37B7-44EE-A8A2-EE920C6FAB9C"),
//				OrderCategory = new OrderCategoryModel { Id = new Guid() },
//				Products = new[]
//				{
//					new OrderedProductModel { Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D"), CustomOptions = new List<OrderedOptionModel>() },
//					new OrderedProductModel { Id = new Guid("4B5174E4-37B7-44EE-A8A2-EE920C6FAB9E"), CustomOptions = new List<OrderedOptionModel>() }
//				}
//			};
//			var validator = new Validator(Mock.Of<ICheckGateway>(MockBehavior.Strict));

//			var validationResult = await validator.ValidateAsync(orderModel);

//			Assert.That(validationResult.IsValid, Is.False);
//		}

//		[Test]
//		public async Task NotValidEmptyProductId()
//		{
//			var orderModel = new OrderModel
//			{
//				Id = new Guid("2B5174E4-37B7-44EE-A8A2-EE920C6FAB9C"),
//				OrderCategory = new OrderCategoryModel { Id = new Guid("885174E4-37B7-44EE-A8A2-EE920C6FAB9C") },
//				Products = new[]
//				{
//					new OrderedProductModel { Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D"), CustomOptions = new List<OrderedOptionModel>() },
//					new OrderedProductModel { Id = new Guid(), CustomOptions = new List<OrderedOptionModel>() }
//				}
//			};
//			var validator = new Validator(Mock.Of<ICheckGateway>(MockBehavior.Strict));

//			var validationResult = await validator.ValidateAsync(orderModel);

//			Assert.That(validationResult.IsValid, Is.False);
//		}

//		[Test]
//		public async Task NotValidEmptyProductList()
//		{
//			var orderModel = new OrderModel
//			{
//				Id = new Guid("2B5174E4-37B7-44EE-A8A2-EE920C6FAB9C"),
//				OrderCategory = new OrderCategoryModel { Id = new Guid("885174E4-37B7-44EE-A8A2-EE920C6FAB9C") },
//				Products = new List<OrderedProductModel>()
//			};
//			var validator = new Validator(Mock.Of<ICheckGateway>(MockBehavior.Strict));

//			var validationResult = await validator.ValidateAsync(orderModel);

//			Assert.That(validationResult.IsValid, Is.False);
//		}

//		[Test]
//		public async Task NotValidEmptyOptionId()
//		{
//			var orderModel = new OrderModel
//			{
//				Id = new Guid("2B5174E4-37B7-44EE-A8A2-EE920C6FAB9C"),
//				OrderCategory = new OrderCategoryModel { Id = new Guid("885174E4-37B7-44EE-A8A2-EE920C6FAB9C") },
//				Products = new[]
//				{
//					new OrderedProductModel
//					{
//						Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D"),
//						CustomOptions = new [] { new OrderedOptionModel { Id = new Guid() } }
//					}
//				}
//			};
//			var validator = new Validator(Mock.Of<ICheckGateway>(MockBehavior.Strict));

//			var validationResult = await validator.ValidateAsync(orderModel);

//			Assert.That(validationResult.IsValid, Is.False);
//		}

//		[Test]
//		public async Task NotValidDuplicatedOptionId()
//		{
//			var optionId = new Guid("A18C78E2-B8B3-4F08-8D0E-8D1687B31686");
//			var orderModel = new OrderModel
//			{
//				Id = new Guid("2B5174E4-37B7-44EE-A8A2-EE920C6FAB9C"),
//				OrderCategory = new OrderCategoryModel { Id = new Guid("885174E4-37B7-44EE-A8A2-EE920C6FAB9C") },
//				Products = new[]
//				{
//					new OrderedProductModel
//					{
//						Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D"),
//						CustomOptions = new []
//						{
//							new OrderedOptionModel { Id = optionId },
//							new OrderedOptionModel { Id = optionId }
//						}
//					}
//				}
//			};
//			var validator = new Validator(Mock.Of<ICheckGateway>(MockBehavior.Strict));

//			var validationResult = await validator.ValidateAsync(orderModel);

//			Assert.That(validationResult.IsValid, Is.False);
//		}

//		[Test]
//		public async Task NotValidOrderAlreadyExists()
//		{
//			var orderModel = new OrderModel
//			{
//				Id = new Guid("2B5174E4-37B7-44EE-A8A2-EE920C6FAB9C"),
//				OrderCategory = new OrderCategoryModel { Id = new Guid("885174E4-37B7-44EE-A8A2-EE920C6FAB9C") },
//				Products = new[]
//				{
//					new OrderedProductModel { Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D"), CustomOptions = new List<OrderedOptionModel>() },
//					new OrderedProductModel { Id = new Guid("4B5174E4-37B7-44EE-A8A2-EE920C6FAB9E"), CustomOptions = new List<OrderedOptionModel>() }
//				}
//			};
			
//			var checkGateway = new Mock<ICheckGateway>(MockBehavior.Strict);
//			checkGateway
//				.Setup(x => x.ExistsOrderAsync(orderModel.Id))
//				.ReturnsAsync(true);
			
//			var validator = new Validator(checkGateway.Object);

			
//			var validationResult = await validator.ValidateAsync(orderModel);

			
//			Assert.That(validationResult.IsValid, Is.False);
//		}

//		[Test]
//		public async Task NotValidMissingCategory()
//		{
//			var orderModel = new OrderModel
//			{
//				Id = new Guid("2B5174E4-37B7-44EE-A8A2-EE920C6FAB9C"),
//				OrderCategory = new OrderCategoryModel { Id = new Guid("885174E4-37B7-44EE-A8A2-EE920C6FAB9C") },
//				Products = new[]
//				{
//					new OrderedProductModel { Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D"), CustomOptions = new List<OrderedOptionModel>() },
//					new OrderedProductModel { Id = new Guid("4B5174E4-37B7-44EE-A8A2-EE920C6FAB9E"), CustomOptions = new List<OrderedOptionModel>() }
//				}
//			};
			
//			var products = new List<Product>
//			{
//				new Product { Id = orderModel.Products.First().Id, Category = new Category { Id = orderModel.OrderCategory.Id } },
//				new Product { Id = orderModel.Products.Last().Id, Category = new Category { Id = orderModel.OrderCategory.Id } }
//			};

//			var checkGateway = new Mock<ICheckGateway>(MockBehavior.Strict);
//			checkGateway
//				.Setup(x => x.ExistsOrderAsync(orderModel.Id))
//				.ReturnsAsync(false);
//			checkGateway
//				.Setup(x => x.GetProductsAsync(It.IsAny<IEnumerable<Guid>>()))
//				.ReturnsAsync(RequestResult<Product, Guid>.New(products, products.Select(x => x.Id)));
//			checkGateway
//				.Setup(x => x.ExistsCategoryAsync(orderModel.OrderCategory.Id))
//				.ReturnsAsync(false);
			
//			var validator = new Validator(checkGateway.Object);

			
//			var validationResult = await validator.ValidateAsync(orderModel);

			
//			Assert.That(validationResult.IsValid, Is.False);
//		}

//		[Test]
//		public async Task NotValidCategoryMisalignment()
//		{
//			var orderModel = new OrderModel
//			{
//				Id = new Guid("2B5174E4-37B7-44EE-A8A2-EE920C6FAB9C"),
//				OrderCategory = new OrderCategoryModel { Id = new Guid("885174E4-37B7-44EE-A8A2-EE920C6FAB9C") },
//				Products = new[]
//				{
//					new OrderedProductModel { Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D"), CustomOptions = new List<OrderedOptionModel>() },
//					new OrderedProductModel { Id = new Guid("4B5174E4-37B7-44EE-A8A2-EE920C6FAB9E"), CustomOptions = new List<OrderedOptionModel>() }
//				}
//			};
			
//			var products = new List<Product>
//			{
//				new Product { Id = orderModel.Products.First().Id, Category = new Category { Id = orderModel.OrderCategory.Id } },
//				new Product { Id = orderModel.Products.Last().Id, Category = new Category { Id = new Guid("995174E4-37B7-44EE-A8A2-EE920C6FAB9C") } }
//			};

//			var checkGateway = new Mock<ICheckGateway>(MockBehavior.Strict);
//			checkGateway
//				.Setup(x => x.ExistsOrderAsync(orderModel.Id))
//				.ReturnsAsync(false);
//			checkGateway
//				.Setup(x => x.GetProductsAsync(It.IsAny<IEnumerable<Guid>>()))
//				.ReturnsAsync(RequestResult<Product, Guid>.New(products, products.Select(x => x.Id)));
//			checkGateway
//				.Setup(x => x.ExistsCategoryAsync(orderModel.OrderCategory.Id))
//				.ReturnsAsync(true);
//			var validator = new Validator(checkGateway.Object);

			
//			var validationResult = await validator.ValidateAsync(orderModel);

			
//			Assert.That(validationResult.IsValid, Is.False);
//		}

//		[Test]
//		public async Task NotValidMissingProduct()
//		{
//			var orderModel = new OrderModel
//			{
//				Id = new Guid("2B5174E4-37B7-44EE-A8A2-EE920C6FAB9C"),
//				OrderCategory = new OrderCategoryModel { Id = new Guid("885174E4-37B7-44EE-A8A2-EE920C6FAB9C") },
//				Products = new[]
//				{
//					new OrderedProductModel { Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D"), CustomOptions = new List<OrderedOptionModel>() },
//					new OrderedProductModel { Id = new Guid("4B5174E4-37B7-44EE-A8A2-EE920C6FAB9E"), CustomOptions = new List<OrderedOptionModel>() }
//				}
//			};
			
//			var products = new List<Product>
//			{
//				new Product { Id = orderModel.Products.First().Id, Category = new Category { Id = orderModel.OrderCategory.Id }, Options = new List<Option>() }
//			};

//			var checkGateway = new Mock<ICheckGateway>(MockBehavior.Strict);
//			checkGateway
//				.Setup(x => x.ExistsOrderAsync(orderModel.Id))
//				.ReturnsAsync(false);
//			checkGateway
//				.Setup(x => x.GetProductsAsync(It.IsAny<IEnumerable<Guid>>()))
//				.ReturnsAsync(RequestResult<Product, Guid>.New(products, products.Select(x => x.Id)));
//			checkGateway
//				.Setup(x => x.ExistsCategoryAsync(orderModel.OrderCategory.Id))
//				.ReturnsAsync(true);
			
//			var validator = new Validator(checkGateway.Object);

			
//			var validationResult = await validator.ValidateAsync(orderModel);

			
//			Assert.That(validationResult.IsValid, Is.False);
//		}

//		[Test]
//		public async Task NotValidMissingOptions()
//		{
//			var orderModel = new OrderModel
//			{
//				Id = new Guid("2B5174E4-37B7-44EE-A8A2-EE920C6FAB9C"),
//				OrderCategory = new OrderCategoryModel { Id = new Guid("885174E4-37B7-44EE-A8A2-EE920C6FAB9C") },
//				Products = new[]
//				{
//					new OrderedProductModel
//					{
//						Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D"), 
//						CustomOptions = new []
//						{
//							new OrderedOptionModel { Id = new Guid("777174E4-37B7-44EE-A8A2-EE920C6FAB9D") },
//							new OrderedOptionModel { Id = new Guid("888174E4-37B7-44EE-A8A2-EE920C6FAB9D") }
//						}
//					}
//				}
//			};

//			var products = new List<Product>
//			{
//				new Product { Id = orderModel.Products.Single().Id, Category = new Category { Id = orderModel.OrderCategory.Id }, Options = new List<Option>() }
//			};
			
//			var checkGateway = new Mock<ICheckGateway>(MockBehavior.Strict);
//			checkGateway
//				.Setup(x => x.ExistsOrderAsync(orderModel.Id))
//				.ReturnsAsync(false);
//			checkGateway
//				.Setup(x => x.GetProductsAsync(It.IsAny<IEnumerable<Guid>>()))
//				.ReturnsAsync(RequestResult<Product, Guid>.New(products, products.Select(x => x.Id)));
//			checkGateway
//				.Setup(x => x.ExistsCategoryAsync(orderModel.OrderCategory.Id))
//				.ReturnsAsync(true);
			
//			var validator = new Validator(checkGateway.Object);

			
//			var validationResult = await validator.ValidateAsync(orderModel);

			
//			Assert.That(validationResult.IsValid, Is.False);
//		}
//	}
//}