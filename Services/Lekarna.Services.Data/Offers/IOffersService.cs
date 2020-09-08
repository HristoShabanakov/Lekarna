namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IOffersService
    {
        Task<string> CreateAsync(string name, string supplierId, string categoryId, IFormFile formData);

        Task<string> EditAsync(string id, string name, string categoryId, string supplierId);

        Task<string> DeleteAsync(string id);

        Task<IEnumerable<T>> GetAll<T>(int? count = null);

        Task<T> GetById<T>(string id);

        Task<IEnumerable<T>> GetAllOffers<T>(int? take = null, int skip = 0);

        Task<int> GetAllOffersCount();
    }
}
