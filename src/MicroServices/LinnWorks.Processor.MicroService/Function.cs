using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using LinnWorks.AWS.Redis;
using LinnWorks.AWS.S3;
using LinnWorks.Task.Dtos.Sales;
using LinnWorks.Task.ExcelReader.Services;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace LinnWorks.Processor.MicroService
{
    public class Function
    {
        private readonly string accessKeyID = "AKIAIDGGLITUHE6HKPNA";
        private readonly string secretKey = "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";

        IAmazonS3 S3Client { get; set; }

        /// <summary>
        /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
        /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
        /// region the Lambda function is executed in.
        /// </summary>
        public Function()
        {
            S3Client = new AmazonS3Client(accessKeyID, secretKey, RegionEndpoint.EUCentral1);
        }

        /// <summary>
        /// Constructs an instance with a preconfigured S3 client. This can be used for testing the outside of the Lambda environment.
        /// </summary>
        /// <param name="s3Client"></param>
        public Function(IAmazonS3 s3Client)
        {
            this.S3Client = s3Client;
        }

        /// <summary>
        /// This method is called for every Lambda invocation. This method takes in an S3 event object and can be used 
        /// to respond to S3 notifications.
        /// </summary>
        /// <param name="evnt"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<string> FunctionHandler(S3Event evnt, ILambdaContext context)
        {
            var s3Event = evnt.Records?[0].S3;
            if (s3Event == null)
            {
                return null;
            }

            try
            {
                RedisDataAgent agent = new RedisDataAgent();
                string key = await agent.GetValueAsync(s3Event.Object.Key);
                context.Logger.LogLine($"{key} redis value");
                S3 s3 = new S3(S3Client);
                StreamReader reader = await s3.ReadObjectDataAsync(key);
                CSVReader csvReader = new CSVReader();
                List<SaleDto> result = csvReader.ReadDocument<SaleDto>(reader);
                context.Logger.LogLine($"{key} file is processed.");
                return "Ok";
            }
            catch (Exception e)
            {
                context.Logger.LogLine($"Error getting object {s3Event.Object.Key} from bucket {s3Event.Bucket.Name}. Make sure they exist and your bucket is in the same region as this function.");
                context.Logger.LogLine(e.Message);
                context.Logger.LogLine(e.StackTrace);
                throw;
            }
        }
    }
}