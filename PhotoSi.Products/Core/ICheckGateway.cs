namespace PhotoSi.Products.Core;

internal interface ICheckGateway
{
	Task<bool> ExistsCategoryAsync(Guid id);
}