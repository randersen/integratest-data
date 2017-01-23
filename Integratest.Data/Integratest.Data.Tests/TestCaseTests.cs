using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Integratest.Data.Client;
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
        public async Task AddTestCase()
        {

            var client = new DataClient("664ca358-3ae8-4d79-9554-682d21a01467").TestCases();

            var testCase = new DataTestCaseRequest()
            {
                Title = "Our First TestCase"
            };

            var id = client.AddTestCase(testCase).Result;

            var returnedTestCase = client.GetTestCase(id).Result;

            Assert.AreEqual(client.AccountId.ToString(), returnedTestCase.AccountId);
            Assert.AreEqual(testCase.Title.ToString(), returnedTestCase.Title);

            await client.DeleteTestCase(id).ConfigureAwait(false);

            Thread.Sleep(1000);

            var deletedTestCase = client.GetTestCase(id).Result;

            Assert.IsNull(deletedTestCase);
        }

    }
}
