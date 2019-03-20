using Amazon.SecretsManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinnWorks.AWS.SecretsManager
{
    public interface ISecretsManager
    {
        DBConnectionString GetConnectionString(IAmazonSecretsManager secretsManager);
    }
}