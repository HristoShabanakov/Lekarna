namespace Lekarna.Services.Data
{
    using System.Threading.Tasks;

    using Lekarna.Data.Common.Repositories;
    using Lekarna.Data.Models;

    public class DiscountsService : IDiscountsService
    {
        private readonly IDeletableEntityRepository<Discount> discountsRepository;

        public DiscountsService(IDeletableEntityRepository<Discount> discountsRepository)
        {
            this.discountsRepository = discountsRepository;
        }

        public async Task<string> CreateAsync(decimal value)
        {
            var discount = new Discount
            {
                Quantity = value,
            };

            await this.discountsRepository.AddAsync(discount);
            await this.discountsRepository.SaveChangesAsync();
            return discount.Id;
        }
    }
}
