using LinnWorks.Task.Common;
using LinnWorks.Task.Core.Network;
using LinnWorks.Task.FrontEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LinnWorks.Task.FrontEnd.Tests.Services.ImportFile
{
    public class ImportFileServiceTests
    {
        private readonly IImportFileService importFileService;
        private readonly IHttpClientWrapper httpClient;
        private readonly IConfiguration configuration;
        private readonly IFormFile file;

        public ImportFileServiceTests()
        {
            httpClient = Substitute.For<IHttpClientWrapper>();
            configuration = Substitute.For<IConfiguration>();
            file = Substitute.For<IFormFile>();
            importFileService = new ImportFileService(httpClient, configuration);
        }

        [Fact]
        public void Constructor_ArgumentIsNull_ThrowsArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => new ImportFileService(null, configuration));
            Assert.Throws<ArgumentNullException>(() => new ImportFileService(httpClient, null));
        }

        [Fact]
        public void UploadFile_ArgumentIsNull_ThrowsArgumentNullException()
        {
            // act & assert
            Assert.ThrowsAsync<ArgumentNullException>(() => importFileService.UploadFileAsync(null));
        }

        [Fact]
        public async System.Threading.Tasks.Task UploadFile_ArgOk_CallsApi()
        {
            // arrange
            string endpoint = "endPoint";
            configuration[EndPoints.Queue.UploadFile].Returns(endpoint);

            // act
            await importFileService.UploadFileAsync(file);

            // assert
            await httpClient.Received(1).PostAsync(endpoint, file);
        }
    }
}