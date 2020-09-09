namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOrdersService
    {
        Task<string> CreateOrderAsync(string offerId, string medicineId, decimal price, int quantity);

        Task<IEnumerable<T>> GetAllAsync<T>(int? count = null);
    }
}
