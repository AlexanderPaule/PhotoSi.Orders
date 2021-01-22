using System;
using System.Collections.Generic;
using PhotoSi.Orders.Server.Orders.Core.Dto;

namespace PhotoSi.Orders.Server.Demo.Controllers.Data
{
	internal class DemoDataCatalog : IDemoDataCatalog
	{
		private readonly Category _categoryPoster;
		private readonly Category _categoryPhoto;

		public DemoDataCatalog()
		{
			_categoryPoster = new Category
			{
				Id = new Guid("111174E4-37B7-44EE-A8A2-EE920C6FAB9D"),
				Name = "Poster",
				Description = "Selezione poster 2020"
			};
			_categoryPhoto = new Category
			{
				Id = new Guid("222174E4-37B7-44EE-A8A2-EE920C6FAB9D"),
				Name = "Foto",
				Description = "Selezione foto 2020"
			};
		}

		public IEnumerable<Category> GetCategories()
		{
			return new[]
			{
				_categoryPoster,
				_categoryPhoto
			};
		}

		public IEnumerable<Product> GetProducts()
		{
			return new[]
			{
				new Product
				{
					Id = new Guid("211174E4-37B7-44EE-A8A2-EE920C6FAB9D"),
					Category = _categoryPhoto,
					Description = "Parigi D'inverno",
					Options = new []
					{
						new Option { Id = new Guid("311174E4-37B7-44EE-A8A2-EE920C6FAB9D"), Name = "Dimensioni", Content = "20cm x 20cm"},
						new Option { Id = new Guid("411174E4-37B7-44EE-A8A2-EE920C6FAB9D"), Name = "Tipo Carta", Content = "Stoffa"},
						new Option { Id = new Guid("511174E4-37B7-44EE-A8A2-EE920C6FAB9D"), Name = "CartaColore", Content = "Blu Petrolio"},
					}
				},
				new Product
				{
					Id = new Guid("611174E4-37B7-44EE-A8A2-EE920C6FAB9D"),
					Category = _categoryPhoto,
					Description = "Sole a catinelle",
					Options = new []
					{
						new Option { Id = new Guid("711174E4-37B7-44EE-A8A2-EE920C6FAB9D"), Name = "Dimensioni", Content = "30cm x 60cm"},
						new Option { Id = new Guid("811174E4-37B7-44EE-A8A2-EE920C6FAB9D"), Name = "Tipo Carta", Content = "Ruvido"},
						new Option { Id = new Guid("911174E4-37B7-44EE-A8A2-EE920C6FAB9D"), Name = "Carta Colore", Content = "Bianco"},
					}
				},
				new Product
				{
					Id = new Guid("121174E4-37B7-44EE-A8A2-EE920C6FAB9D"),
					Category = _categoryPhoto,
					Description = "New York Skyline",
					Options = new []
					{
						new Option { Id = new Guid("131174E4-37B7-44EE-A8A2-EE920C6FAB9D"), Name = "Dimensioni", Content = "30cm x 30cm"},
						new Option { Id = new Guid("141174E4-37B7-44EE-A8A2-EE920C6FAB9D"), Name = "Tipo Carta", Content = "Lucido"},
						new Option { Id = new Guid("151174E4-37B7-44EE-A8A2-EE920C6FAB9D"), Name = "Carta Colore", Content = "Blu Petrolio"},
					}
				},
				new Product
				{
					Id = new Guid("161174E4-37B7-44EE-A8A2-EE920C6FAB9D"),
					Category = _categoryPoster,
					Description = "Black Sabbath",
					Options = new []
					{
						new Option { Id = new Guid("171174E4-37B7-44EE-A8A2-EE920C6FAB9D"), Name = "Dimensioni", Content = "1m x 2m"},
						new Option { Id = new Guid("181174E4-37B7-44EE-A8A2-EE920C6FAB9D"), Name = "Tipo Carta", Content = "Lucido"},
						new Option { Id = new Guid("191174E4-37B7-44EE-A8A2-EE920C6FAB9D"), Name = "Carta Colore", Content = "Nero"},
					}
				},
				new Product
				{
					Id = new Guid("112174E4-37B7-44EE-A8A2-EE920C6FAB9D"),
					Category = _categoryPoster,
					Description = "Miley Cyrus",
					Options = new []
					{
						new Option { Id = new Guid("113174E4-37B7-44EE-A8A2-EE920C6FAB9D"), Name = "Dimensioni", Content = "1m x 2m"},
						new Option { Id = new Guid("114174E4-37B7-44EE-A8A2-EE920C6FAB9D"), Name = "Tipo Carta", Content = "Lucido"},
						new Option { Id = new Guid("115174E4-37B7-44EE-A8A2-EE920C6FAB9D"), Name = "Carta Colore", Content = "Rosa"},
					}
				}
			};
		}
	}
}