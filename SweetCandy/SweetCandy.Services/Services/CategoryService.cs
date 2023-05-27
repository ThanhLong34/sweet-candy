using Microsoft.EntityFrameworkCore;
using SweetCandy.Core.Entities;
using SweetCandy.Data.Contexts;

namespace SweetCandy.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly SweetCandyContext _dbContext;

        public CategoryService(SweetCandyContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<Category>> GetCategoriesAsync(
            string name = null,
            bool? ShowOnMenu = false,
            CancellationToken cancellationToken = default
        )
        {
            IQueryable<Category> queryable = _dbContext.Set<Category>();

            if (!string.IsNullOrWhiteSpace(name))
            {
                queryable = queryable.Where(i => i.Name.Contains(name));
            }

            if (ShowOnMenu.HasValue)
            {
                queryable = queryable.Where(i => i.ShowOnMenu == ShowOnMenu);
            }

            return await queryable.Include(i => i.Candies).Select(i => new Category()
            {
                Id = i.Id,
                Name = i.Name,
                ShowOnMenu = i.ShowOnMenu,
                Candies = i.Candies,
            }).ToListAsync(cancellationToken);
        }

        public async Task<Category> AddOrUpdateCategoryAsync(
            string categoryName,
            bool showOnMenu = false,
            int? categoryId = null,
            CancellationToken cancellationToken = default
        )
        {
            Category category = new Category()
            {
                Id = categoryId ?? 0,
                Name = categoryName,
                ShowOnMenu = showOnMenu,
            };

            if (category.Id == 0)
            {
                _dbContext.Categories.Add(category);
            } else
            {
                _dbContext.Categories.Update(category);
            }

            bool isSuccess = await _dbContext.SaveChangesAsync(cancellationToken) > 0;
            return isSuccess ? category : null;
        }

        public async Task<bool> DeleteCategoryAsync(
            int? categoryId = null, 
            CancellationToken cancellationToken = default
        )
        {
            if (categoryId == null) 
                return false;

            IQueryable<Category> queryable = _dbContext.Set<Category>();
            bool isSuccess = await queryable.Where(i => i.Id == categoryId).ExecuteDeleteAsync(cancellationToken) > 0;
            return isSuccess;
        }

        public async Task<Category> FindCategoryByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            IQueryable<Category> queryable = _dbContext.Set<Category>();
            return await queryable.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
        }
    }
}
