using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Amazon;
using Amazon.S3;
using LinnWorks.AWS.Redis;
using LinnWorks.AWS.S3;
using Microsoft.AspNetCore.Http;

namespace LinnWorks.Queue.MicroService.Services
{
    public class FileService : IFileService
    {
        private IAmazonS3 s3Client;

        public FileService(IAmazonS3 s3Client)
        {
            this.s3Client = s3Client ?? throw new ArgumentNullException(nameof(s3Client));
        }

        public async System.Threading.Tasks.Task AddQueue(IFormFile file)
        {
            RedisDataAgent agent = new RedisDataAgent();
            await agent.AddValueAsync(file.Name, file.FileName);
        }

        public async System.Threading.Tasks.Task UploadFileToS3(IFormFile file)
        {
            S3 uploader = new S3(s3Client);
            Stream fileStream = file.OpenReadStream();
            await uploader.UploadFileToS3Async(fileStream, file.FileName);
        }
    }
}