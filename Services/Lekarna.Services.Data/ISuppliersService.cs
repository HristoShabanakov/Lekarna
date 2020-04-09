namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Lekarna.Data.Models;
    using Lekarna.Web.ViewModels.Suppliers;

    public interface ISuppliersService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        Task<string> CreateAsync(SupplierCreateViewModel inputModel, ApplicationUser user);

        T GetByName<T>(string name);

        T GetById<T>(string id);

        IEnumerable<T> GetAllSuppliers<T>(int? take = null, int skip = 0);

        int GetAllSuppliersCount();
    }
}
