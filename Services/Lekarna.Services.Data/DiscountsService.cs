namespace Lekarna.Services.Data
{
    using System.Threading.Tasks;

    using Lekarna.Data.Common.Repositories;
    using Lekarna.Data.Models;
    using Lekarna.Web.ViewModels.Discounts;

    public class DiscountsService : IDiscountsService
    {
        private readonly IDeletableEntityRepository<Discount> discountsRepository;

        public DiscountsService(IDeletableEntityRepository<Discount> discountsRepository)
        {
            this.discountsRepository = discountsRepository;
        }

        public async Task<string> CreateAsync(DiscountViewModel inputModel)
        {
            var discount = new Discount
            {
                Quantity = inputModel.Quantity,
            };

            await this.discountsRepository.AddAsync(discount);
            await this.discountsRepository.SaveChangesAsync();
            return discount.Id;
        }
    }
}
