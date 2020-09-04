namespace Lekarna.Services.Data
{
    using System.Threading.Tasks;

    using Lekarna.Data.Models;
    using Microsoft.AspNetCore.Http;

    public interface IImagesService
    {
        Task<Image> CreateAsync(IFormFile imageSource);

        Task DeleteAsync(string id);
    }
}
