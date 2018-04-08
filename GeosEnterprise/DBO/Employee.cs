using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DBO
{
    public class Employee : DBObject<int>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
