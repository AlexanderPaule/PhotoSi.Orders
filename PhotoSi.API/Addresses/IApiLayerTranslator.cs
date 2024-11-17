using PhotoSi.Addresses.Core.Models;
using PhotoSi.API.Addresses.Models;

namespace PhotoSi.API.Addresses;

public interface IApiLayerTranslator
{
	AddrressModel Translate(Address source);
	Address Translate(AddrressModel source);
}