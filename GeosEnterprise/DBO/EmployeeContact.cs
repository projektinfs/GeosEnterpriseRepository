using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DBO
{
    public class EmployeeContact : DBObject<int>
    {
        public string Www { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
    }
}
