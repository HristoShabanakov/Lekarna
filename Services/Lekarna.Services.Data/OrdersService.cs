namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Lekarna.Data.Common.Repositories;
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;
    using Lekarna.Web.ViewModels.Orders;

    public class OrdersService : IOrdersService
    {
        private readonly IDeletableEntityRepository<OrderItem> ordersRepository;

        public OrdersService(
            IDeletableEntityRepository<OrderItem> ordersRepository)
        {
            this.ordersRepository = ordersRepository;
        }

        public async Task<string> CreateOrder(OrderCreateViewModel inputModel, ApplicationUser user)
        {
            var order = new OrderItem
            {
               OfferId = inputModel.OfferId,
               MedicineId = inputModel.MedicineId,
               Price = inputModel.Price,
               Quantity = inputModel.Quantity,
            };

            await this.ordersRepository.AddAsync(order);
            await this.ordersRepository.SaveChangesAsync();
            return order.Id;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<OrderItem> query = this.ordersRepository.All().OrderBy(x => x.Id);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }
    }
}
