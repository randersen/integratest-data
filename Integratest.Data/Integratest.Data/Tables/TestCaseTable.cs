using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace Integratest.Data.Tables
{
    public class TestCaseTable
    {

        public static void CreateTable()
        {
            //Instatiate table request and lists
            var tableRequest = new CreateTableRequest()
            {
                AttributeDefinitions = new List<AttributeDefinition>(),
                GlobalSecondaryIndexes = new List<GlobalSecondaryIndex>(),
                KeySchema = new List<KeySchemaElement>(),
                TableName = "TestCases"

            };

            //add attributes
            tableRequest.AttributeDefinitions.Add(new AttributeDefinition()
            {
                AttributeName = "Id",
                AttributeType = ScalarAttributeType.S
            });
            tableRequest.AttributeDefinitions.Add(new AttributeDefinition()
            {
                AttributeName = "AccountId",
                AttributeType = ScalarAttributeType.S
            });

            //add indexes
            tableRequest.GlobalSecondaryIndexes.Add(new GlobalSecondaryIndex()
            {
                IndexName = "AccountId-index",
                KeySchema = new List<KeySchemaElement>()
                {
                    new KeySchemaElement()
                    {
                        AttributeName = "AccountId",
                        KeyType = KeyType.HASH

                    }
                },
                ProvisionedThroughput = new ProvisionedThroughput()
                {
                    ReadCapacityUnits = 1,
                    WriteCapacityUnits = 1
                },
                Projection = new Projection()
                {
                    ProjectionType = new ProjectionType("ALL")
                }
            });

            //add key
            tableRequest.KeySchema.Add(new KeySchemaElement()
            {
                AttributeName = "Id",
                KeyType = KeyType.HASH
            });

            tableRequest.KeySchema.Add(new KeySchemaElement()
            {
                AttributeName = "AccountId",
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
