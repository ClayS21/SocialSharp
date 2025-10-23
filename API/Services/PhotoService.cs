using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Helpers;
using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace API.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary cloudinary;

        public PhotoService(IOptions<CloudinarySettings> configuration)
        {
            var account = new Account(configuration.Value.CloudName, configuration.Value.ApiKey, configuration.Value.ApiSecret);
            cloudinary = new Cloudinary(account);

        }
        
        public async Task<ImageUploadResult> UploadImageAsync(IFormFile file)
        {
            var imageUploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                await using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = "SocialSharp"
                };

                imageUploadResult = await cloudinary.UploadAsync(uploadParams);
            }

            return imageUploadResult;
        }

        public async Task<DeletionResult> DeleteImageAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            return await cloudinary.DestroyAsync(deleteParams);
        }
    }
}