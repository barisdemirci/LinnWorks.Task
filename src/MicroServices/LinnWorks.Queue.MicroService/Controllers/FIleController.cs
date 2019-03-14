using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Upload()
        {
            try
            {
                var files = this.Request.Form.Files;
                if (files != null)
                {
                    await fileService.UploadFile(files);
                    long size = files.Sum(f => f.Length);
                    // full path to file in temp location
                    var filePath = Path.GetTempFileName();

                    foreach (var formFile in files)
                    {
                        if (formFile.Length > 0)
                        {
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await formFile.CopyToAsync(stream);
                            }
                        }
                    }
                    return Ok(new { count = files.Count, size, filePath });
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