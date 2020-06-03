﻿namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Lekarna.Data.Common.Repositories;
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;
    using Lekarna.Web.ViewModels.Medicines;
    using Microsoft.EntityFrameworkCore;

    public class MedicinesService : IMedicinesService
    {
        private readonly IDeletableEntityRepository<Medicine> medicinesRepository;

        public MedicinesService(IDeletableEntityRepository<Medicine> medicinesRepository)
        {
            this.medicinesRepository = medicinesRepository;
        }

        public async Task<string> CreateAsync(MedicineViewModel medicineModel)
        {
            var medicine = new Medicine
            {
                Name = medicineModel.Name,
                Price = medicineModel.Price,
                OfferId = medicineModel.OfferId,
                TargetId = medicineModel.TargetId,
                DiscountId = medicineModel.DiscountId,
            };

            // var dbMedicine = this.medicinesRepository.All().Where(o => o.Name == medicine.Name).FirstOrDefault();

            // if (dbMedicine != null)
            // {
            //    return null;
            // }
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

        public async Task<IEnumerable<T>> GetAllMedicines<T>(string id)
        {
            var medicines = await this.medicinesRepository.All()
                .Where(x => x.OfferId == id)
                .OrderBy(m => m.Name)
                .To<T>()
                .ToListAsync();

            return medicines;
        }

        public async Task<IEnumerable<T>> GetSameTargetsId<T>(string id)
        {
            var medicineTargetId = await this.medicinesRepository.All()
                .Where(x => x.OfferId == id)
                .GroupBy(t => t.TargetId).To<T>()
                .ToListAsync();
            return medicineTargetId;
        }
    }
}
