using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DBO
{
    public class Log : DBO.DBObject<int>
    {
        public Log()
        {

        }

        public Log(string val)
        {
            Value = val;
        }
        public string Value { get; set; }
    }
}
