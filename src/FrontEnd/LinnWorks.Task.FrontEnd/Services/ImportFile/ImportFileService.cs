using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinnWorks.Task.Common;
using LinnWorks.Task.Core;
using LinnWorks.Task.Core.Network;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace LinnWorks.Task.FrontEnd.Services
{
    public class ImportFileService : IImportFileService
    {
        private readonly IHttpClientWrapper httpClient;
        private readonly IConfiguration configuration;

        public ImportFileService(IHttpClientWrapper httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            httpClient.BaseUrl = this.configuration[Constants.QueueBaseUrl];
        }

        public async System.Threading.Tasks.Task UploadFileAsync(IFormFile file)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));

            string endPoint = configuration[EndPoints.Queue.UploadFile];
            var result = await httpClient.PostAsync(endPoint, file);
        }
    }
}