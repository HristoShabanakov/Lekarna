namespace Lekarna.Services.Data
{
    using System.Threading.Tasks;

    public interface IDiscountsService
    {
        Task<string> CreateAsync(decimal value);
    }
}
