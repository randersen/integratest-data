using System;
using System.Net;
using System.Security;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;

namespace Integratest.Data.Providers
{
    public class DynamoDbContextProvider
    {
        private static string _awsUsername = Environment.GetEnvironmentVariable("INTEAGRATEST_AWS_USERNAME");

        private static readonly string _awsPassword = Environment.GetEnvironmentVariable("INTEGRATEST_AWS_PASSWORD");

        private static AmazonDynamoDBClient _client
            => new AmazonDynamoDBClient(new BasicAWSCredentials(_awsUsername, _awsPassword), RegionEndpoint.USWest2);


        public static DynamoDBContext CurrentContext => new DynamoDBContext(_client);
        public static AmazonDynamoDBClient CurrentClient => _client;
    }
}
