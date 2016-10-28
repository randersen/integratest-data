using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integratest.Data.RequestModels;
using Integratest.Data.Security;
using Integratest.Data.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Integratest.Data.Tests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void AddUser()
        {
            var user = new DataAccountsRequest()
            {
                CompanyName = "Number One",
                Email = "Douche@gmail.com",
                Password = "test1234"
            };

           var id = DataAccountsService.AddAccount(user).Result;

            var returnedUser = DataAccountsService.GetAccountById(id);

            var otherReturnedUser = DataAccountsService.GetAccountByEmail(user.Email).Result.FirstOrDefault();

            var passwordHash = returnedUser.Result.PasswordHash;

            var correct = IntegraTestEncryption.IsCorrectPassword(user.Password, passwordHash);
        }

    }
}
