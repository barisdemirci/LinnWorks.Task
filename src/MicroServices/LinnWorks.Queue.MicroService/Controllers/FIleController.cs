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
        public async Task<IActionResult> UploadAsync()
        {
            try
            {
                IFormFile file = Request.Form.Files[0];
                if (file != null)
                {
                    await fileService.AddQueue(file.Name, file.FileName);
                    await fileService.UploadFileToS3(file);
                    return Ok(true);
                }
                else
                {
                    return BadRequest(false);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}