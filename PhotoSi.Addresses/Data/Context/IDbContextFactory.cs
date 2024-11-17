namespace PhotoSi.Addresses.Data.Context;

internal interface IDbContextFactory
{
	AddressesDbContext CreateDbContext();
}