using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LinnWorks.AWS.S3
{
    public interface IS3
    {
        Task UploadFileToS3Async(Stream fileStream, string fileNameInS3);

        Task<StreamReader> ReadObjectDataAsync(string fileName);
    }
}