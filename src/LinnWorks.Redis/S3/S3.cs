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
        private readonly IAmazonS3 client;
        private const string bucketName = "linnworksexcel";

        public S3(IAmazonS3 client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task UploadFileToS3Async(Stream fileStream, string fileNameInS3)
        {
            // create a TransferUtility instance passing it the IAmazonS3 created in the first step
            TransferUtility utility = new TransferUtility(client);
            // making a TransferUtilityUploadRequest instance
            TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();
            request.BucketName = bucketName; //no subdirectory just bucket name
            request.InputStream = fileStream;
            request.Key = fileNameInS3; //file name up in S3
            await utility.UploadAsync(request); //commensing the transfer
        }

        public async Task<StreamReader> ReadObjectDataAsync(string fileName)
        {
            StreamReader reader = null;
            try
            {
                GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = fileName
                };

                GetObjectResponse response = await client.GetObjectAsync(request);
                Stream responseStream = response.ResponseStream;
                reader = new StreamReader(responseStream);
                return reader;
            }
            catch (AmazonS3Exception e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteFileASync(string fileName)
        {
            DeleteObjectRequest request = new DeleteObjectRequest()
            {
                BucketName = bucketName,
                Key = fileName
            };

            await client.DeleteObjectAsync(request);
        }
    }
}