namespace Lekarna.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Lekarna.Data.Models;

    public class SuppliersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Suppliers.Any())
            {
                return;
            }

            var suppliers = new List<Supplier>()
           {
               new Supplier { Name = "Sanofi", Country = "French",  Address = "Paris", },
               new Supplier { Name = "Sopharma", Country = "Bulgaria", Address = "Sofia", },
               new Supplier { Name = "Medica", Country = "USA", Address = "DC", },
               new Supplier { Name = "Bayer", Country = "Germany", Address = "Berlin", },
               new Supplier { Name = "Zelenka", Country = "Bulgaria", Address = "Tarnovo", },
               new Supplier { Name = "Pfizer", Country = "USA", Address = "LA",  },
               new Supplier { Name = "Novartis", Country = "Switzerland", Address = "Bern", },
           };
            await dbContext.Suppliers.AddRangeAsync(suppliers);
        }
    }
}
