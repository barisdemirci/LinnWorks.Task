using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinnWorks.Queue.MicroService.Services
{
    public interface IFileService
    {
        Task AddQueue(IFormFile file);

        Task UploadFileToS3(IFormFile file);
    }
}