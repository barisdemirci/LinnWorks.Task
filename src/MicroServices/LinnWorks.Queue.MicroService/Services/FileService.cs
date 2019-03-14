using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LinnWorks.Redis;
using LinnWorks.Redis.S3;
using Microsoft.AspNetCore.Http;
using StackExchange.Redis;

namespace LinnWorks.Queue.MicroService.Services
{
    public class FileService : IFileService
    {
        public Task AddQueue(IFormFile file)
        {
            RedisDataAgent agent = new RedisDataAgent();
            agent.AddFile(file.FileName, "test");
            return Task.FromResult(0);
        }

        public Task UploadFileToS3(IFormFile file)
        {
            AmazonUploader uploader = new AmazonUploader();

            Stream fileStream = file.OpenReadStream();

            uploader.SendMyFileToS3(fileStream, file.FileName);


            return Task.FromResult(0);
        }
    }
}