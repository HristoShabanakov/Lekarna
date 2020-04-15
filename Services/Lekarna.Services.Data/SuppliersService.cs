namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Lekarna.Data.Common.Repositories;
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;
    using Lekarna.Web.ViewModels.Suppliers;

    public class SuppliersService : ISuppliersService
    {
        private readonly IDeletableEntityRepository<Supplier> suppliersRepository;
        private readonly IImagesService imagesService;

        public SuppliersService(
            IDeletableEntityRepository<Supplier> suppliersRepository,
            IImagesService imagesService)
        {
            this.suppliersRepository = suppliersRepository;
            this.imagesService = imagesService;
        }

        public async Task<string> CreateAsync(SupplierCreateViewModel inputModel, ApplicationUser user)
        {
            var supplier = new Supplier
            {
                UserId = user.Id,
                Name = inputModel.Name,
                Country = inputModel.Country,
                Address = inputModel.Address,
            };

            var dbSupplier = this.suppliersRepository.All().Where(s => s.Name == supplier.Name).FirstOrDefault();

            if (dbSupplier != null)
            {
                return null;
            }

            if (inputModel.NewImage != null)
            {
                var newImage = await this.imagesService.CreateAsync(inputModel.NewImage);
                supplier.ImageId = newImage.Id;
            }

            await this.suppliersRepository.AddAsync(supplier);
            await this.suppliersRepository.SaveChangesAsync();
            return supplier.Id;
        }

        public async Task<string> EditAsync(SupplierEditViewModel inputModel)
        {
            var supplier = this.suppliersRepository.All().FirstOrDefault(p => p.Id == inputModel.Id);

            if (supplier == null)
            {
                return null;
            }

            supplier.Name = inputModel.Name;
            supplier.Country = inputModel.Country;
            supplier.Address = inputModel.Address;

            if (inputModel.NewImage != null)
            {
                var newImage = await this.imagesService.CreateAsync(inputModel.NewImage);
                supplier.ImageId = newImage.Id;
            }

            this.suppliersRepository.Update(supplier);
            await this.suppliersRepository.SaveChangesAsync();

            return supplier.Id;
        }

        public async Task<string> DeleteAsync(string id)
        {
            var supplier = this.suppliersRepository.All().Where(x => x.Id == id).FirstOrDefault();

            if (supplier == null)
            {
                return null;
            }

            var supplierId = supplier.Id;

            this.suppliersRepository.Delete(supplier);
            await this.suppliersRepository.SaveChangesAsync();

            return supplierId;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Supplier> query = this.suppliersRepository.All().OrderBy(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllSuppliers<T>(int? take = null, int skip = 0)
        {
            var query = this.suppliersRepository.All()
                .OrderByDescending(s => s.CreatedOn)
                .Skip(skip);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public int GetAllSuppliersCount()
        {
             return this.suppliersRepository.All().ToList().Count;
        }

        public T GetById<T>(string id)
        {
            var supplier = this.suppliersRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return supplier;
        }

        public T GetByName<T>(string name)
        {
            var supplier = this.suppliersRepository.All().Where(x => x.Name == name)
                .To<T>().FirstOrDefault();

            return supplier;
        }
    }
}
