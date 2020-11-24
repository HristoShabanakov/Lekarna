namespace Lekarna.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Lekarna.Services.Data.Common;
    using Microsoft.AspNetCore.Http;

    public interface IOffersService : IService
    {
        Task<string> CreateAsync(string name, string supplierId, string categoryId, DateTime expirationDate, IFormFile formData);

        Task<string> EditAsync(string id, string name, string categoryId, string supplierId, DateTime expirationDate);

        Task<string> DeleteAsync(string id);

        Task<IEnumerable<T>> GetAllAsync<T>(int? count = null);

        Task<T> GetByIdAsync<T>(string id);

        Task<IEnumerable<T>> GetAllOffersAsync<T>(int? take = null, int skip = 0);

        Task<int> GetAllOffersCountAsync();
    }
}
