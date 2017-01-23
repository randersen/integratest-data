using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Integratest.Data.DataModels;
using Integratest.Data.Providers;
using Integratest.Data.RequestModels;
using Integratest.Data.ServiceInterfaces;
using Integratest.Security;

namespace Integratest.Data.Services
{
    public class DataTestCasesService : BaseDataService, IDataTestCaseService
    {

        public DataTestCasesService(string accountId)
        {
            AccountId = accountId;
        }

        public async Task<string> AddTestCase(DataTestCaseRequest testCaseRequest)
        {
            var testCase = new TestCasesDto()
            {
                AccountId = AccountId,
                Id = Guid.NewGuid().ToString(),
                Title = testCaseRequest.Title
            };
          
            var user = await new DataAccountsService(AccountId).GetAccountById(testCase.AccountId);

            if (user == null)
                throw new KeyNotFoundException("AccountId does not exist");

            await DynamoDbContextProvider.CurrentContext.SaveAsync<TestCasesDto>(testCase);

            return testCase.Id;
        }

        public async Task<TestCasesDto> GetTestCase(string id)
        {
            return await DynamoDbContextProvider.CurrentContext.LoadAsync<TestCasesDto>(id, rangeKey: AccountId);
        }

        public async Task<List<TestCasesDto>> GetTestCasesForAccount()
        {
            var dynamoDbConfig = new DynamoDBOperationConfig();
            dynamoDbConfig.IndexName = "AccountId-index";

            return await DynamoDbContextProvider.CurrentContext.QueryAsync<TestCasesDto>(AccountId, operationConfig: dynamoDbConfig).GetRemainingAsync();
        }

        public async Task DeleteTestCase(string id)
        {
            await DynamoDbContextProvider.CurrentContext.DeleteAsync<TestCasesDto>(id, rangeKey: AccountId);
        }



    }
}
