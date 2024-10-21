using Microsoft.AspNetCore.Http;

namespace ICMA_LEARN.API.DataModel.FileUpload
{
    public class UploadRequestDataModel
    {
        public string? FolderName { get; set; }
        public IFormFile? Files { get; set; }
        // public string? ImgName { get; set; }
    }
}
