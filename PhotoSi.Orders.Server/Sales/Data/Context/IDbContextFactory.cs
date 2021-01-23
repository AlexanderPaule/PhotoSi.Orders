namespace PhotoSi.Orders.Server.Sales.Data.Context
{
	internal interface IDbContextFactory
	{
		SalesDbContext CreateDbContext();
	}
}