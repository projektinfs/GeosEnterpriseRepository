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
        public int Phone { get; set; }
        public int Fax { get; set; }
    }
}
