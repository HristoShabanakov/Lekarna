namespace Lekarna.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Lekarna.Data.Common.Repositories;
    using Lekarna.Data.Models;
    using Lekarna.Services.Data.Common;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;

    public class ImagesService : IImagesService
    {
        private readonly Cloudinary cloudinary;
        private readonly IConfiguration configuration;
        private readonly IDeletableEntityRepository<Image> imagesRepository;

        private readonly string imagePathPrefix;
        private readonly string cloudinaryPrefix = "https://res.cloudinary.com/{0}/image/upload/";

        public ImagesService(
            Cloudinary cloudinary,
            IConfiguration configuration,
            IDeletableEntityRepository<Image> imagesRepository)
        {
            this.cloudinary = cloudinary;
            this.configuration = configuration;
            this.imagesRepository = imagesRepository;
            this.imagePathPrefix = string.Format(this.cloudinaryPrefix, this.configuration["Cloudinary:CloudName"]);
        }

        public async Task<Image> CreateAsync(IFormFile imageSource)
        {
            var completeUrl = await ApplicationCloudinary.UploadFileAsync(this.cloudinary, imageSource);
            var url = completeUrl.Replace(this.imagePathPrefix, string.Empty);
            var image = new Image { Url = url };

            await this.imagesRepository.AddAsync(image);
            await this.imagesRepository.SaveChangesAsync();

            return image;
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
