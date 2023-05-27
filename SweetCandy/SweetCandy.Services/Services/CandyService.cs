using Microsoft.EntityFrameworkCore;
using SweetCandy.Core.Entities;
using SweetCandy.Data.Contexts;

namespace SweetCandy.Services.Services
{
    public class CandyService : ICandyService
    {
        private readonly SweetCandyContext _dbContext;

        public CandyService(SweetCandyContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<Candy>> GetCandiesAsync(
            string name = null, 
            int? categoryId = null, 
            string categoryName = null, 
            decimal? minPrice = null, 
            decimal? maxPrice = null, 
            CancellationToken cancellationToken = default
        )
        {
            IQueryable<Candy> queryable = _dbContext.Set<Candy>();

            // Tìm kẹo chưa hết hạn sử dụng
            queryable = queryable.Where(i => DateTime.Now <= i.ExpirationDate);

            if (!string.IsNullOrWhiteSpace(name))
            {
                queryable = queryable.Where(i => i.Name.Contains(name));
            }

            if (categoryId.HasValue)
            {
                queryable = queryable.Where(c => c.CategoryId == categoryId);
            }
            else if (!string.IsNullOrWhiteSpace(categoryName))
            {
                queryable = queryable.Where(i => i.Category.Name == categoryName);
            }

            if (minPrice.HasValue)
            {
                queryable = queryable.Where(c => c.Price >= minPrice);
            }

            if (maxPrice.HasValue)
            {
                queryable = queryable.Where(c => c.Price <= maxPrice);
            }

            queryable = queryable.OrderBy(c => c.Name);

            return await queryable.ToListAsync(cancellationToken);
        }

        public Task<Candy> AddOrUpdateCandyAsync(string categoryName, bool showOnMenu = false, int? categoryId = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCandyAsync(int? categoryId = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
