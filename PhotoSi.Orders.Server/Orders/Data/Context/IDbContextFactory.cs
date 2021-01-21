namespace PhotoSi.Orders.Server.Orders.Data.Context
{
	internal interface IDbContextFactory
	{
		SalesDbContext CreateDbContext();
	}
}