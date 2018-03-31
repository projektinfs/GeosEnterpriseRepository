using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DBO
{
    public class Computer : DBObject<int>
    {
        public int RepairID { get; set; }
        public string SerialNumber { get; set; }
        public List<Component> Components { get; set; }
    }
}
