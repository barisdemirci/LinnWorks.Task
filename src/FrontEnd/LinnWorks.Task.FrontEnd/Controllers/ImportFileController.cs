using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
        public async Task<IActionResult> Upload()
        {
            var files = this.Request.Form.Files;
            long size = files.Sum(f => f.Length);
            await importFileService.UploadFile(files);
            // full path to file in temp location
            //  var filePath = Path.GetTempFileName();

            //  foreach (var formFile in files)
            //    {
            //    if (formFile.Length > 0)
            //   {
            //     using (var stream = new FileStream(filePath, FileMode.Create))
            //  {
            //   await formFile.CopyToAsync(stream);
            //          }
            // }


            //    return Ok(new { count = files.Count, size, filePath });
            return Ok();
        }
    }
}