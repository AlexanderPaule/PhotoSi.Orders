using System;

namespace PhotoSi.Orders.Server.Orders.Data.Models
{
	public class TimeTrackedEntity
	{
		public DateTimeOffset DbCreated { get; set; }
		public DateTimeOffset DbUpdated { get; set; }
	}
}