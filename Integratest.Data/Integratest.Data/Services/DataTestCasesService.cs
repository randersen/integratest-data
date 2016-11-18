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
    public class DataTestCasesService : IDataTestCaseService
    {

        public async Task<string> AddTestCase(DataTestCaseRequest testCaseRequest)
        {
            if (testCaseRequest.AccountId == null)
                throw new ArgumentNullException(nameof(testCaseRequest.AccountId));

            var testCase = new TestCasesDto()
            {
                AccountId = testCaseRequest.AccountId.ToString(),
                Id = Guid.NewGuid().ToString(),
                Title = testCaseRequest.Title
            };
          
            var user = await new DataAccountsService().GetAccountById(testCase.AccountId);

            if (user == null)
                throw new KeyNotFoundException("AccountId does not exist");

            await DynamoDbContextProvider.CurrentContext.SaveAsync<TestCasesDto>(testCase);

            return testCase.Id;
        }

        public async Task<TestCasesDto> GetTestCase(string id, string accountId)
        {
            return await DynamoDbContextProvider.CurrentContext.LoadAsync<TestCasesDto>(id, rangeKey: accountId);
        }

        public async Task<List<TestCasesDto>> GetTestCasesForAccount(string accountId)
        {
            var dynamoDbConfig = new DynamoDBOperationConfig();
            dynamoDbConfig.IndexName = "AccountId-index";

            return await DynamoDbContextProvider.CurrentContext.QueryAsync<TestCasesDto>(accountId, operationConfig: dynamoDbConfig).GetRemainingAsync();
        }

        public async Task DeleteTestCase(string id, string accountId)
        {
            await DynamoDbContextProvider.CurrentContext.DeleteAsync<TestCasesDto>(id, rangeKey: accountId);
        }



    }
}
