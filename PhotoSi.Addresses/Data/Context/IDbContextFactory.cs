namespace PhotoSi.Addresses.Data.Context;

public interface IDbContextFactory
{
	AddressesDbContext CreateDbContext();
}