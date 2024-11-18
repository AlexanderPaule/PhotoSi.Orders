using PhotoSi.Addresses.Core.Models;
using PhotoSi.API.Addresses.Models;

namespace PhotoSi.API.Addresses.Translation;

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