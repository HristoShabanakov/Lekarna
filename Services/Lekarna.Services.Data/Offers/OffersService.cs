﻿namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Lekarna.Data.Common.Repositories;
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;
    using Lekarna.Web.ViewModels.Offers;
    using Microsoft.EntityFrameworkCore;

    public class OffersService : IOffersService
    {
        private readonly IDeletableEntityRepository<Offer> offersRepository;
        private readonly IMedicinesService medicinesService;

        public OffersService(
            IDeletableEntityRepository<Offer> offersRepository,
            IMedicinesService medicinesService)
        {
            this.offersRepository = offersRepository;
            this.medicinesService = medicinesService;
        }

        public async Task<string> CreateAsync(OfferCreateInputModel inputModel)
        {
            var offer = new Offer
            {
                Name = inputModel.Name,
                SupplierId = inputModel.SupplierId,
                CategoryId = inputModel.CategoryId,
            };

            await this.offersRepository.AddAsync(offer);
            await this.offersRepository.SaveChangesAsync();

            await this.medicinesService.SaveAllFromFile(inputModel.Data, offer.Id);

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
            offer.CategoryId = inputModel.CategoryId;
            offer.SupplierId = inputModel.SupplierId;

            this.offersRepository.Update(offer);
            await this.offersRepository.SaveChangesAsync();

            return offer.Id;
        }

        public async Task<IEnumerable<T>> GetAll<T>(int? count = null)
        {
            IQueryable<Offer> query = this.offersRepository.All().OrderBy(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return await query.To<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllOffers<T>(int? take = null, int skip = 0)
        {
            var query = this.offersRepository.All()
                .OrderByDescending(s => s.CreatedOn)
                .Skip(skip);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return await query.To<T>().ToListAsync();
        }

        public async Task<int> GetAllOffersCount()
        => await this.offersRepository.All().CountAsync();

        public async Task<T> GetById<T>(string id)
        => await this.offersRepository
                 .All()
                 .Where(x => x.Id == id)
                 .To<T>()
                 .FirstOrDefaultAsync();
    }
}