namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Lekarna.Data.Models;
    using Lekarna.Web.ViewModels.Suppliers;

    public interface ISuppliersService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        Task<string> CreateAsync(SupplierCreateInputModel inputModel, ApplicationUser user);

        T GetByName<T>(string name);
    }
}
