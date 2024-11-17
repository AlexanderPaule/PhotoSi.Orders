using System;

namespace PhotoSi.Orders.Data.Models;

public class TimeTrackedEntity
{
	public DateTimeOffset DbCreated { get; set; }
	public DateTimeOffset DbUpdated { get; set; }
}