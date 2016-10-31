using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.Model;
using Integratest.Data.Providers;

namespace Integratest.Data.Tables
{
    public class TableCreator
    {

        public static void CreateTable(CreateTableRequest tableRequest)
        {

            var response = DynamoDbContextProvider.CurrentClient.CreateTable(tableRequest);

            var tableDescription = response.TableDescription;
            Console.WriteLine("{1}: {0} \t ReadsPerSec: {2} \t WritesPerSec: {3}",
                            tableDescription.TableStatus,
                            tableDescription.TableName,
                            tableDescription.ProvisionedThroughput.ReadCapacityUnits,
                            tableDescription.ProvisionedThroughput.WriteCapacityUnits);

            string status = tableDescription.TableStatus;
            Console.WriteLine(tableRequest.TableName + " - " + status);

            WaitUntilTableReady(tableRequest.TableName);
        }


        private static void WaitUntilTableReady(string tableName)
        {
            string status = null;
            // Let us wait until table is created. Call DescribeTable.
            do
            {
                System.Threading.Thread.Sleep(5000); // Wait 5 seconds.
                try
                {
                    var res = DynamoDbContextProvider.CurrentClient.DescribeTable(new DescribeTableRequest
                    {
                        TableName = tableName
                    });

                    Console.WriteLine("Table name: {0}, status: {1}",
                                   res.Table.TableName,
                                   res.Table.TableStatus);
                    status = res.Table.TableStatus;
                }
                catch (ResourceNotFoundException)
                {
                    // DescribeTable is eventually consistent. So you might
                    // get resource not found. So we handle the potential exception.
                }
            } while (status != "ACTIVE");
        }

    }
}
