namespace PhotoSi.Users.Data.Context;

internal interface IDbContextFactory
{
	UsersDbContext CreateDbContext();
}