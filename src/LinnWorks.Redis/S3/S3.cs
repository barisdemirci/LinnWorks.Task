using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace LinnWorks.AWS.S3
{
    public class S3 : IS3
    {
        private readonly string accessKeyID = "AKIAIDGGLITUHE6HKPNA";
        private readonly string secretKey = "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";

        public async Task UploadFileToS3Async(Stream fileStream, string fileNameInS3)
        {
            string bucketName = "linnworkssales";
            IAmazonS3 client = new AmazonS3Client(accessKeyID, secretKey, RegionEndpoint.EUCentral1);

            // create a TransferUtility instance passing it the IAmazonS3 created in the first step
            TransferUtility utility = new TransferUtility(client);
            // making a TransferUtilityUploadRequest instance
            TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();
            request.BucketName = bucketName; //no subdirectory just bucket name
            request.InputStream = fileStream;
            request.Key = fileNameInS3; //file name up in S3
            await utility.UploadAsync(request); //commensing the transfer
        }

        public async Task ReadObjectDataAsync(string fileName)
        {
            string responseBody = "";
            try
            {
                GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = "linnworkssales",
                    Key = fileName
                };
                IAmazonS3 client = new AmazonS3Client(RegionEndpoint.EUCentral1);
                using (GetObjectResponse response = await client.GetObjectAsync(request))
                using (Stream responseStream = response.ResponseStream)
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    string title = response.Metadata["x-amz-meta-title"]; // Assume you have "title" as medata added to the object.
                    string contentType = response.Headers["Content-Type"];
                    Console.WriteLine("Object metadata, Title: {0}", title);
                    Console.WriteLine("Content type: {0}", contentType);

                    responseBody = reader.ReadToEnd(); // Now you process the response body.
                }
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered ***. Message:'{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }
        }
    }
}