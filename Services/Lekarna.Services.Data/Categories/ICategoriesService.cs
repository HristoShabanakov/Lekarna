namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoriesService
    {
        Task<string> CreateAsync(string categoryName, string description);

        Task<string> EditAsync(string id, string name, string description);

        Task<string> DeleteAsync(string id);

        Task<IEnumerable<T>> GetAll<T>(int? count = null);

        Task<T> GetById<T>(string id);

        Task<IEnumerable<T>> GetAllCategories<T>(int? take = null, int skip = 0);

        Task<int> GetAllCategoriesCount();
    }
}
