using ICMA_LEARN.API.DataModel.FileUpload;
using ICMA_LEARN.API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ICMA_LEARN.API.Controllers
{

    [ApiController]
    [AllowAnonymous]
    [Route("api")]
    public class FileUploadController : ControllerBase
    {
        private readonly IFileUploadService fileUploadService;
        private readonly ILogger<FileUploadController> logger;
        private readonly IOptions<List<FolderDataModel>> folderNames;

        public FileUploadController(IFileUploadService fileUploadService,
             ILogger<FileUploadController> logger,
              IOptions<List<FolderDataModel>> folderNames)
        {
            this.fileUploadService = fileUploadService;
            this.logger = logger;
            this.folderNames = folderNames;
        }


        [HttpPost("FileUpload")]
        public async Task<ActionResult<string>> UploadToCloud([FromForm] UploadRequestDataModel objFile)
        {
            var folder = folderNames.Value.Where(x => x.Name == objFile.FolderName);
            if (!folder.Any())
            {
                ModelState.AddModelError("UploadToCloud", "Folder Name Not Valid");
                return ValidationProblem(instance: "100", modelStateDictionary: ModelState);
            }
            var result = await fileUploadService.UploadAsync(objFile);
            if (result.Status != "Successful")
            {

                ModelState.AddModelError("UploadToCloud", result.Status);
                return ValidationProblem(instance: "100", modelStateDictionary: ModelState);
            }

            logger.LogInformation("FileUploaded successfully with new image url -  {0}", result.Uri);

            return Ok(result.Uri);
        }
        [HttpPost("uploadVideo")]
        public async Task<IActionResult> UploadVideo([FromForm] UploadRequestDataModel objFile)
        {
            var folder = folderNames.Value.Where(x => x.Name == objFile.FolderName);
            if (!folder.Any())
            {
                ModelState.AddModelError("UploadToCloud", "Folder Name Not Valid");
                return ValidationProblem(instance: "100", modelStateDictionary: ModelState);
            }

            try
            {
                var videoUrl = await fileUploadService.UploadVideoAsync(objFile);
                return Ok(new { Url = videoUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
