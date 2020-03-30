namespace Lekarna.Services.Data
{
    using System.Threading.Tasks;

    using Lekarna.Data.Common.Repositories;
    using Lekarna.Data.Models;

    public class PharmaciesService : IPharmaciesService
    {
        private readonly IDeletableEntityRepository<Pharmacy> pharmaciesRepository;

        public PharmaciesService(IDeletableEntityRepository<Pharmacy> pharmaciesRepository)
        {
            this.pharmaciesRepository = pharmaciesRepository;
        }

        public async Task<string> CreateAsync(string name, string country, string address, string imageUrl)
        {
            var pharmacy = new Pharmacy
            {
                Name = name,
                Country = country,
                Address = address,
                ImageUrl = imageUrl,
            };

            await this.pharmaciesRepository.AddAsync(pharmacy);
            await this.pharmaciesRepository.SaveChangesAsync();
            return pharmacy.Id;
        }
    }
}
