using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeosEnterprise.DBO;


namespace GeosEnterprise.Repositories
{
    public class EmployeeContactsRepository
    {
        public static EmployeeContact GetById(int contactId)
        {
            return App.DB.EmployeeContacts.Where(p => p.ID == contactId).FirstOrDefault();
        }
    }
}
