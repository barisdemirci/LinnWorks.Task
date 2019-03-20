using System;
using System.Threading.Tasks;
using LinnWorks.Task.FrontEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinnWorks.Task.FrontEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportFileController : Controller
    {
        private readonly IImportFileService importFileService;

        public ImportFileController(IImportFileService sectorService)
        {
            this.importFileService = sectorService ?? throw new ArgumentNullException(nameof(sectorService));
        }


        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));

            await importFileService.UploadFileAsync(file);

            return Ok();
        }
    }
}