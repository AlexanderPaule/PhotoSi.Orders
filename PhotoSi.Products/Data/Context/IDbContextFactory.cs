namespace PhotoSi.Products.Data.Context;

internal interface IDbContextFactory
{
	ProductsDbContext CreateDbContext();
}