namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Lekarna.Data.Common.Repositories;
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;
    using Lekarna.Web.ViewModels.Pharmacies;

    public class PharmaciesService : IPharmaciesService
    {
        private readonly IDeletableEntityRepository<Pharmacy> pharmaciesRepository;
        private readonly IImagesService imagesService;
        private readonly IRepository<ApplicationUser> usersRepository;

        public PharmaciesService(
            IDeletableEntityRepository<Pharmacy> pharmaciesRepository,
            IImagesService imagesService,
            IRepository<ApplicationUser> usersRepository)
        {
            this.pharmaciesRepository = pharmaciesRepository;
            this.imagesService = imagesService;
            this.usersRepository = usersRepository;
        }

        public async Task<string> CreateAsync(PharmacyViewModel inputModel, ApplicationUser user)
        {
            var pharmacy = new Pharmacy
            {
                ApplicationUserId = user.Id,
                Name = inputModel.Name,
                Country = inputModel.Country,
                Address = inputModel.Address,
            };

            user.PharmacyId = pharmacy.Id;

            var dbPharmacy = this.pharmaciesRepository.All().Where(p => p.Name == pharmacy.Name).FirstOrDefault();

            if (dbPharmacy != null)
            {
                return null;
            }

            if (inputModel.NewImage != null)
            {
                var newImage = await this.imagesService.CreateAsync(inputModel.NewImage);
                pharmacy.ImageId = newImage.Id;
            }

            await this.pharmaciesRepository.AddAsync(pharmacy);
            await this.pharmaciesRepository.SaveChangesAsync();

            this.usersRepository.Update(user);
            await this.usersRepository.SaveChangesAsync();
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

        public IEnumerable<T> GetAllPharmacies<T>(int? take = null, int skip = 0)
        {
            var query = this.pharmaciesRepository.All()
                .OrderByDescending(p => p.Name)
                .Skip(skip);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public int GetAllPharmaciesCount()
        {
            return this.pharmaciesRepository.All().ToList().Count;
        }

        public T GetById<T>(string id)
        {
            var pharmacy = this.pharmaciesRepository.All().Where(x => x.Id == id)
               .To<T>().FirstOrDefault();

            return pharmacy;
        }
    }
}
