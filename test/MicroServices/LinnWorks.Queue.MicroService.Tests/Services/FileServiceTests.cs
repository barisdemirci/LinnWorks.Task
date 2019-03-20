using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Amazon.S3;
using LinnWorks.AWS.Redis;
using LinnWorks.AWS.S3;
using LinnWorks.Queue.MicroService.Services;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using Xunit;

namespace LinnWorks.Queue.MicroService.Tests.Services
{
    public class FileServiceTests
    {
        private readonly IAmazonS3 s3Client;
        private readonly IFileService fileService;
        private readonly IRedisDataAgent regisAgent;
        private readonly IFormFile file;
        private readonly IS3 s3;

        public FileServiceTests()
        {
            this.s3Client = Substitute.For<IAmazonS3>();
            this.regisAgent = Substitute.For<IRedisDataAgent>();
            file = Substitute.For<IFormFile>();
            s3 = Substitute.For<IS3>();
            fileService = new FileService(s3Client, regisAgent, s3);
        }

        [Fact]
        public void Constructor_ArgumentIsNull_ThrowsArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => new FileService(null, regisAgent, s3));
            Assert.Throws<ArgumentNullException>(() => new FileService(s3Client, null, s3));
            Assert.Throws<ArgumentNullException>(() => new FileService(s3Client, regisAgent, null));
        }

        [Fact]
        public void AddQueue_ArgumentIsNull_ThrowsArgumentNullException()
        {
            // act & assert
            Assert.ThrowsAsync<ArgumentNullException>(() => fileService.AddQueue(null));
        }

        [Fact]
        public async System.Threading.Tasks.Task AddQueue_ArgsOk_CallsRedisService()
        {
            // act
            await fileService.AddQueue(file);

            // assert
            await regisAgent.Received(1).AddValueAsync(file.Name, file.FileName);
        }

        [Fact]
        public void UploadFileToS3_ArgumentIsNull_ThrowsArgumentNullException()
        {
            // act & assert
            Assert.ThrowsAsync<ArgumentNullException>(() => fileService.UploadFileToS3(null));
        }

        [Fact]
        public async System.Threading.Tasks.Task UploadFileToS3_ArgsOk_CallsS3Service()
        {
            // act
            await fileService.UploadFileToS3(file);

            // assert
            await s3.Received(1).UploadFileToS3Async(Arg.Any<FileStream>(), file.FileName);
        }
    }
}