namespace Lekarna.Services.Data
{
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Lekarna.Services.Data.Common;
    using Microsoft.AspNetCore.Http;

    public class ImagesService : IImagesService
    {
        private readonly Cloudinary cloudinary;

        public ImagesService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task<string> GetImageUrl(IFormFile imageSource)
        {
            if (imageSource != null)
            {
                return await this.UploadToCloudAsync(imageSource);
            }

            return null;
        }

        public async Task DeleteFromCloudAsync(string url)
        => await ApplicationCloudinary.DeleteFileAsync(this.cloudinary, url);

        private async Task<string> UploadToCloudAsync(IFormFile imageSource)
        => await ApplicationCloudinary.UploadFileAsync(this.cloudinary, imageSource);
    }
}
