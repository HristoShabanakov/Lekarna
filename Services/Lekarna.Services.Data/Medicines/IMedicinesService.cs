namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Lekarna.Services.Data.Common;
    using Microsoft.AspNetCore.Http;

    public interface IMedicinesService : IService
    {
        Task<string> CreateAsync(string name, decimal price, string offerId, string targetId, string discountId);

        Task<IEnumerable<T>> GetAll<T>(int? count = null);

        Task<IEnumerable<T>> GetAllMedicines<T>(string id);

        Task SaveAllFromFile(IFormFile file, string offerId);

        Task<IEnumerable<T>> GetSameTargetsId<T>(string id);
    }
}
