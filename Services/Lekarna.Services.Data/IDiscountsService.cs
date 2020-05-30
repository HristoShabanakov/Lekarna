namespace Lekarna.Services.Data
{
    using System.Threading.Tasks;

    using Lekarna.Web.ViewModels.Discounts;

    public interface IDiscountsService
    {
        Task<string> CreateAsync(DiscountViewModel inputModel);
    }
}
