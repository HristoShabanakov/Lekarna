namespace Lekarna.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Lekarna.Data.Common.Repositories;
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;
    using Lekarna.Web.ViewModels.Offers;

    public class OffersService : IOffersService
    {
        private readonly IDeletableEntityRepository<Offer> offersRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> user;

        public OffersService(
            IDeletableEntityRepository<Offer> offersRepository,
            IDeletableEntityRepository<ApplicationUser> user)
        {
            this.offersRepository = offersRepository;
            this.user = user;
        }

        public async Task<string> CreateAsync(OfferCreateInputModel inputModel, ApplicationUser user)
        {
            var offer = new Offer
            {
                Name = inputModel.Name,
                Medicine = inputModel.Medicine,
                Price = inputModel.Price,
                Target = inputModel.Target,
                Quantity = inputModel.Quantity,
                Discount = inputModel.Discount,
                SupplierId = inputModel.SupplierId,
                UserId = user.Id,
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
