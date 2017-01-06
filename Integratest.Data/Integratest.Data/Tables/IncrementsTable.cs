using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace Integratest.Data.Tables
{
    public class IncrementsTable
    {
        public static void CreateTable()
        {
            //Instatiate table request and lists
            var tableRequest = new CreateTableRequest()
            {
                AttributeDefinitions = new List<AttributeDefinition>(),
                GlobalSecondaryIndexes = new List<GlobalSecondaryIndex>(),
                KeySchema = new List<KeySchemaElement>(),
                TableName = "Increments"

            };

            //add attributes
            tableRequest.AttributeDefinitions.Add(new AttributeDefinition()
            {
                AttributeName = "AccountId",
                AttributeType = ScalarAttributeType.S
            });
            tableRequest.AttributeDefinitions.Add(new AttributeDefinition()
            {
                AttributeName = "TableName",
                AttributeType = ScalarAttributeType.N
            });
            

            tableRequest.KeySchema.Add(new KeySchemaElement()
            {
                AttributeName = "AccountId",
                KeyType = KeyType.HASH
            });
            //add key
            tableRequest.KeySchema.Add(new KeySchemaElement()
            {
                AttributeName = "TableName",
                KeyType = KeyType.RANGE
            });
            

            //add throughput
            tableRequest.ProvisionedThroughput = new ProvisionedThroughput()
            {
                ReadCapacityUnits = 1,
                WriteCapacityUnits = 1
            };

            TableCreator.CreateTable(tableRequest);


        }
    }
}
