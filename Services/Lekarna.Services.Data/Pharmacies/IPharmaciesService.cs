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

        Task<IEnumerable<T>> GetAll<T>(int? count = null);

        Task<T> GetById<T>(string id);

        Task<IEnumerable<T>> GetAllPharmacies<T>(string userId = null, int? take = null, int skip = 0);

        Task<int> GetAllPharmaciesCount();
    }
}
