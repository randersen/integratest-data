using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Integratest.Data.DataModels;
using Integratest.Data.RequestModels;
using Integratest.Data.Services;
using Integratest.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Integratest.Data.Tests
{
    [TestClass]
    public class TestCaseTests
    {
        [TestMethod]
        public void AddTestCase()
        {
            var dataTestCaseService = new DataTestCasesService();

            var testCase = new DataTestCaseRequest()
            {
                AccountId = Guid.Parse("2c99f5f0-002a-41e3-8de6-0aa81f2c9907"),
                Title = "Our First TestCase"
            };

            var id = dataTestCaseService.AddTestCase(testCase).Result;

            var returnedTestCase = dataTestCaseService.GetTestCase(id, testCase.AccountId.ToString()).Result;

            Assert.AreEqual(testCase.AccountId.ToString(), returnedTestCase.AccountId);
            Assert.AreEqual(testCase.Title.ToString(), returnedTestCase.Title);

            dataTestCaseService.DeleteTestCase(id, testCase.AccountId.ToString());

            Thread.Sleep(1000);

            var deletedTestCase = dataTestCaseService.GetTestCase(id, testCase.AccountId.ToString()).Result;

            Assert.IsNull(deletedTestCase);
        }

    }
}
