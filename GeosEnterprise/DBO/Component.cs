using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DBO
{
    public class Component : DBObject<int>
    {
        public string Name { get; set; }
        public ComponentType Type { get; set; }
        public string Description { get; set; }
        public string Producer { get; set; }
        public List<Photo> Photos { get; set; }
        public decimal? ShoppingPrice { get; set; }
        public decimal? RepairPrice { get; set; }
        public int ComputerID { get; set; }
    }

    
}
