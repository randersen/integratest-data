using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace Integratest.Data.Tables
{
    public class AccountsTable
    {

        public static void CreateTable()
        {
            //Instatiate table request and lists
            var tableRequest = new CreateTableRequest()
            {
                AttributeDefinitions = new List<AttributeDefinition>(),
                GlobalSecondaryIndexes = new List<GlobalSecondaryIndex>(),
                KeySchema = new List<KeySchemaElement>(),
                TableName = "Accounts"

            };

            //add attributes
            tableRequest.AttributeDefinitions.Add(new AttributeDefinition()
            {
                AttributeName = "Id",
                AttributeType = ScalarAttributeType.S
            });
            tableRequest.AttributeDefinitions.Add(new AttributeDefinition()
            {
                AttributeName = "Email",
                AttributeType = ScalarAttributeType.S
            });

            tableRequest.GlobalSecondaryIndexes.Add(new GlobalSecondaryIndex()
            {
                IndexName = "Email-index",
                KeySchema = new List<KeySchemaElement>()
                {
                    new KeySchemaElement()
                    {
                        AttributeName = "Email",
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

            tableRequest.KeySchema.Add(new KeySchemaElement()
            {
                AttributeName = "Id",
                KeyType = KeyType.HASH
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
