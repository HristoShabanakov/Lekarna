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
               new Offer { Name = "Sanofi", },
               new Offer { Name = "Sopharma", },
               new Offer { Name = "Medica", },
               new Offer { Name = "Bayer", },
               new Offer { Name = "Zelenka", },
               new Offer { Name = "Panoramna", },
               new Offer { Name = "Buntovnik", },
           };
            await dbContext.Offers.AddRangeAsync(offers);
        }
    }
}
