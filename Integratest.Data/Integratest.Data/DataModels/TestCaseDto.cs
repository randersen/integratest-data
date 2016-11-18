using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;

namespace Integratest.Data.DataModels
{
    [DynamoDBTable("TestCases")]
    public class TestCasesDto
    {
        [DynamoDBHashKey]
        public string Id { get; set; }
        [DynamoDBRangeKey]
        public string AccountId { get; set; }
        public string Title { get; set; }
    }
}
