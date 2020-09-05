namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IMedicinesService
    {
        Task<string> CreateAsync(string name, decimal price, string offerId, string targetId, string discountId);

        IEnumerable<T> GetAll<T>(int? count = null);

        Task<IEnumerable<T>> GetAllMedicines<T>(string id);

        Task SaveAllFromFile(IFormFile file, string offerId);

       // Task<IEnumerable<T>> GetSameTargetsId<T>(string id);
    }
}
