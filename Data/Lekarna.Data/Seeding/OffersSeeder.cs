namespace Lekarna.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Lekarna.Data.Models;

    public class OffersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Offers.Any())
            {
                return;
            }

            var offers = new List<Offer>()
           {
               new Offer { Name = "Sanofi", Price = 2.55m, Target = 100, Quantity = 500, Discount = 50 },
               new Offer { Name = "Sopharma", Price = 4.55m, Target = 20, Quantity = 300, Discount = 80 },
               new Offer { Name = "Medica", Price = 12.86m, Target = 50, Quantity = 1100, Discount = 220 },
               new Offer { Name = "Bayer", Price = 150.71m, Target = 700, Quantity = 1000, Discount = 300 },
               new Offer { Name = "Zelenka", Price = 10.21m, Target = 40, Quantity = 150, Discount = 15 },
               new Offer { Name = "Panoramna", Price = 18.19m, Target = 80, Quantity = 220, Discount = 25 },
               new Offer { Name = "Buntovnik", Price = 245.19m, Target = 680, Quantity = 375, Discount = 90 },
           };
            await dbContext.Offers.AddRangeAsync(offers);
        }
    }
}
