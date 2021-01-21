﻿using System;
using System.Threading.Tasks;
using PhotoSi.Orders.Server.Orders.Core.Models;

namespace PhotoSi.Orders.Server.Orders.Core
{
	public interface IOrderEngine
	{
		Task ProcessAsync(Order order);
		Task<RequestResult<Order>> GetAsync(Guid id);
	}
}