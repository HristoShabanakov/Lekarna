namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
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
                CategoryId = inputModel.CategoryId,
                UserId = user.Id,
            };

            // var dbOffer = this.offersRepository.All().Where(o => o.Name == offer.Name).FirstOrDefault();

            // if (dbOffer.Name == offer.Name)
            // {
            //    return null;
            // }
            await this.offersRepository.AddAsync(offer);
            await this.offersRepository.SaveChangesAsync();
            return offer.Id;
        }

        public async Task<string> DeleteAsync(string id)
        {
            var offer = this.offersRepository.All().Where(x => x.Id == id).FirstOrDefault();

            if (offer == null)
            {
                return null;
            }

            var offerId = offer.Id;

            this.offersRepository.Delete(offer);
            await this.offersRepository.SaveChangesAsync();

            return offerId;
        }

        public async Task<string> EditAsync(OfferEditViewModel inputModel)
        {
            var offer = this.offersRepository.All().FirstOrDefault(o => o.Id == inputModel.Id);

            if (offer == null)
            {
                return null;
            }

            offer.Name = inputModel.Name;
            offer.Medicine = inputModel.Medicine;
            offer.Price = inputModel.Price;
            offer.Target = inputModel.Target;
            offer.Quantity = inputModel.Quantity;
            offer.Discount = inputModel.Discount;
            offer.CategoryId = inputModel.CategoryId;
            offer.SupplierId = inputModel.SupplierId;

            this.offersRepository.Update(offer);
            await this.offersRepository.SaveChangesAsync();

            return offer.Id;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Offer> query = this.offersRepository.All().OrderBy(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllOffers<T>(int? take = null, int skip = 0)
        {
            var query = this.offersRepository.All()
                .OrderByDescending(s => s.CreatedOn)
                .Skip(skip);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public int GetAllOffersCount()
        {
            return this.offersRepository.All().ToList().Count;
        }

        public T GetById<T>(string id)
        {
            var offer = this.offersRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return offer;
        }
    }
}
