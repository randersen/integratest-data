using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integratest.Data.DataModels;
using Integratest.Data.RequestModels;

namespace Integratest.Data.ServiceInterfaces
{
    public interface IDataTestCaseService
    {
        Task<string> AddTestCase(DataTestCaseRequest testCaseRequest);
        Task<TestCasesDto> GetTestCase(string id);
        Task<List<TestCasesDto>> GetTestCasesForAccount();
        Task DeleteTestCase(string id);


    }
}
