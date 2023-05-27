using SweetCandy.Core.Entities;

namespace SweetCandy.Services.Services
{
    public interface ICandyService
    {
        public Task<IList<Candy>> GetCandiesAsync(
            string name = null,
            int? categoryId = null,
            string categoryName = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            CancellationToken cancellationToken = default
        );

        public Task<Candy> AddOrUpdateCandyAsync(
            string candyName,
            decimal candyPrice,
            DateTime candyExpirationDate,
            int categoryId,
            int? candyId = null,
            CancellationToken cancellationToken = default
        );

        public Task<bool> DeleteCandyAsync(
            int? candyId = null,
            CancellationToken cancellationToken = default
        );
    }
}
