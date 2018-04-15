using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeosEnterprise.DBO;


namespace GeosEnterprise.Repositories
{
    public class AdressesRepository
    {
        public static Adress GetById(int adressId)
        {
            return App.DB.Adresses.Where(p => p.ID == adressId).FirstOrDefault();
        }
    }
}
