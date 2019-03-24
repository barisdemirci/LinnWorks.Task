using Microsoft.AspNetCore.Http;

namespace LinnWorks.Queue.MicroService.Services
{
    public interface IFileService
    {
        System.Threading.Tasks.Task AddQueue(string key, string value);

        System.Threading.Tasks.Task UploadFileToS3(IFormFile file);
    }
}