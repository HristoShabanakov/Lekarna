namespace Lekarna.Services.Data
{
    using System.Threading.Tasks;

    using Lekarna.Data.Models;
    using Lekarna.Web.ViewModels.Offers;

    public interface IOffersService
    {
        Task<string> CreateAsync(OfferCreateInputModel inputModel, ApplicationUser user);

        T GetById<T>(string id);
    }
}
