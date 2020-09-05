namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Lekarna.Web.ViewModels.Offers;

    public interface IOffersService
    {
        Task<string> CreateAsync(OfferCreateInputModel inputModel);

        Task<string> EditAsync(OfferEditViewModel inputModel);

        Task<string> DeleteAsync(string id);

        Task<IEnumerable<T>> GetAll<T>(int? count = null);

        Task<T> GetById<T>(string id);

        Task<IEnumerable<T>> GetAllOffers<T>(int? take = null, int skip = 0);

        Task<int> GetAllOffersCount();
    }
}
