using ICMA_LEARN.API.DataModel.FileUpload;


namespace ICMA_LEARN.API.Service
{
    public interface IFileUploadService
    {
        ValueTask<UploadResponseDataModel> UploadAsync(UploadRequestDataModel objFile);
        ValueTask<UploadResponseDataModel> UploadVideoAsync(UploadRequestDataModel objFile);
    }
}
