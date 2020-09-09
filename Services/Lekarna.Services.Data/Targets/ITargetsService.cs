namespace Lekarna.Services.Data
{
    using System.Threading.Tasks;

    public interface ITargetsService
    {
        Task<string> CreateAsync(int quantity);
    }
}
