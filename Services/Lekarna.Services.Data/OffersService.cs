﻿namespace Lekarna.Services.Data
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

            await this.offersRepository.AddAsync(offer);
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

        public T GetById<T>(string id)
        {
            var offer = this.offersRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return offer;
        }
    }
}
