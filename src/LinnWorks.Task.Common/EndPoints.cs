using System;

namespace LinnWorks.Task.Common
{
    public static class EndPoints
    {
        public static class Api
        {
            public const string GetSales = "api:getsales";
            public const string GetFilterParameters = "api:getfilterparameters";
            public const string UpdateSales = "api:updatesales";
            public const string GetLastPageIndex = "api:getlastpageindex";
        }

        public static class Queue
        {
            public const string UploadFile = "queue:uploadfile";
        }
    }
}