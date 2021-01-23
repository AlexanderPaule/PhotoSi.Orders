using System;

namespace PhotoSi.Orders.Server.Sales.Data.Models
{
	public class TimeTrackedEntity
	{
		public DateTimeOffset DbCreated { get; set; }
		public DateTimeOffset DbUpdated { get; set; }
	}
}