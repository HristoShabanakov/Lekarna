namespace Lekarna.Services.Data
{
    using System.Threading.Tasks;

    using Lekarna.Services.Data.Common;
    using Microsoft.AspNetCore.Http;

    public interface IImagesService : IService
    {
        Task<string> GetImageUrl(IFormFile imageSource);

        Task DeleteFromCloudAsync(string url);
    }
}
