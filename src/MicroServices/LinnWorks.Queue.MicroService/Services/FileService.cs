using System;
using System.Collections.Generic;
using System.IO;
using Amazon.S3;
using LinnWorks.AWS.Redis;
using LinnWorks.AWS.S3;
using Microsoft.AspNetCore.Http;

namespace LinnWorks.Queue.MicroService.Services
{
    public class FileService : IFileService
    {
        private IAmazonS3 s3Client;
        private IS3 s3;
        private IRedisDataAgent regisAgent;

        public FileService(IAmazonS3 s3Client, IRedisDataAgent regisAgent, IS3 s3)
        {
            this.s3Client = s3Client ?? throw new ArgumentNullException(nameof(s3Client));
            this.regisAgent = regisAgent ?? throw new ArgumentNullException(nameof(regisAgent));
            this.s3 = s3 ?? throw new ArgumentNullException(nameof(s3));
        }

        public System.Threading.Tasks.Task AddQueue(IFormFile file)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));

            return regisAgent.AddValueAsync(file.Name, file.FileName);
        }

        public System.Threading.Tasks.Task UploadFileToS3(IFormFile file)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));

            Stream fileStream = file.OpenReadStream();
            return s3.UploadFileToS3Async(fileStream, file.FileName);
        }
    }
}