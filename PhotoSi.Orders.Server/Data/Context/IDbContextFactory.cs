namespace PhotoSi.Orders.Data.Context;

public interface IDbContextFactory
{
	OrdersDbContext CreateDbContext();
}