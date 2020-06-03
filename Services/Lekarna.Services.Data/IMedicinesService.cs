namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Lekarna.Web.ViewModels.Medicines;

    public interface IMedicinesService
    {
        Task<string> CreateAsync(MedicineViewModel inputModel);

        IEnumerable<T> GetAll<T>(int? count = null);

        Task<IEnumerable<T>> GetAllMedicines<T>(string id);

        Task<IEnumerable<T>> GetSameTargetsId<T>(string id);
    }
}
