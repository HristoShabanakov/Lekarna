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

        public async Task<string> CreateAsync(string name, string country, string address, IFormFile newImage)
        {
            var supplier = new Supplier
            {
                Name = name,
                Country = country,
                Address = address,
            };

            var dbSupplier = await this.suppliersRepository
                .All()
                .Where(s => s.Name == supplier.Name)
                .FirstOrDefaultAsync();

            if (dbSupplier != null)
            {
                return null;
            }

            if (newImage != null)
            {
                var newPictureImage = await this.imagesService.CreateAsync(newImage);
                supplier.ImageId = newPictureImage.Id;
            }

            await this.suppliersRepository.AddAsync(supplier);
            await this.suppliersRepository.SaveChangesAsync();
            return supplier.Id;
        }

        public async Task<string> EditAsync(string name, string country, string address, IFormFile newImage, string id)
        {
            var supplier = await this.suppliersRepository
                .All()
                .FirstOrDefaultAsync(s => s.Id == id);

            if (supplier == null)
            {
                return null;
            }

            supplier.Name = name;
            supplier.Country = country;
            supplier.Address = address;

            if (newImage != null)
            {
                var newPictureImage = await this.imagesService.CreateAsync(newImage);
                supplier.ImageId = newPictureImage.Id;
            }

            this.suppliersRepository.Update(supplier);
            await this.suppliersRepository.SaveChangesAsync();

            return supplier.Id;
        }

        public async Task<string> DeleteAsync(string id)
        {
            var supplier = await this.suppliersRepository
                .All()
                .FirstOrDefaultAsync(s => s.Id == id);

            if (supplier == null)
            {
                return null;
            }

            var supplierId = supplier.Id;

            this.suppliersRepository.Delete(supplier);
            await this.suppliersRepository.SaveChangesAsync();

            return supplierId;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(int? count = null)
        {
            IQueryable<Supplier> query = this.suppliersRepository.All().OrderBy(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return await query.To<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllSuppliersAsync<T>(int? take = null, int skip = 0)
        {
            var query = this.suppliersRepository
                .All()
                .OrderByDescending(s => s.CreatedOn)
                .Skip(skip);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return await query.To<T>().ToListAsync();
        }

        public async Task<int> GetAllSuppliersCountAsync()
        => await this.suppliersRepository.All().CountAsync();

        public async Task<T> GetByIdAsync<T>(string id)
        => await this.suppliersRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

        public async Task<T> GetByNameAsync<T>(string name)
        => await this.suppliersRepository
                .All()
                .Where(x => x.Name == name)
                .To<T>()
                .FirstOrDefaultAsync();
    }
}
