namespace Lekarna.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Lekarna.Data.Common.Repositories;
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;

    public class OffersService : IOffersService
    {
        private readonly IDeletableEntityRepository<Offer> offersRepository;

        public OffersService(IDeletableEntityRepository<Offer> offersRepository)
        {
            this.offersRepository = offersRepository;
        }

        public async Task<string> CreateAsync(string name, string medicine, decimal price, int target, int quantity, decimal discount, string supplierId, string userId)
        {
            var offer = new Offer
            {
                Name = name,
                Medicine = medicine,
                Price = price,
                Target = target,
                Quantity = quantity,
                Discount = discount,
                SupplierId = supplierId,
                UserId = userId,
            };

            await this.offersRepository.AddAsync(offer);
            await this.offersRepository.SaveChangesAsync();
            return offer.Id;
        }

        public T GetById<T>(string id)
        {
            var offer = this.offersRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return offer;
        }
    }
}
