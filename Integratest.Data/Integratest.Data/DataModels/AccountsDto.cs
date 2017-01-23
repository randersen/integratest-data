using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;

namespace Integratest.Data.DataModels
{
    [DynamoDBTable("Accounts")]
    public class AccountsDto
    {
        [DynamoDBHashKey]
        public string Id { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string PasswordHash { get; set; }
        public List<string> Roles { get; set; }
        public bool IsEmployee { get; set; }



    }
}
