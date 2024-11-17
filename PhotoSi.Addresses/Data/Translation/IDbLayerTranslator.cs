using PhotoSi.Addresses.Core.Models;
using PhotoSi.Addresses.Data.Models;

namespace PhotoSi.Addresses.Data.Translation;

public interface IDbLayerTranslator
{
	Address Translate(AddressEntity source);
	AddressEntity Translate(Address source);
}