using System;
using System.Linq;
using NUnit.Framework;
using PhotoSi.Sales.Sales.Core.Models;
using PhotoSi.Sales.Sales.Data.Models;

namespace PhotoSi.Sales.Test.Sales
{
	[TestFixture]
	internal class DbLayerTranslatorFixture
	{
		[Test]
		public void TranslateProduct()
		{
			var category = new CategoryEntity
			{
				Id = new Guid("A4079731-1371-4CCE-83B2-26E9464C8AA9"),
				Name = "Poster",
				Description = "Posters Punk"
			};

			var dbCatalogOrder = new ProductEntity
			{
				Id = new Guid("C5F81452-2383-479A-AA69-FF29268CD1FA"),
				Category = category,
				Description = "Sum 41",
				Options = new[]
				{
					new OptionEntity
					{
						Id = new Guid("239B0406-C2B3-4683-AB7C-061338226502"),
						Name = "Size",
						Content = "200cm x 200cm"
					},
					new OptionEntity
					{
						Id = new Guid("77D4506F-87C0-4790-B46C-E9E0046960B0"),
						Name = "Paper Color",
						Content = "Red"
					}
				}
			};

			var dbOrderedProduct = new OrderedProductEntity
			{
				Id = new Guid("F8D1A374-6D91-42AC-A4B2-A309D9E11EC0"),
				CustomOptions = new[]
				{
					new OrderedOptionEntity
					{
						Id = new Guid("9096310E-D3A3-4927-BB0A-987D501156FF"),
						Content = "100cm x 100cm",
						OptionId = dbCatalogOrder.Options.First<OptionEntity>().Id,
						ReferencedOption = dbCatalogOrder.Options.First()
					}
				},
				ReferencedProduct = dbCatalogOrder
			};
			
			var dbOrder = new OrderEntity
			{
				Id = new Guid("EC2DBD26-7AC5-4962-A627-39E271070D23"),
				CreatedOn = DateTimeOffset.Now,
				Category = category ,
				Products = new []
				{
					dbOrderedProduct
				}
			};
			
			var translator = new DbLayerTranslator();

			
			var coreOrder = translator.Translate(dbOrder);
			
			
			Assert.That(coreOrder, Is.Not.Null);
			Assert.That(coreOrder.Id, Is.EqualTo(dbOrder.Id));
			
			Assert.That(coreOrder.Category, Is.Not.Null);
			Assert.That(coreOrder.Category.Id, Is.EqualTo(category.Id));
			Assert.That(coreOrder.Category.Name, Is.EqualTo(category.Name));
			Assert.That(coreOrder.Category.Description, Is.EqualTo(category.Description));
			
			Assert.That(Enumerable.Count<Product>(coreOrder.Products), Is.EqualTo(1));
			var coreOrderProduct = Enumerable.Single<Product>(coreOrder.Products);
			Assert.That(coreOrderProduct.Id, Is.EqualTo(dbCatalogOrder.Id));
			Assert.That(coreOrderProduct.Category.Id, Is.EqualTo(category.Id));
			Assert.That(coreOrderProduct.Category.Name, Is.EqualTo(category.Name));
			Assert.That(coreOrderProduct.Category.Description, Is.EqualTo(category.Description));
			Assert.That(coreOrderProduct.Description, Is.EqualTo(dbCatalogOrder.Description));
			Assert.That(coreOrderProduct.Options.Count(), Is.EqualTo(2));

			var option1 = coreOrderProduct.Options.First();
			Assert.That(option1, Is.Not.Null);
			Assert.That(option1.Id, Is.EqualTo(dbCatalogOrder.Options.First().Id));
			Assert.That(option1.Name, Is.EqualTo(dbCatalogOrder.Options.First().Name));
			Assert.That(option1.Content, Is.EqualTo(dbOrderedProduct.CustomOptions.Single().Content));

			var option2 = coreOrderProduct.Options.Last();
			Assert.That(option2, Is.Not.Null);
			Assert.That(option2.Id, Is.EqualTo(dbCatalogOrder.Options.Last().Id));
			Assert.That(option2.Name, Is.EqualTo(dbCatalogOrder.Options.Last().Name));
			Assert.That(option2.Content, Is.EqualTo(dbCatalogOrder.Options.Last().Content));
		}
	}
}