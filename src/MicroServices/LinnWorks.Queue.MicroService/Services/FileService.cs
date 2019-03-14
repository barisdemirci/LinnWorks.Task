using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinnWorks.Redis;
using Microsoft.AspNetCore.Http;
using StackExchange.Redis;

namespace LinnWorks.Queue.MicroService.Services
{
    public class FileService : IFileService
    {
        public Task UploadFile(IFormFileCollection files)
        {
            RedisDataAgent agent = new RedisDataAgent();
            agent.AddFile(files[0].FileName, "test");
            return Task.FromResult(0);
        }
    }
}