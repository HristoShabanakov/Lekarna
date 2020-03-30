namespace Lekarna.Services.Data
{
    using System.Threading.Tasks;

    public interface IPharmaciesService
    {
        Task<string> CreateAsync(string name, string country, string address, string imageUrl);
    }
}
