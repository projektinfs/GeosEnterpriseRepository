using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise
{
    public static class Config
    {
        /// <summary>
        /// True - jeśli chcemy wciąż trzymać usunięte rekordy w bazie
        /// </summary>
        public static bool DoNotDeletePermanently = true;
        /// <summary>
        /// True - jeżeli w trybie debugowania proces autentykacji ma zostać pominięty
        /// </summary>
        public static bool IgnoreAuthentication = false;
    }
}
