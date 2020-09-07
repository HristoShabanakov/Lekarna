namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Lekarna.Data.Common.Repositories;
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public async Task<string> CreateAsync(string categoryName, string description)
        {
            var category = new Category
            {
                CategoryName = categoryName,
                Description = description,
            };

            var dbCategory = await this.categoriesRepository
                .All()
                .FirstOrDefaultAsync(c => c.CategoryName == category.CategoryName);

            if (dbCategory != null)
            {
                return null;
            }

            await this.categoriesRepository.AddAsync(category);
            await this.categoriesRepository.SaveChangesAsync();
            return category.Id;
        }

        public async Task<string> DeleteAsync(string id)
        {
            var category = await this.categoriesRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
            {
                return null;
            }

            var categoryId = category.Id;

            this.categoriesRepository.Delete(category);
            await this.categoriesRepository.SaveChangesAsync();

            return categoryId;
        }

        public async Task<string> EditAsync(string id, string name, string description)
        {
            var category = await this.categoriesRepository
                .All().
                FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return null;
            }

            category.CategoryName = name;
            category.Description = description;

            this.categoriesRepository.Update(category);
            await this.categoriesRepository.SaveChangesAsync();

            return category.Id;
        }

        public async Task<IEnumerable<T>> GetAll<T>(int? count = null)
        {
            IQueryable<Category> query = this.categoriesRepository.All().OrderBy(x => x.CategoryName);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return await query.To<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllCategories<T>(int? take = null, int skip = 0)
        {
            var query = this.categoriesRepository.All()
               .OrderByDescending(c => c.CategoryName)
               .Skip(skip);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return await query.To<T>().ToListAsync();
        }

        public async Task<int> GetAllCategoriesCount()
        => await this.categoriesRepository.All().CountAsync();

        public async Task<T> GetById<T>(string id)
        => await this.categoriesRepository
            .All()
            .Where(x => x.Id == id)
            .To<T>()
            .FirstOrDefaultAsync();
    }
}
