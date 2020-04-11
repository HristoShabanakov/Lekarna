namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Lekarna.Data.Models;
    using Lekarna.Web.ViewModels.Pharmacies;

    public interface IPharmaciesService
    {
        Task<string> CreateAsync(PharmacyViewModel inputModel, ApplicationUser user);

        Task<string> EditAsync(PharmacyViewModel inputModel);

        IEnumerable<T> GetAll<T>(int? count = null);

        T GetById<T>(string id);

        IEnumerable<T> GetAllPharmacies<T>(int? take = null, int skip = 0);

        int GetAllPharmaciesCount();
    }
}
