using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LinnWorks.AWS.Redis;
using LinnWorks.AWS.S3;
using LinnWorks.Task.Dtos.Sales;
using Microsoft.AspNetCore.Http;

namespace LinnWorks.Queue.MicroService.Services
{
    public class FileService : IFileService
    {
        public async System.Threading.Tasks.Task AddQueue(IFormFile file)
        {
            RedisDataAgent agent = new RedisDataAgent();
            await agent.AddValueAsync(file.Name, file.FileName);
        }

        public async System.Threading.Tasks.Task UploadFileToS3(IFormFile file)
        {
            S3 uploader = new S3();

            Stream fileStream = file.OpenReadStream();

            await uploader.UploadFileToS3Async(fileStream, file.FileName);
        }
    }
}