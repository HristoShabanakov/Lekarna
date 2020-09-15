namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Lekarna.Data.Common.Enumerations;
    using Lekarna.Data.Common.Repositories;
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class OrdersService : IOrdersService
    {
        private readonly IDeletableEntityRepository<Order> ordersRepository;

        public OrdersService(
            IDeletableEntityRepository<Order> ordersRepository)
        {
            this.ordersRepository = ordersRepository;
        }

        public async Task<string> CreateOrderAsync(string pharmacyId, string offerId)
        {
            var order = new Order
            {
               PharmacyId = pharmacyId,
               OfferId = offerId,
               Status = Status.Active,
            };

            await this.ordersRepository.AddAsync(order);
            await this.ordersRepository.SaveChangesAsync();
            return order.Id;
        }

        public async Task<string> AddToOrderAsync(string orderId, string medicineId, int quantity)
        {
            var order = await this.ordersRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == orderId);

            if (order == null)
            {
                return null;
            }

            var orderItem = new OrderItem
            {
                OrderId = orderId,
                MedicineId = medicineId,
                Quantity = quantity,
            };

            order.OrdersItems.Add(orderItem);
            this.ordersRepository.Update(order);
            await this.ordersRepository.SaveChangesAsync();
            return order.Id;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(int? count = null)
        {
            IQueryable<Order> query = this.ordersRepository.All();

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return await query.To<T>().ToListAsync();
        }
    }
}
