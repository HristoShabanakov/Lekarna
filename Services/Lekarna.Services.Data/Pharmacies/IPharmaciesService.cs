namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Lekarna.Web.ViewModels.Pharmacies;

    public interface IPharmaciesService
    {
        Task<string> CreateAsync(PharmacyViewModel inputModel, string userId);

        Task<string> EditAsync(PharmacyEditViewModel inputModel);

        Task<string> DeleteAsync(string id);

        IEnumerable<T> GetAll<T>(int? count = null);

        Task<T> GetById<T>(string id);

        Task<IEnumerable<T>> GetAllPharmacies<T>(string userId = null, int? take = null, int skip = 0);

        int GetAllPharmaciesCount();
    }
}
