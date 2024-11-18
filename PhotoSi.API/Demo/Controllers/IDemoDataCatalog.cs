using PhotoSi.Addresses.Core.Models;
using PhotoSi.Products.Core.Models;
using PhotoSi.Users.Core.Models;

namespace PhotoSi.API.Demo.Controllers;

public interface IDemoDataCatalog
{
	IEnumerable<Address> GetAddresses();
	IEnumerable<Category> GetCategories();
	IEnumerable<Product> GetProducts();
	IEnumerable<User> GetUsers();
}