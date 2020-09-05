namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Lekarna.Data.Common.Repositories;
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;
    using Lekarna.Web.ViewModels.Pharmacies;
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

        public async Task<string> CreateAsync(PharmacyViewModel inputModel, string userId)
        {
            var dbPharmacy = this.pharmaciesRepository.All().Where(p => p.Name == inputModel.Name).FirstOrDefault();

            if (dbPharmacy != null)
            {
                return string.Empty;
            }

            var pharmacy = new Pharmacy
            {
                Name = inputModel.Name,
                Country = inputModel.Country,
                Address = inputModel.Address,
                UserId = userId,
            };

            if (inputModel.NewImage != null)
            {
                var newImage = await this.imagesService.CreateAsync(inputModel.NewImage);
                pharmacy.ImageId = newImage.Id;
            }

            await this.pharmaciesRepository.AddAsync(pharmacy);
            await this.pharmaciesRepository.SaveChangesAsync();

            return pharmacy.Id;
        }

        public async Task<string> DeleteAsync(string id)
        {
            var pharmacy = this.pharmaciesRepository.All().Where(x => x.Id == id).FirstOrDefault();

            if (pharmacy == null)
            {
                return null;
            }

            var pharmacyId = pharmacy.Id;

            this.pharmaciesRepository.Delete(pharmacy);
            await this.pharmaciesRepository.SaveChangesAsync();

            return pharmacyId;
        }

        public async Task<string> EditAsync(PharmacyEditViewModel inputModel)
        {
            var pharmacy = this.pharmaciesRepository.All().FirstOrDefault(p => p.Id == inputModel.Id);

            if (pharmacy == null)
            {
                return null;
            }

            pharmacy.Name = inputModel.Name;
            pharmacy.Country = inputModel.Country;
            pharmacy.Address = inputModel.Address;

            if (inputModel.NewImage != null)
            {
                var newImage = await this.imagesService.CreateAsync(inputModel.NewImage);
                pharmacy.ImageId = newImage.Id;
            }

            this.pharmaciesRepository.Update(pharmacy);
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

        public async Task<IEnumerable<T>> GetAllPharmacies<T>(string userId = null, int? take = null, int skip = 0)
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

        public int GetAllPharmaciesCount()
        {
            return this.pharmaciesRepository.All().ToList().Count;
        }

        public async Task<T> GetById<T>(string id)
        => await this.pharmaciesRepository
            .All()
            .Where(x => x.Id == id)
            .To<T>().FirstOrDefaultAsync();
    }
}
