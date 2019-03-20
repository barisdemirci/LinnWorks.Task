using System;
using LinnWorks.Task.FrontEnd.Controllers;
using LinnWorks.Task.FrontEnd.Services;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using Xunit;

namespace LinnWorks.Task.FrontEnd.Tests.Controllers
{
    public class ImportFileControllerTests
    {
        private readonly ImportFileController importFileController;
        private readonly IImportFileService importFileService;
        private readonly IFormFile file;

        public ImportFileControllerTests()
        {
            importFileService = Substitute.For<IImportFileService>();
            file = Substitute.For<IFormFile>();
            importFileController = new ImportFileController(importFileService);
        }

        [Fact]
        public void Constructor_ArgumentIsNull_ThrowsArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => new ImportFileController(null));
        }

        [Fact]
        public async System.Threading.Tasks.Task Upload_CallsService()
        {
            // act
            await importFileController.Upload(file);

            // assert
            await importFileService.Received(1).UploadFileAsync(file);
        }
    }
}
