using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integratest.Data.ServiceInterfaces;
using Integratest.Data.Services;

namespace Integratest.Data
{
    public class DataClient
    {
        public Guid AccountId { get; set; }

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

        public static DataIncrementsService Increments(this DataClient dataClient)
        {
            return new DataIncrementsService();
        }

        public static DataClient ForAccount(this DataClient dataClient, int accountid)
        {
            return new DataClient();
        }
    }



}
