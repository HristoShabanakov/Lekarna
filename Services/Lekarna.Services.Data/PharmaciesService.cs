namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Lekarna.Data.Common.Repositories;
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;

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

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Pharmacy> query = this.pharmaciesRepository.All().OrderBy(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            var pharmacy = this.pharmaciesRepository.All().Where(x => x.Id == id)
               .To<T>().FirstOrDefault();

            return pharmacy;
        }
    }
}
