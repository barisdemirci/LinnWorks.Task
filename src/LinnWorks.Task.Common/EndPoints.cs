using System;

namespace LinnWorks.Task.Common
{
    public static class EndPoints
    {
        public static class Api
        {
            public const string GetSales = "api:getsales";
            public const string GetFilterParameters = "api:getfilterparameters";
        }

        public static class Queue
        {
            public const string UploadFile = "queue:uploadfile";
        }
    }
}