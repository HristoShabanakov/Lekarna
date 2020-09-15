namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Lekarna.Services.Data.Common;

    public interface IOrdersService : IService
    {
        Task<string> CreateOrderAsync(string pharmacyId, string offerId);

        Task<string> AddToOrderAsync(string orderId, string medicineId, int quantity);

        Task<IEnumerable<T>> GetAllAsync<T>(int? count = null);
    }
}
