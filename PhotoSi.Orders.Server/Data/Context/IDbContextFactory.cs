namespace PhotoSi.Orders.Data.Context;

internal interface IDbContextFactory
{
	OrdersDbContext CreateDbContext();
}