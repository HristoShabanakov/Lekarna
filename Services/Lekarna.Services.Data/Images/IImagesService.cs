namespace Lekarna.Services.Data
{
    using System.Threading.Tasks;

    using Lekarna.Data.Models;
    using Lekarna.Services.Data.Common;
    using Microsoft.AspNetCore.Http;

    public interface IImagesService : IService
    {
        Task<Image> CreateAsync(IFormFile imageSource);

        Task DeleteAsync(string id);
    }
}
