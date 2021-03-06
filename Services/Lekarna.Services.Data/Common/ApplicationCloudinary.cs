﻿namespace Lekarna.Services.Data.Common
{
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class ApplicationCloudinary
    {
        public static async Task<string> UploadFileAsync(Cloudinary cloudinary, IFormFile file)
        {
            byte[] destinationFile;

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                destinationFile = memoryStream.ToArray();
            }

            ImageUploadResult uploadResult;
            using (var memoryStream = new MemoryStream(destinationFile))
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, memoryStream),
                };

                uploadResult = await cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult.SecureUri.AbsoluteUri;
        }

        public static async Task DeleteFileAsync(Cloudinary cloudinary, string url)
        {
            var publicId = GetCloudinaryPublicIdFromUrl(url);

            var deletionParams = new DeletionParams(publicId);
            await cloudinary.DestroyAsync(deletionParams);
        }

        private static string GetCloudinaryPublicIdFromUrl(string url)
        {
            var startIndex = url.IndexOf('/') + 1;
            var length = url.LastIndexOf('.') - startIndex;
            var publicId = url.Substring(startIndex, length);

            return publicId;
        }
    }
}
