using PhotoSi.Addresses.Core.Models;
using PhotoSi.Addresses.Data.Models;

namespace PhotoSi.Addresses.Data.Translation;

internal class DbLayerTranslator : IDbLayerTranslator
{
	public Address Translate(AddressEntity source)
	{
		return new Address
		{
			Id = source.Id,
			UserId = source.UserId,
			City = source.City,
			Country = source.Country
		};
	}

	public AddressEntity Translate(Address source)
	{
		return new AddressEntity
		{
			Id = source.Id,
			UserId = source.UserId,
			City = source.City,
			Country = source.Country
		};
	}
}