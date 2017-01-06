using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integratest.Data.DataModels;
using Integratest.Data.Providers;

namespace Integratest.Data.Services
{
    public class DataIncrementsService
    {
        private Guid _accountId { get; }

        public DataIncrementsService(Guid accountId)
        {
            _accountId = accountId;
        }

        //public async Task<int> IncrementTable(string tableName)
        //{
        //    var increment = await GetIncrement(tableName).ConfigureAwait(false) ??
        //                    await CreateNewIncrement(tableName).ConfigureAwait(false);

            


        //}

        public async Task AddIncrement(IncrementsModel increment)
        {
            await DynamoDbContextProvider.CurrentContext.SaveAsync(increment).ConfigureAwait(false);
        }

        public async Task<IncrementsModel> GetIncrement(string tableName)
        {
            return await DynamoDbContextProvider.CurrentContext.LoadAsync<IncrementsModel>(tableName).ConfigureAwait(false);
        }

        public async Task<IncrementsModel> CreateNewIncrement(string tableName)
        {
            var increment = new IncrementsModel()
            {
                Next = 1,
                TableName = tableName
            };

            await AddIncrement(increment).ConfigureAwait(false);

            return increment;
        }


    }
}
