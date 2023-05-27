using SweetCandy.Core.Entities;
using SweetCandy.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetCandy.Data.Seeders
{
    public class DataSeeder
    {
        private readonly SweetCandyContext _dbContext;

        public DataSeeder(SweetCandyContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Initialize()
        {
            _dbContext.Database.EnsureCreated();

            if (_dbContext.Candies.Any()) return;

            IList<Category> categories = AddCategories();
            IList<Candy> candies = AddCandies(categories);
        }

        private IList<Category> AddCategories()
        {
            List<Category> categories = new()
            {
                new() { Name = "Sweet Candy", ShowOnMenu = true },
                new() { Name = "Chocolate", ShowOnMenu = true },
                new() { Name = "Egg Milk" },
            };

            _dbContext.Categories.AddRange(categories);
            _dbContext.SaveChanges();

            return categories;
        }

        private IList<Candy> AddCandies(IList<Category> categories)
        {
            List<Candy> candies = new()
            {
                new() { Name = "Coacocreamy Taffy", Price = 19.2M, ExpirationDate = DateTime.Parse("25/05/2024"), Category = categories[0] },
                new() { Name = "Fudgy Gooey Fudge", Price = 12.0M, ExpirationDate = DateTime.Parse("20/05/2023"), Category = categories[1] },
                new() { Name = "Raspberry Fudge", Price = 15.5M, ExpirationDate = DateTime.Parse("19/04/2023"), Category = categories[2] },
                new() { Name = "Lime Mochachocolates", Price = 9.8M, ExpirationDate = DateTime.Parse("05/01/2024"), Category = categories[1] },
                new() { Name = "Peanut Nutty Spinners", Price = 6.5M, ExpirationDate = DateTime.Parse("15/03/2022"), Category = categories[2] },
                new() { Name = "Marshmallow Mochanuts", Price = 14.6M, ExpirationDate = DateTime.Parse("27/05/2023"), Category = categories[1] }
            };

            _dbContext.Candies.AddRange(candies);
            _dbContext.SaveChanges();

            return candies;
        }
    }
}
