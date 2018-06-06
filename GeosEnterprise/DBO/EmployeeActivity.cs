using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DBO
{
    public class EmployeeActivity : DBObject<int>
    {
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public int? RepairID { get; set; }
        public string Description { get; set; }
    }
}
