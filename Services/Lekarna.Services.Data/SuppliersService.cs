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
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public SuppliersService(
            IDeletableEntityRepository<Supplier> suppliersRepository,
            IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.suppliersRepository = suppliersRepository;
            this.categoriesRepository = categoriesRepository;
        }

        public async Task<string> CreateAsync(SupplierCreateInputModel inputModel, ApplicationUser user)
        {
            var supplier = new Supplier
            {
                Name = inputModel.Name,
                Country = inputModel.Country,
                Address = inputModel.Address,
                ImageUrl = inputModel.ImageUrl,
            };

            await this.suppliersRepository.AddAsync(supplier);
            await this.suppliersRepository.SaveChangesAsync();
            return supplier.Id;
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
