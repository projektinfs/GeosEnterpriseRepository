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
        public Byte[] Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public virtual Adress Adress { get; set; }
        public virtual EmployeeContact EmployeeContact { get; set; }
        public UserRole UserRole { get; set; }

        public string Position
        {
            get
            {
                return Util.GetEnumDescription(UserRole);
            }
        }

        public static IList<Employee> ForSeedToDatabase()
        {
            return new List<Employee>
            {
                new Employee
                {
                    Email = "andrzejek@gmail.com",
                    Name = "Andrzej",
                    Surname = "Kasztan",
                    Adress = new Adress
                    {
                        AppartamentNumber = "4",
                        BuildingNumber = "3",
                        City = "London",
                        District = "Buckingham",
                        PostCode = "33-000",
                        Street = "Queen Elisabeth Square",
                        Voivodeship = "Wessex"
                    },
                    EmployeeContact = new EmployeeContact
                    {
                        Phone = "503948392",
                    },
                    UserRole = UserRole.Dealer
                },
                new Employee
                {
                    Email = "kasiakasia13@gmail.com",
                    Name = "Katarzyna",
                    Surname = "Karmowska",
                    Adress = new Adress
                    {
                        BuildingNumber = "13",
                        City = "Kraków",
                        PostCode = "31-325",
                        Street = "Makowskiego",
                        Voivodeship = "Małopolskie"
                    },
                    EmployeeContact = new EmployeeContact
                    {
                        Phone = "586985458",
                    },
                    UserRole = UserRole.Serviceman
                }
            };
        }
    }
}
