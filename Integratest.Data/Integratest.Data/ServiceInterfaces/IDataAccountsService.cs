using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integratest.Data.DataModels;
using Integratest.Data.RequestModels;

namespace Integratest.Data.ServiceInterfaces
{
    public interface IDataAccountsService
    {
        Task<string> AddAccount(DataAccountsRequest account);
        Task<AccountsDto> GetAccountById(string id);
        Task<List<AccountsDto>> GetAccountByEmail(string email);


    }
}
