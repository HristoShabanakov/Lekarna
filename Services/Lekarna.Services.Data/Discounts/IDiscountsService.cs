namespace Lekarna.Services.Data
{
    using System.Threading.Tasks;

    using Lekarna.Services.Data.Common;

    public interface IDiscountsService : IService
    {
        Task<string> CreateAsync(decimal value);
    }
}
