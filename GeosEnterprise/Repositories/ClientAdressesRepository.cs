using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeosEnterprise.DBO;


namespace GeosEnterprise.Repositories
{
    public class ClientAdressesRepository
    {
        public static ClientAdress GetById(int adressId)
        {
            return App.DB.ClientAdresses.Where(p => p.ID == adressId).FirstOrDefault();
        }
    }
}
