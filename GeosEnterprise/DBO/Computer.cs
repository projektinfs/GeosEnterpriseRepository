using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DBO
{
    public class Computer : DBObject<int>
    {
        public string SerialNumber { get; set; }
        public virtual List<Component> Components { get; set; }
    }
}
