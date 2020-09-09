namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IPharmaciesService
    {
        Task<string> CreateAsync(string name, string country, string address, IFormFile newImage, string userId);

        Task<string> EditAsync(string name, string country, string address, IFormFile newImage, string id);

        Task<string> DeleteAsync(string id);

        Task<IEnumerable<T>> GetAllAsync<T>(int? count = null);

        Task<T> GetByIdAsync<T>(string id);

        Task<IEnumerable<T>> GetAllPharmaciesAsync<T>(string userId = null, int? take = null, int skip = 0);

        Task<int> GetAllPharmaciesCountAsync();
    }
}
