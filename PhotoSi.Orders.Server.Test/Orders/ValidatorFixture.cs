using System;
using System.Threading.Tasks;
using NUnit.Framework;
using PhotoSi.Orders.Server.Orders.Controllers.Models;
using PhotoSi.Orders.Server.Orders.Controllers.Validation;

namespace PhotoSi.Orders.Server.Test.Orders
{
	[TestFixture]
	internal class ValidatorFixture
	{
		private Validator _validator;

		[SetUp]
		public void SetUp()
		{
			_validator = new Validator();
		}

		[Test]
		public async Task ValidOrder()
		{
			var orderModel = new OrderModel
			{
				Id = new Guid("2B5174E4-37B7-44EE-A8A2-EE920C6FAB9C"),
				Category = new Category { Id = new Guid("885174E4-37B7-44EE-A8A2-EE920C6FAB9C") },
				Products = new []
				{
					new OrderedProductModel { Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D") },
					new OrderedProductModel { Id = new Guid("4B5174E4-37B7-44EE-A8A2-EE920C6FAB9E") }
				}
			};

			var validationResult = await _validator.ValidateAsync(orderModel);
			
			Assert.That(validationResult.IsValid, Is.True);
		}

		[Test]
		public async Task NotValidEmptyId()
		{
			var orderModel = new OrderModel
			{
				Id = new Guid(),
				Category = new Category { Id = new Guid("885174E4-37B7-44EE-A8A2-EE920C6FAB9C") },
				Products = new []
				{
					new OrderedProductModel { Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D") },
					new OrderedProductModel { Id = new Guid("4B5174E4-37B7-44EE-A8A2-EE920C6FAB9E") }
				}
			};

			var validationResult = await _validator.ValidateAsync(orderModel);
			
			Assert.That(validationResult.IsValid, Is.False);
		}

		[Test]
		public async Task NotValidEmptyCategoryId()
		{
			var orderModel = new OrderModel
			{
				Id = new Guid("2B5174E4-37B7-44EE-A8A2-EE920C6FAB9C"),
				Category = new Category { Id = new Guid() },
				Products = new[]
				{
					new OrderedProductModel { Id = new Guid("3B5174E4-37B7-44EE-A8A2-EE920C6FAB9D") },
					new OrderedProductModel { Id = new Guid("4B5174E4-37B7-44EE-A8A2-EE920C6FAB9E") }
				}
			};

			var validationResult = await _validator.ValidateAsync(orderModel);

			Assert.That(validationResult.IsValid, Is.False);
		}
	}
}