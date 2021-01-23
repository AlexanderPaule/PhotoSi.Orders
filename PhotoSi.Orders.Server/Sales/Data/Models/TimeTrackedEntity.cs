using System;

namespace PhotoSi.Sales.Sales.Data.Models
{
	public class TimeTrackedEntity
	{
		public DateTimeOffset DbCreated { get; set; }
		public DateTimeOffset DbUpdated { get; set; }
	}
}