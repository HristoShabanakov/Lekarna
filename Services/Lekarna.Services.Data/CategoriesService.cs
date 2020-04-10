namespace Lekarna.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Lekarna.Data.Common.Repositories;
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;

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

            await this.categoriesRepository.AddAsync(category);
            await this.categoriesRepository.SaveChangesAsync();
            return category.Id;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Category> query = this.categoriesRepository.All().OrderBy(x => x.CategoryName);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllCategories<T>(int? take = null, int skip = 0)
        {
            var query = this.categoriesRepository.All()
               .OrderByDescending(c => c.CategoryName)
               .Skip(skip);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public int GetAllCategoriesCount()
        {
            return this.categoriesRepository.All().ToList().Count;
        }

        public T GetById<T>(string id)
        {
            var category = this.categoriesRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return category;
        }
    }
}
