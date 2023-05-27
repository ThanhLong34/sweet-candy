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
            string categoryName,
            bool showOnMenu = false,
            int? categoryId = null,
            CancellationToken cancellationToken = default
        );

        public Task<bool> DeleteCandyAsync(
            int? categoryId = null,
            CancellationToken cancellationToken = default
        );
    }
}
