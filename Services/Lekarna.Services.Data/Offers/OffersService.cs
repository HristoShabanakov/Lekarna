namespace Lekarna.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Lekarna.Data.Common.Repositories;
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;
    using Microsoft.AspNetCore.Http;
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

        public async Task<string> CreateAsync(string name, string supplierId, string categoryId, DateTime expirationDate, IFormFile formData)
        {
            var offer = new Offer
            {
                Name = name,
                SupplierId = supplierId,
                CategoryId = categoryId,
                ExpirationDate = expirationDate.ToUniversalTime(),
            };

            await this.offersRepository.AddAsync(offer);
            await this.offersRepository.SaveChangesAsync();

            await this.medicinesService.SaveAllFromFile(formData, offer.Id);

            return offer.Id;
        }

        public async Task<string> DeleteAsync(string id)
        {
            var offer = await this.offersRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (offer == null)
            {
                return null;
            }

            var offerId = offer.Id;

            this.offersRepository.Delete(offer);
            await this.offersRepository.SaveChangesAsync();

            return offerId;
        }

        public async Task<string> EditAsync(string id, string name, string categoryId, string supplierId, DateTime expirationDate)
        {
            var offer = await this.offersRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id);

            if (offer == null)
            {
                return null;
            }

            offer.Name = name;
            offer.CategoryId = categoryId;
            offer.SupplierId = supplierId;
            offer.ExpirationDate = expirationDate.ToUniversalTime();

            this.offersRepository.Update(offer);
            await this.offersRepository.SaveChangesAsync();

            return offer.Id;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(int? count = null)
        {
            IQueryable<Offer> query = this.offersRepository.All().OrderBy(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return await query.To<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllOffersAsync<T>(int? take = null, int skip = 0)
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

        public async Task<int> GetAllOffersCountAsync()
        => await this.offersRepository.All().CountAsync();

        public async Task<T> GetByIdAsync<T>(string id)
        => await this.offersRepository
                 .All()
                 .Where(x => x.Id == id)
                 .To<T>()
                 .FirstOrDefaultAsync();
    }
}
