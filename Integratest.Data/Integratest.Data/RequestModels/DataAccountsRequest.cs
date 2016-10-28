using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integratest.Data.RequestModels
{
    public class DataAccountsRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }

    }
}
