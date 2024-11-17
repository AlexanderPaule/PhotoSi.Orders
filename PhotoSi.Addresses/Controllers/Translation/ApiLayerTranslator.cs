using PhotoSi.Addresses.Controllers.Models;
using PhotoSi.Addresses.Core.Models;

namespace PhotoSi.Addresses.Controllers.Translation;

internal class ApiLayerTranslator : IApiLayerTranslator
{

	public AddrressModel Translate(Address source)
	{
		return new AddrressModel
		{
			Id = source.Id,
			UserId = source.UserId,
			City = source.City,
			Country = source.Country
		};
	}

	public Address Translate(AddrressModel source)
	{
		return new Address
		{
			Id = source.Id,
			UserId = source.Id,
			City = source.City,
			Country = source.Country
		};
	}
}