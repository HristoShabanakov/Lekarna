namespace Lekarna.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IOffersService
    {
        Task<string> CreateAsync(string name, string medicine, decimal price, int target, int quantity, decimal discount, string supplierId, string userId);

        T GetById<T>(string id);
    }
}
