using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoSi.Orders.Core;

internal interface ICheckGateway
{
	Task<bool> ExistsOrderAsync(Guid id);
	Task<bool> ExistsAddressAsync(Guid id);
	Task<IDictionary<Guid, bool>> ExistsProductsAsync(IEnumerable<Guid> id);
}