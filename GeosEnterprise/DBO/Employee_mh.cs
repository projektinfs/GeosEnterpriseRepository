using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DBO
{
    public class Employee_mh : DBObject<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
