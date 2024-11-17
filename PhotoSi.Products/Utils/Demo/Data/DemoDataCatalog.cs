using PhotoSi.Products.Core.Models;
using PhotoSi.Products.Utils.Demo.Controllers;

namespace PhotoSi.Products.Utils.Demo.Data;

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
		return
		[
			_categoryPoster,
			_categoryPhoto
		];
	}

	public IEnumerable<Product> GetProducts()
	{
		return
		[
			new Product
			{
				Id = new Guid("211174E4-37B7-44EE-A8A2-EE920C6FAB9D"),
				Category = _categoryPhoto,
				Description = "Parigi D'inverno"
			},
			new Product
			{
				Id = new Guid("611174E4-37B7-44EE-A8A2-EE920C6FAB9D"),
				Category = _categoryPhoto,
				Description = "Sole a catinelle"
			},
			new Product
			{
				Id = new Guid("121174E4-37B7-44EE-A8A2-EE920C6FAB9D"),
				Category = _categoryPhoto,
				Description = "New York Skyline"
			},
			new Product
			{
				Id = new Guid("161174E4-37B7-44EE-A8A2-EE920C6FAB9D"),
				Category = _categoryPoster,
				Description = "Black Sabbath"
			},
			new Product
			{
				Id = new Guid("112174E4-37B7-44EE-A8A2-EE920C6FAB9D"),
				Category = _categoryPoster,
				Description = "Miley Cyrus"
			}
		];
	}
}