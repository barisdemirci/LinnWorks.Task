using System;
using System.Threading.Tasks;
using LinnWorks.Task.FrontEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinnWorks.Task.FrontEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportFileController : Controller
    {
        private readonly IImportFileService importFileService;

        public ImportFileController(IImportFileService importFileService)
        {
            this.importFileService = importFileService ?? throw new ArgumentNullException(nameof(importFileService));
        }


        [HttpPost]
        [Route("Upload")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            IFormFile file = this.Request.Form.Files[0];
            if (Request == null)
            {
                return Ok("File is empty");
            }
            if (file == null) throw new ArgumentNullException(nameof(file));

            await importFileService.UploadFileAsync(file);

            return Ok();
        }
    }
}