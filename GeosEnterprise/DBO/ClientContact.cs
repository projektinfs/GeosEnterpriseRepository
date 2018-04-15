using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DBO
{
    public class ClientContact : DBObject<int>
    {
        public string Email { get; set; }
        public string Www { get; set; }
        public int Phone { get; set; }
        public int Fax { get; set; }
    }
}
