using System;
using System.Collections.Generic;
using System.Text;

namespace LinnWorks.AWS.SecretsManager
{
    public class DBConnectionString
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Host { get; set; }

        public string DbInstanceIdentifier { get; set; }
    }
}