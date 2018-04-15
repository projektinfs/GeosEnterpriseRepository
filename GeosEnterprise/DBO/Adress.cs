using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DBO
{
    public class Adress : DBObject<int>
    {
        public string City { get; set; }
        public string Voivodeship { get; set; }
        public string District { get; set; }
        public int PostCode { get; set; }
        public string Street { get; set; }
        public int BuildingNumber { get; set; }
        public int AppartamentNumber { get; set; }
    }
}
