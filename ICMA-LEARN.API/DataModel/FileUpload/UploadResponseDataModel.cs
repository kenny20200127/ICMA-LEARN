namespace ICMA_LEARN.API.DataModel.FileUpload
{
    public class UploadResponseDataModel
    {
        public string? Uri { get; set; }
        public string? Name { get; set; }
        public string? ContentType { get; set; }
        public string? Status { get; set; }
        public Stream? Content { get; set; }
    }
}
