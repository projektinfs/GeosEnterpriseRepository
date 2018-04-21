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
        public string Phone { get; set; }
        public string Fax { get; set; }
    }
}
