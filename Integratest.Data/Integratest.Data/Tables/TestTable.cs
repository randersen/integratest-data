using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace Integratest.Data.Tables
{
    public class TestTable
    {
        public static void CreateTable()
        {
            //Instatiate table request and lists
            var tableRequest = new CreateTableRequest()
            {
                AttributeDefinitions = new List<AttributeDefinition>(),
                KeySchema = new List<KeySchemaElement>(),
                TableName = "TestTable"

            };

            //add attributes
            tableRequest.AttributeDefinitions.Add(new AttributeDefinition()
            {
                AttributeName = "Id",
                AttributeType = ScalarAttributeType.S
            });

            //add key
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
