namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Lekarna.Services.Data.Common;

    public interface IOrdersService : IService
    {
        Task<string> CreateOrderAsync(string offerId, string medicineId, decimal price, int quantity);

        Task<IEnumerable<T>> GetAllAsync<T>(int? count = null);
    }
}
