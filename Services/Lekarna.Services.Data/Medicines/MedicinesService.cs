namespace Lekarna.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Lekarna.Data.Common.Repositories;
    using Lekarna.Data.Models;
    using Lekarna.Services.Mapping;
    using Lekarna.Services.Models.Medicines;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    using static Lekarna.Common.GlobalConstants.Offer;

    public class MedicinesService : IMedicinesService
    {
        private readonly IDeletableEntityRepository<Medicine> medicinesRepository;
        private readonly ITargetsService targetsService;
        private readonly IDiscountsService discountsService;

        public MedicinesService(
            IDeletableEntityRepository<Medicine> medicinesRepository,
            ITargetsService targetsService,
            IDiscountsService discountsService)
        {
            this.medicinesRepository = medicinesRepository;
            this.targetsService = targetsService;
            this.discountsService = discountsService;
        }

        public async Task<string> CreateAsync(string name, decimal price, string offerId, string targetId, string discountId)
        {
            var medicine = new Medicine
            {
                Name = name,
                Price = price,
                OfferId = offerId,
                TargetId = targetId,
                DiscountId = discountId,
            };

            await this.medicinesRepository.AddAsync(medicine);
            await this.medicinesRepository.SaveChangesAsync();
            return medicine.Id;
        }

        public async Task<IEnumerable<T>> GetAll<T>(int? count = null)
        {
            IQueryable<Medicine> query = this.medicinesRepository.All().OrderBy(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return await query.To<T>().ToListAsync();
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
             var commonTarget = await this.medicinesRepository.AllAsNoTracking()
              .Select(x => new { x.Name, x.Id, Count = this.medicinesRepository.AllAsNoTracking().Count(y => y.TargetId == x.TargetId) })
              .Where(x => x.Count > 1)
              .To<T>()
              .ToListAsync();

             return commonTarget;
        }

        public async Task SaveAllFromFile(IFormFile file, string offerId)
        {
            var medicinesDbRecords = new List<MedicinesServiceModel>();
            using var reader = new StreamReader(file.OpenReadStream());

            string[] headers = reader.ReadLine()
                .Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            var textReader = reader.ReadToEnd()
                .Replace(" лв.", string.Empty)
                .Replace("%", string.Empty)
                .Replace("\"", string.Empty)
                .Trim();

            var rows = textReader.Split("\r\n");

            for (int i = 0; i < rows.Length; i++)
            {
                var cols = rows[i].Split(";");
                string name = cols[0];
                string price = cols[1];
                string target = cols[2];
                string discount = cols[3];
                int targetValue = 0;
                decimal discountValue = 0;

                if (name.Any() && price.Any() && target.Length == 0 && discount.Any())
                {
                    discountValue = decimal.Parse(cols[3]);

                    var discountIdDb = await this.discountsService.CreateAsync(discountValue);

                    medicinesDbRecords.Add(new MedicinesServiceModel
                    {
                        Name = cols[0],
                        Price = decimal.Parse(cols[1]),
                        DiscountId = discountIdDb,
                        OfferId = offerId,
                    });
                }

                if (name.Any() && price.Any() && target.Any() && discount.Length == 0)
                {
                    targetValue = int.Parse(cols[2]);

                    var idTarget = await this.targetsService.CreateAsync(targetValue);

                    medicinesDbRecords.Add(new MedicinesServiceModel
                    {
                        Name = cols[0],
                        Price = decimal.Parse(cols[1]),
                        TargetId = idTarget,
                        OfferId = offerId,
                    });
                }

                if (name.Contains(Formula) && discount.Any())
                {
                    discountValue = decimal.Parse(cols[3]);

                    var discountIdDb = await this.discountsService.CreateAsync(discountValue);

                    for (int j = 0; j < medicinesDbRecords.Count; j++)
                    {
                        medicinesDbRecords[j].DiscountId = discountIdDb;
                        var medicines = medicinesDbRecords[j];
                        await this.CreateAsync(medicines.Name, medicines.Price, medicines.OfferId, medicines.TargetId, medicines.DiscountId);
                    }

                    medicinesDbRecords.Clear();
                }

                if (name.Contains(Total) && target.Any())
                {
                    targetValue = int.Parse(cols[2]);

                    var idTarget = await this.targetsService.CreateAsync(targetValue);

                    for (int index = 0; index < medicinesDbRecords.Count; index++)
                    {
                        medicinesDbRecords[index].TargetId = idTarget;

                        var medicines = medicinesDbRecords[index];
                        await this.CreateAsync(medicines.Name, medicines.Price, medicines.OfferId, medicines.TargetId, medicines.DiscountId);
                    }

                    medicinesDbRecords.Clear();
                }

                if (cols.Contains(string.Empty))
                {
                    continue;
                }

                targetValue = int.Parse(cols[2]);
                var targetId = await this.targetsService.CreateAsync(targetValue);

                discountValue = decimal.Parse(cols[3]);
                var discountId = await this.discountsService.CreateAsync(discountValue);

                var medicineRecord = new MedicinesServiceModel
                {
                    Name = cols[0].ToString(),
                    Price = decimal.Parse(cols[1]),
                    OfferId = offerId,
                    TargetId = targetId,
                    DiscountId = discountId,
                };

                await this.CreateAsync(
                    medicineRecord.Name,
                    medicineRecord.Price,
                    medicineRecord.OfferId,
                    medicineRecord.TargetId,
                    medicineRecord.DiscountId);
            }
        }
    }
}
