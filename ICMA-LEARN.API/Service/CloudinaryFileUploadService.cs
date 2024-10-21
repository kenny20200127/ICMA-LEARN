
using System.ComponentModel.DataAnnotations;
using Azure;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ICMA_LEARN.API.DataModel.FileUpload;
using Microsoft.Extensions.Options;

namespace ICMA_LEARN.API.Service
{
    /// <summary>
    /// Upload File Using Cloudinary
    /// </summary>
    public class CloudinaryFileUploadService : IFileUploadService
    {
        private readonly Cloudinary cloudinary;
        private readonly ILogger<CloudinaryFileUploadService> logger;

        public CloudinaryFileUploadService(IOptionsSnapshot<CloudinarySettings> cloudinary,
            ILogger<CloudinaryFileUploadService> logger)
        {

            this.logger = logger;

            var acc = new Account
            (
                cloudinary.Value.CloudName,
                cloudinary.Value.ApiKey,
                cloudinary.Value.ApiSecret
            );
            this.cloudinary = new Cloudinary(acc);
        }
        public async ValueTask<UploadResponseDataModel> UploadAsync(UploadRequestDataModel objFile)
        {
            var response = new UploadResponseDataModel();
            var uploadResult = new ImageUploadResult();
            try
            {
                if (objFile.Files.Length > 0)
                {
                    using var stream = objFile.Files.OpenReadStream();
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(objFile.Files.FileName, stream),
                        //Transformation= new Transformation().Width(100).Height(150).Crop("fill").Gravity("face"),
                        PublicId = objFile.FolderName + "/" + objFile.Files.FileName

                    };
                    uploadResult = await cloudinary.UploadAsync(uploadParams);
                    response.Uri = uploadResult.SecureUri.AbsoluteUri;
                    response.Status = "Successful";
                    response.Name = uploadResult.DisplayName;
                }
            }
            catch (RequestFailedException ex)
            {
                logger.LogError($"Unhandled Exception. ID: {ex.StackTrace} - Message: {ex.Message}");
                response.Status = $"Unexpected error: {ex.StackTrace}. Check log with StackTrace ID.";
                return response;
            }
            return response;
        }

        public async ValueTask<UploadResponseDataModel> UploadVideoAsync(UploadRequestDataModel objFile)
        {
            var response = new UploadResponseDataModel();
            var uploadResult = new VideoUploadResult();

            try
            {
                if (objFile.Files.Length > 0)
                {
                    using var stream = objFile.Files.OpenReadStream();

                    var uploadParams = new VideoUploadParams() 
                    {
                        File = new FileDescription(objFile.Files.FileName, stream),
                        PublicId = objFile.FolderName + "/" + objFile.Files.FileName
                    };
                    uploadResult = await cloudinary.UploadAsync(uploadParams);
                    response.Uri = uploadResult.SecureUri.AbsoluteUri;
                    response.Status = "Successful";
                    response.Name = uploadResult.DisplayName;
                }
            }
            catch (RequestFailedException ex)
            {
                logger.LogError($"Unhandled Exception. ID: {ex.StackTrace} - Message: {ex.Message}");
                response.Status = $"Unexpected error: {ex.StackTrace}. Check log with StackTrace ID.";
                return response;
            }

            return response;
        }

    }
}
