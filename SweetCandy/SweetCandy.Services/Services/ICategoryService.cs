using SweetCandy.Core.Entities;

namespace SweetCandy.Services.Services
{
    public interface ICategoryService
    {
        public Task<IList<Category>> GetCategoriesAsync(
            string name = null,
            bool? ShowOnMenu = false,
            CancellationToken cancellationToken = default
        );

        public Task<Category> AddOrUpdateCategoryAsync(
            string categoryName,
            bool showOnMenu = false,
            int? categoryId = null,
            CancellationToken cancellationToken = default
        );

        public Task<bool> DeleteCategoryAsync(
            int? categoryId = null,
            CancellationToken cancellationToken = default
        );
    }
}
