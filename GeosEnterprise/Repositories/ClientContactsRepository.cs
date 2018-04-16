using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeosEnterprise.DBO;


namespace GeosEnterprise.Repositories
{
    public class ClientContactsRepository
    {
        public static ClientContact GetById(int contactId)
        {
            return App.DB.ClientContacts.Where(p => p.ID == contactId).FirstOrDefault();
        }
    }
}
