using PhotoSi.Addresses.Controllers.Models;
using PhotoSi.Addresses.Core.Models;

namespace PhotoSi.Addresses.Controllers;

public interface IApiLayerTranslator
{
	AddrressModel Translate(Address source);
	Address Translate(AddrressModel source);
}