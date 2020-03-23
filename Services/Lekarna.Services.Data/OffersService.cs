namespace Lekarna.Services.Data
{
    using System.Threading.Tasks;

    using Lekarna.Data.Common.Repositories;
    using Lekarna.Data.Models;

    public class OffersService : IOffersService
    {
        private readonly IDeletableEntityRepository<Offer> offersRepository;

        public OffersService(IDeletableEntityRepository<Offer> offersRepository)
        {
            this.offersRepository = offersRepository;
        }

        public async Task<string> CreateAsync(string name, string medicine, decimal price, string supplierId, string userId)
        {
            var offer = new Offer
            {
                Name = name,
                Medicine = medicine,
                Price = price,
                SupplierId = supplierId,
                UserId = userId,
            };

            await this.offersRepository.AddAsync(offer);
            await this.offersRepository.SaveChangesAsync();
            return offer.Id;
        }
    }
}
