using GeosEnterprise.DBO;
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
        public static Employee _AcctualEmployee = new Employee
        {
            Email = "NaN@NaN.pl",
            Password = { },
            Name = "NaN",
            Surname = "NaN",
            Position = "NaN",
            Adress = new Adress
            {
                City = "NoCity",
                Voivodeship = "NaN",
                District = "NaN",
                PostCode = "NaN:NaN",
                Street = "NaN",
                BuildingNumber = "NaN",
                AppartamentNumber = "NaN"
            },
            EmployeeContact = new EmployeeContact
            {
                Www = "www.NaN.pl",
                Phone = "+48NaN",
                Fax = "+48NaN"
            },

            UserRole = UserRole.Administrator
        };
        
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

        public static string Name
        {
            get
            {
                return AcctualEmployee.Name + " " + AcctualEmployee.Surname;
            }

            set
            {
                _AcctualUser = value;
            }
        }

        public static Employee AcctualEmployee
        {
            get
            {
                return _AcctualEmployee;
            }

            set
            {
                _AcctualEmployee = value;
            }
        }

    }
}
