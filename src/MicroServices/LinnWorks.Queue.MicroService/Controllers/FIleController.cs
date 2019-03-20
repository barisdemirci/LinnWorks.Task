using System;
using System.Threading.Tasks;
using LinnWorks.Queue.MicroService.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinnWorks.Queue.MicroService.Controllers
{
    [Route("api/[controller]")]
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
        [EnableCors("CorsPolicy")]
        public async Task<IActionResult> Upload()
        {
            try
            {
                IFormFile file = this.Request.Form.Files[0];
                if (file != null)
                {
                    await fileService.AddQueue(file);
                    await fileService.UploadFileToS3(file);
                    return Ok("uploaded");
                }
                else
                {
                    return Ok("No file");
                }
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    error = ex.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }
    }
}