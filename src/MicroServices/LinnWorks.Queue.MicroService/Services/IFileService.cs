using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinnWorks.Queue.MicroService.Services
{
    public interface IFileService
    {
        System.Threading.Tasks.Task AddQueue(IFormFile file);

        System.Threading.Tasks.Task UploadFileToS3(IFormFile file);
    }
}