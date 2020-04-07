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
                UserId = user.Id,
                Name = inputModel.Name,
                Country = inputModel.Country,
                Address = inputModel.Address,
            };

            // user.Pharmacy = pharmacy;
            // this.usersRepository.Update(user);
            // await this.usersRepository.SaveChangesAsync();
            if (inputModel.NewImage != null)
            {
                var newImage = await this.imagesService.CreateAsync(inputModel.NewImage);
                pharmacy.ImageId = newImage.Id;
            }

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
