namespace ClientsWebApp.Domain.Categories
{
    public interface ICategoryService
    {
        public Task CreateAsync(CreateCategoryModel model, CancellationToken cancellationToken);
    }
}
