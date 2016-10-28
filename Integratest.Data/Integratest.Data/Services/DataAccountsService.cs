using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Integratest.Data.DataModels;
using Integratest.Data.Providers;
using Integratest.Data.RequestModels;
using Integratest.Data.Security;

namespace Integratest.Data.Services
{
    public class DataAccountsService
    {

        public static async Task<string> AddAccount(DataAccountsRequest account)
        {
            var accountDto = new AccountsDto()
            {
                CompanyName = account.CompanyName,
                Email = account.Email,
                Id = Guid.NewGuid().ToString(),
                PasswordHash = IntegraTestEncryption.ComputePasswordHash(account.Password)

            };

            await DynamoDbContextProvider.CurrentContext.SaveAsync<AccountsDto>(accountDto);

            return accountDto.Id;
        }

        public static async Task<AccountsDto> GetAccountById(string id)
        {
            return await DynamoDbContextProvider.CurrentContext.LoadAsync<AccountsDto>(id);
        }

        public static async Task<List<AccountsDto>> GetAccountByEmail(string email)
        {
            var dynamoDbConfig = new DynamoDBOperationConfig();
            dynamoDbConfig.IndexName = "Email-index";

            return await DynamoDbContextProvider.CurrentContext.QueryAsync<AccountsDto>(email, operationConfig: dynamoDbConfig).GetRemainingAsync();
        }


    }
}
