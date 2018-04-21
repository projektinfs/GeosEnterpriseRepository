using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DBO
{
    public class Employee : DBObject<int>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public virtual Adress Adress { get; set; }
        public virtual EmployeeContact EmployeeContact { get; set; }
        public UserRole UserRole { get; set; }
    }
}
