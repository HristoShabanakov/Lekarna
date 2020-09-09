namespace Lekarna.Services.Data
{
    using System.Threading.Tasks;

    using Lekarna.Services.Data.Common;

    public interface ITargetsService : IService
    {
        Task<string> CreateAsync(int quantity);
    }
}
