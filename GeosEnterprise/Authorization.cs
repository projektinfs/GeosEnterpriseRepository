using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise
{
    public class Authorization
    {

        public static string _AcctualUser = "NaN";
        public static string AcctualUser
        {
            get
            {
                return _AcctualUser;
            }

            set
            {
                _AcctualUser = value;
            }
        }

    }
}
