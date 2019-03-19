using System;
using System.Threading.Tasks;
using Amazon.S3;
using LinnWorks.Queue.MicroService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinnWorks.Queue.MicroService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService fileService;

        public FileController(IFileService fileService)
        {
            this.fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        }

        [HttpPost]
        [Route("upload")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                if (file != null)
                {
                    await fileService.AddQueue(file);
                    await fileService.UploadFileToS3(file);
                    return Ok("uploaded");
                }
                else
                {
                    return BadRequest("No file");
                }
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    error = ex.Message,
                    innerException = ex.InnerException,
                    stackTrace = ex.StackTrace
                });
            }
        }
    }
}