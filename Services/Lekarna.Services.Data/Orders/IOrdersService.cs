namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Lekarna.Data.Models;
    using Lekarna.Web.ViewModels.Orders;

    public interface IOrdersService
    {
        Task<string> CreateOrder(OrderCreateViewModel inputModel, ApplicationUser user);

        IEnumerable<T> GetAll<T>(int? count = null);
    }
}
