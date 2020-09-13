namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Lekarna.Data.Common.Repositories;
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    public class PharmaciesService : IPharmaciesService
    {
        private readonly IDeletableEntityRepository<Pharmacy> pharmaciesRepository;
        private readonly IImagesService imagesService;

        public PharmaciesService(
            IDeletableEntityRepository<Pharmacy> pharmaciesRepository,
            IImagesService imagesService)
        {
            this.pharmaciesRepository = pharmaciesRepository;
            this.imagesService = imagesService;
        }

        public async Task<string> CreateAsync(string name, string country, string address, IFormFile newImage, string userId)
        {
            var dbPharmacy = await this.pharmaciesRepository.All().Where(p => p.Name == name).FirstOrDefaultAsync();

            if (dbPharmacy != null)
            {
                return string.Empty;
            }

            var pharmacy = new Pharmacy
            {
                Name = name,
                Country = country,
                Address = address,
                UserId = userId,
                ImageUrl = await this.imagesService.GetImageUrl(newImage),
            };

            await this.pharmaciesRepository.AddAsync(pharmacy);
            await this.pharmaciesRepository.SaveChangesAsync();

            return pharmacy.Id;
        }

        public async Task<string> DeleteAsync(string id)
        {
            var pharmacy = await this.pharmaciesRepository.All().Where(x => x.Id == id).FirstOrDefaultAsync();

            if (pharmacy == null)
            {
                return null;
            }

            if (pharmacy.ImageUrl != null)
            {
                await this.imagesService.DeleteFromCloudAsync(pharmacy.ImageUrl);
            }

            var pharmacyId = pharmacy.Id;

            this.pharmaciesRepository.Delete(pharmacy);
            await this.pharmaciesRepository.SaveChangesAsync();

            return pharmacyId;
        }

        public async Task<string> EditAsync(string name, string country, string address, IFormFile newImage, string id)
        {
            var pharmacy = await this.pharmaciesRepository.All().FirstOrDefaultAsync(p => p.Id == id);

            if (pharmacy == null)
            {
                return null;
            }

            pharmacy.Name = name;
            pharmacy.Country = country;
            pharmacy.Address = address;
            pharmacy.ImageUrl = await this.imagesService.GetImageUrl(newImage);

            this.pharmaciesRepository.Update(pharmacy);
            await this.pharmaciesRepository.SaveChangesAsync();

            return pharmacy.Id;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(int? count = null)
        {
            IQueryable<Pharmacy> query = this.pharmaciesRepository.All().OrderBy(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return await query.To<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllPharmaciesAsync<T>(string userId = null, int? take = null, int skip = 0)
        {
            var query = this.pharmaciesRepository.All()
                .OrderByDescending(p => p.Name)
                .Skip(skip);

            if (userId != null)
            {
                query = query.Where(x => x.UserId == userId);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return await query.To<T>().ToListAsync();
        }

        public async Task<int> GetAllPharmaciesCountAsync()
        => await this.pharmaciesRepository.All().CountAsync();

        public async Task<T> GetByIdAsync<T>(string id)
        => await this.pharmaciesRepository
            .All()
            .Where(x => x.Id == id)
            .To<T>().FirstOrDefaultAsync();
    }
}
