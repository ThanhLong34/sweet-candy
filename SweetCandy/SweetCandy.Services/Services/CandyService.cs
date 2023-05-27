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

            return await queryable.Include(i => i.Category).Select(i => new Candy()
            {
                Id = i.Id,
                Name = i.Name,
                Price = i.Price,
                ExpirationDate = i.ExpirationDate,
                CategoryId = i.CategoryId,
                Category = i.Category,
            }).ToListAsync(cancellationToken);
        }

        public async Task<Candy> AddOrUpdateCandyAsync(
            string candyName,
            decimal candyPrice,
            DateTime candyExpirationDate,
            int categoryId,
            int? candyId = null,
            CancellationToken cancellationToken = default
        )
        {
            Candy candy = new Candy()
            {
                Id = candyId ?? 0,
                Name = candyName,
                Price = candyPrice,
                ExpirationDate = candyExpirationDate,
                CategoryId = categoryId,
            };

            if (candy.Id == 0)
            {
                _dbContext.Candies.Add(candy);
            }
            else
            {
                _dbContext.Candies.Update(candy);
            }

            bool isSuccess = await _dbContext.SaveChangesAsync(cancellationToken) > 0;
            return isSuccess ? candy : null;
        }

        public async Task<bool> DeleteCandyAsync(int? candyId = null, CancellationToken cancellationToken = default)
        {
            if (candyId == null)
                return false;

            IQueryable<Candy> queryable = _dbContext.Set<Candy>();
            bool isSuccess = await queryable.Where(i => i.Id == candyId).ExecuteDeleteAsync(cancellationToken) > 0;
            return isSuccess;
        }
    }
}
