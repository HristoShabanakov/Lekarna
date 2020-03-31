namespace Lekarna.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPharmaciesService
    {
        Task<string> CreateAsync(string name, string country, string address, string imageUrl);

        IEnumerable<T> GetAll<T>(int? count = null);

        T GetById<T>(string id);
    }
}
