namespace Lekarna.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Lekarna.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var categories = new List<Category>()
            {
                 new Category { CategoryName = "Pain", Description = "Open Package for Pain and Temperature" },
                 new Category { CategoryName = "Allergies", Description = "Allergies Package" },
                 new Category { CategoryName = "Syrups", Description = "Various Syrups" },
                 new Category { CategoryName = "Painkillers", Description = "Different type of painkillers" },
                 new Category { CategoryName = "Vitamins", Description = "Vitamins for kids and adults" },
            };
            await dbContext.Categories.AddRangeAsync(categories);
        }
    }
}
