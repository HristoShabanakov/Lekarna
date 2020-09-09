namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Lekarna.Services.Data.Common;

    public interface ICategoriesService : IService
    {
        Task<string> CreateAsync(string categoryName, string description);

        Task<string> EditAsync(string id, string name, string description);

        Task<string> DeleteAsync(string id);

        Task<IEnumerable<T>> GetAllAsync<T>(int? count = null);

        Task<T> GetByIdAsync<T>(string id);

        Task<IEnumerable<T>> GetAllCategoriesAsync<T>(int? take = null, int skip = 0);

        Task<int> GetAllCategoriesCountAsync();
    }
}
