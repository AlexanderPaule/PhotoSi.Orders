using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using PhotoSi.Sales.Products.Models;
using PhotoSi.Sales.Products.Validation;
using PhotoSi.Sales.Sales.Core;
using PhotoSi.Sales.Sales.Core.Models;

namespace PhotoSi.Sales.Test.Products
{
	[TestFixture]
	internal class ValidatorFixture
	{
		[Test]
		public async Task ValidOrder()
		{
			var productModel = new ProductModel
			{
				Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D"),
				Category = new CategoryModel {Id = new Guid("1AB1C2E0-2929-4D79-8B17-FEF390822F71")},
				Options = new []
				{
					new OptionModel { Id = new Guid("B885C0E8-23B2-482C-8D59-8BD96EF58AA3") },
					new OptionModel { Id = new Guid("05FC5BD3-BC16-4EAD-8A1E-1D92C2060D8E") }
				}
			};
			
			var checkGateway = new Mock<ICheckGateway>(MockBehavior.Strict);
			checkGateway
				.Setup(x => x.ExistsCategoryAsync(productModel.Category.Id))
				.ReturnsAsync(true);
			
			var validator = new Validator(checkGateway.Object);

			
			var validationResult = await validator.ValidateAsync(productModel);
			
			
			Assert.That(validationResult.IsValid, Is.True, validationResult.GetErrorMessage);
		}
		
		[Test]
		public async Task NotValidIdEmpty()
		{
			var productModel = new ProductModel
			{
				Id = new Guid(),
				Category = new CategoryModel {Id = new Guid("1AB1C2E0-2929-4D79-8B17-FEF390822F71")},
				Options = new []
				{
					new OptionModel { Id = new Guid("B885C0E8-23B2-482C-8D59-8BD96EF58AA3") },
					new OptionModel { Id = new Guid("05FC5BD3-BC16-4EAD-8A1E-1D92C2060D8E") }
				}
			};
			var validator = new Validator(Mock.Of<ICheckGateway>(MockBehavior.Strict));

			
			var validationResult = await validator.ValidateAsync(productModel);
			
			
			Assert.That(validationResult.IsValid, Is.False);
		}

		[Test]
		public async Task NotValidCategoryIdEmpty()
		{
			var productModel = new ProductModel
			{
				Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D"),
				Category = new CategoryModel { Id = new Guid() },
				Options = new[]
				{
					new OptionModel { Id = new Guid("B885C0E8-23B2-482C-8D59-8BD96EF58AA3") },
					new OptionModel { Id = new Guid("05FC5BD3-BC16-4EAD-8A1E-1D92C2060D8E") }
				}
			};
			var validator = new Validator(Mock.Of<ICheckGateway>(MockBehavior.Strict));


			var validationResult = await validator.ValidateAsync(productModel);


			Assert.That(validationResult.IsValid, Is.False);
		}

		[Test]
		public async Task NotValidOptionIdEmpty()
		{
			var productModel = new ProductModel
			{
				Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D"),
				Category = new CategoryModel { Id = new Guid("1AB1C2E0-2929-4D79-8B17-FEF390822F71") },
				Options = new[]
				{
					new OptionModel { Id = new Guid("B885C0E8-23B2-482C-8D59-8BD96EF58AA3") },
					new OptionModel { Id = new Guid() }
				}
			};
			var validator = new Validator(Mock.Of<ICheckGateway>(MockBehavior.Strict));


			var validationResult = await validator.ValidateAsync(productModel);


			Assert.That(validationResult.IsValid, Is.False);
		}

		[Test]
		public async Task NotValidDuplicatedOption()
		{
			var duplicatedOptionId = new Guid("05FC5BD3-BC16-4EAD-8A1E-1D92C2060D8E");
			var productModel = new ProductModel
			{
				Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D"),
				Category = new CategoryModel { Id = new Guid("1AB1C2E0-2929-4D79-8B17-FEF390822F71") },
				Options = new[]
				{
					new OptionModel { Id = duplicatedOptionId },
					new OptionModel { Id = duplicatedOptionId }
				}
			};
			var validator = new Validator(Mock.Of<ICheckGateway>(MockBehavior.Strict));


			var validationResult = await validator.ValidateAsync(productModel);


			Assert.That(validationResult.IsValid, Is.False);
		}
		
		[Test]
		public async Task NotValidNotExistingCategory()
		{
			var productModel = new ProductModel
			{
				Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D"),
				Category = new CategoryModel { Id = new Guid("1AB1C2E0-2929-4D79-8B17-FEF390822F71") },
				Options = new[]
				{
					new OptionModel { Id = new Guid("B885C0E8-23B2-482C-8D59-8BD96EF58AA3") },
					new OptionModel { Id = new Guid("05FC5BD3-BC16-4EAD-8A1E-1D92C2060D8E") }
				}
			};

			var checkGateway = new Mock<ICheckGateway>(MockBehavior.Strict);
			checkGateway
				.Setup(x => x.ExistsCategoryAsync(productModel.Category.Id))
				.ReturnsAsync(false);

			var validator = new Validator(checkGateway.Object);


			var validationResult = await validator.ValidateAsync(productModel);


			Assert.That(validationResult.IsValid, Is.False);
		}
	}
}