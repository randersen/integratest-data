using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integratest.Data.ServiceInterfaces;
using Integratest.Data.Services;

namespace Integratest.Data.Client
{
    public class DataClient
    {
        internal string AccountId { get; set; }

        public DataClient(string accountId)
        {
            AccountId = accountId;
        }

    }

    public static class DataClientExtensions
    {
        public static DataAccountsService Accounts(this DataClient dataClient)
        {
            return new DataAccountsService(dataClient.AccountId);
        }

        public static DataTestCasesService TestCases(this DataClient dataClient)
        {
            return new DataTestCasesService(dataClient.AccountId);
        }


        
    }



}
