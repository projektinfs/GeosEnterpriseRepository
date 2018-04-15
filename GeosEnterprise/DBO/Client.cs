using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DBO
{
    public class Client : DBObject<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public virtual ClientAdress ClientAdress { get; set; }
        public virtual ClientContact ClientContact { get; set; }

    }
}
