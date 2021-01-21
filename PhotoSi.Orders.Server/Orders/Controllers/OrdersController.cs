using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoSi.Orders.Server.Orders.Controllers.Models;

namespace PhotoSi.Orders.Server.Orders.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class OrdersController : ControllerBase
	{
		private readonly ILogger<OrdersController> _logger;

		public OrdersController(ILogger<OrdersController> logger)
		{
			_logger = logger;
		}

		[HttpPost]
		public void Create(OrderModel order)
		{
			throw new NotImplementedException();
		}
	}
}
