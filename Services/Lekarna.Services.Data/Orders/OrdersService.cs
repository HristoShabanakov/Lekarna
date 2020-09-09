namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Lekarna.Data.Common.Repositories;
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class OrdersService : IOrdersService
    {
        private readonly IDeletableEntityRepository<OrderItem> ordersRepository;

        public OrdersService(
            IDeletableEntityRepository<OrderItem> ordersRepository)
        {
            this.ordersRepository = ordersRepository;
        }

        public async Task<string> CreateOrderAsync(string offerId, string medicineId, decimal price, int quantity)
        {
            var order = new OrderItem
            {
               OfferId = offerId,
               MedicineId = medicineId,
               Price = price,
               Quantity = quantity,
            };

            await this.ordersRepository.AddAsync(order);
            await this.ordersRepository.SaveChangesAsync();
            return order.Id;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(int? count = null)
        {
            IQueryable<OrderItem> query = this.ordersRepository.All().OrderBy(x => x.Id);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return await query.To<T>().ToListAsync();
        }
    }
}
