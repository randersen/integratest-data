using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integratest.Data.Providers;
using Integratest.Data.Tables;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Integratest.Data.Tests
{
    [TestClass]
    public class TablesTests
    {

        [TestMethod]
        public void Test()
        {
            TestCaseTable.CreateTable();
            //AccountsTable.CreateTable();
        }

    }
}
