namespace PhotoSi.Sales.Sales.Data.Context
{
	internal interface IDbContextFactory
	{
		SalesDbContext CreateDbContext();
	}
}