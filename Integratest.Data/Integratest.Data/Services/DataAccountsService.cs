using System;
using System.Collections.Generic;
using System.Data;
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
    public class DataAccountsService : IDataAccountsService
    {

        public  async Task<string> AddAccount(DataAccountsRequest account)
        {
            var duplicateAccount = await GetAccountByEmail(account.Email);

            if (duplicateAccount.Count > 0)
            {
                throw new DuplicateNameException($"Account alread exists with email: {account.Email}");
            }

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

        public  async Task<AccountsDto> GetAccountById(string id)
        {
            return await DynamoDbContextProvider.CurrentContext.LoadAsync<AccountsDto>(id);
        }

        public  async Task<List<AccountsDto>> GetAccountByEmail(string email)
        {
            var dynamoDbConfig = new DynamoDBOperationConfig();
            dynamoDbConfig.IndexName = "Email-index";

            return await DynamoDbContextProvider.CurrentContext.QueryAsync<AccountsDto>(email, operationConfig: dynamoDbConfig).GetRemainingAsync();
        }


    }
}
