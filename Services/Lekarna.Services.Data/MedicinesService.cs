namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Lekarna.Data.Common.Repositories;
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;
    using Lekarna.Web.ViewModels.Medicines;

    public class MedicinesService : IMedicinesService
    {
        private readonly IDeletableEntityRepository<Medicine> medicinesRepository;

        public MedicinesService(IDeletableEntityRepository<Medicine> medicinesRepository)
        {
            this.medicinesRepository = medicinesRepository;
        }

        public async Task<string> CreateAsync(MedicineViewModel inputModel)
        {
            var medicine = new Medicine
            {
                Name = inputModel.Name,
                Price = inputModel.Price,
                Discount = inputModel.Discount,
                Target = inputModel.Target,
                OfferId = inputModel.OfferId,
            };

            var dbMedicine = this.medicinesRepository.All().Where(o => o.Name == medicine.Name).FirstOrDefault();

            if (dbMedicine != null)
            {
                return null;
            }

            await this.medicinesRepository.AddAsync(medicine);
            await this.medicinesRepository.SaveChangesAsync();
            return medicine.Id;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Medicine> query = this.medicinesRepository.All().OrderBy(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }
    }
}
