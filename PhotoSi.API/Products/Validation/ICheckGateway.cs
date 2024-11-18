namespace PhotoSi.API.Products.Validation;

internal interface ICheckGateway
{
	Task<bool> ExistsCategoryAsync(Guid id);
}