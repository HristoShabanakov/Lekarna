namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Lekarna.Data.Models;
    using Lekarna.Web.ViewModels.Offers;

    public interface IOffersService
    {
        Task<string> CreateAsync(OfferCreateInputModel inputModel, ApplicationUser user);

        Task<string> EditAsync(OfferEditViewModel inputModel);

        Task<string> DeleteAsync(string id);

        IEnumerable<T> GetAll<T>(int? count = null);

        T GetById<T>(string id);

        IEnumerable<T> GetAllOffers<T>(int? take = null, int skip = 0);

        int GetAllOffersCount();
    }
}
