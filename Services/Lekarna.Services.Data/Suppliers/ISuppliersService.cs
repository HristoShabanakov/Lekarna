namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface ISuppliersService
    {
        Task<IEnumerable<T>> GetAllAsync<T>(int? count = null);

        Task<string> CreateAsync(string name, string country, string address, IFormFile newImage);

        Task<string> EditAsync(string name, string country, string address, IFormFile newImage, string id);

        Task<string> DeleteAsync(string id);

        Task<T> GetByNameAsync<T>(string name);

        Task<T> GetByIdAsync<T>(string id);

        Task<IEnumerable<T>> GetAllSuppliersAsync<T>(int? take = null, int skip = 0);

        Task<int> GetAllSuppliersCountAsync();
    }
}
