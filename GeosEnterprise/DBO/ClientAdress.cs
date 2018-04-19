using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DBO
{
    public class ClientAdress : DBObject<int>
    {
        public string City { get; set; }
        public string Voivodeship { get; set; }
        public string District { get; set; }
        public string PostCode { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public int AppartamentNumber { get; set; }
    }
}
