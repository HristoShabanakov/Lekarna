namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoriesService
    {
        Task<string> CreateAsync(string categoryName, string description);

        IEnumerable<T> GetAll<T>(int? count = null);

        T GetById<T>(string id);
    }
}
