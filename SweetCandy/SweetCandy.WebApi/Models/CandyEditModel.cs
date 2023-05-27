using SweetCandy.Core.Entities;

namespace SweetCandy.WebApi.Models
{
    public class CandyEditModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int CategoryId { get; set; }
    }
}
