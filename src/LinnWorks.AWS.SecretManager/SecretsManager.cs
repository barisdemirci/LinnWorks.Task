using System;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

namespace LinnWorks.AWS.SecretsManager
{
    public class SecretsManager : ISecretsManager
    {
        private const string SecretName = "ConnectionStringOfDB";

        public DBConnectionString GetConnectionString(IAmazonSecretsManager secretsManager)
        {
            GetSecretValueRequest request = new GetSecretValueRequest();
            request.SecretId = SecretName;
            request.VersionStage = "AWSCURRENT"; // VersionStage defaults to AWSCURRENT if unspecified.
            GetSecretValueResponse response = null;
            try
            {
                response = secretsManager.GetSecretValueAsync(request).Result;
            }
            catch (DecryptionFailureException)
            {
                // Secrets Manager can't decrypt the protected secret text using the provided KMS key.
                // Deal with the exception here, and/or rethrow at your discretion.
                throw;
            }
            catch (InternalServiceErrorException)
            {
                // An error occurred on the server side.
                // Deal with the exception here, and/or rethrow at your discretion.
                throw;
            }
            catch (InvalidParameterException)
            {
                // You provided an invalid value for a parameter.
                // Deal with the exception here, and/or rethrow at your discretion
                throw;
            }
            catch (InvalidRequestException)
            {
                // You provided a parameter value that is not valid for the current state of the resource.
                // Deal with the exception here, and/or rethrow at your discretion.
                throw;
            }
            catch (ResourceNotFoundException)
            {
                // We can't find the resource that you asked for.
                // Deal with the exception here, and/or rethrow at your discretion.
                throw;
            }
            catch (System.AggregateException)
            {
                // More than one of the above exceptions were triggered.
                // Deal with the exception here, and/or rethrow at your discretion.
                throw;
            }
            DBConnectionString connectionString = null;
            // Decrypts secret using the associated KMS CMK.
            // Depending on whether the secret is a string or binary, one of these fields will be populated.
            if (response.SecretString != null)
            {
                connectionString = Newtonsoft.Json.JsonConvert.DeserializeObject<DBConnectionString>(response.SecretString);
            }

            return connectionString;
        }
    }
}