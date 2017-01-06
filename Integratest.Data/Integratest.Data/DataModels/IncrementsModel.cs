using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;

namespace Integratest.Data.DataModels
{
    [DynamoDBTable("Increments")]
    public class IncrementsModel
    {
        [DynamoDBHashKey]
        public string AccountId { get; set; }
        [DynamoDBRangeKey]
        public string TableName { get; set; }
        public int Next { get; set; }


    }
}
