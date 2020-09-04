namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Lekarna.Data.Models;
    using Lekarna.Web.ViewModels.Categories;

    public interface ICategoriesService
    {
        Task<string> CreateAsync(string categoryName, string description, ApplicationUser user);

        Task<string> EditAsync(CategoryEditViewModel inputModel);

        Task<string> DeleteAsync(string id);

        IEnumerable<T> GetAll<T>(int? count = null);

        T GetById<T>(string id);

        IEnumerable<T> GetAllCategories<T>(int? take = null, int skip = 0);

        int GetAllCategoriesCount();
    }
}
