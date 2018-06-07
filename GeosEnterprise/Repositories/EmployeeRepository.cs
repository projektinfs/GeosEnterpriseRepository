using System;
using System.Linq;
using GeosEnterprise.DBO;
using GeosEnterprise.DTO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Collections;

namespace GeosEnterprise.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee, EmployeeDTO>
    {

        public static Employee GetByEmail(string Email)
        {
            return Where(p => p.Email == Email).FirstOrDefault();
        }

        public static Employee GetByNameAndSurname(string Name,string Surname)
        {
            return Where(p => p.Name == Name).FirstOrDefault();
        }

        /*
        public static Employee GetByID(int ID)
        {
            return Where(p => p.ID == ID).FirstOrDefault();
        }
        */

        public static bool ValidateData(string Email, string Password)
        {
            Employee employee = GetByEmail(Email);
            if (employee != null)
            {
                SHA256 SHA256 = SHA256Managed.Create();
                Byte[] HasedEmpolyeePassword = employee.Password;

                Byte[] InBytePassword = Encoding.UTF8.GetBytes(Password);
                Byte[] HasedPassword = SHA256.ComputeHash(InBytePassword);

                return StructuralComparisons.StructuralEqualityComparer.Equals(HasedEmpolyeePassword, HasedPassword);
            }

            return false;
        }


        public static void Edit(Employee employee)
        {
            ExecuteQuery(() =>
            {
                var toEdit = App.DB.Employees.Where(p => p.ID == employee.ID).FirstOrDefault();
                toEdit.ModifiedBy = Session.Username;
                toEdit.ModifiedDate = DateTime.Now;
                toEdit.Email = employee.Email;
                toEdit.Password = employee.Password;
                toEdit.Name = employee.Name;
                toEdit.Surname = employee.Surname;
                //toEdit.Position = employee.Position;
                toEdit.UserRole = employee.UserRole;
                toEdit.Adress.City = employee.Adress.City;
                toEdit.Adress.Voivodeship = employee.Adress.Voivodeship;
                toEdit.Adress.District = employee.Adress.District;
                toEdit.Adress.PostCode = employee.Adress.PostCode;
                toEdit.Adress.Street = employee.Adress.Street;
                toEdit.Adress.BuildingNumber = employee.Adress.BuildingNumber;
                toEdit.Adress.AppartamentNumber = employee.Adress.AppartamentNumber;
                toEdit.EmployeeContact.Phone = employee.EmployeeContact.Phone;
                toEdit.EmployeeContact.Fax = employee.EmployeeContact.Fax;
                toEdit.EmployeeContact.Www = employee.EmployeeContact.Www;
                return toEdit;
            });

        }


        public UserRole PositionToUserRole(string position)
        {
            switch (position)
            {
                case "Kierownik":
                    return UserRole.Manager;
                case "Księgowy":
                    return UserRole.Accountant;
                case "Serwisant":
                    return UserRole.Serviceman;
                case "Sprzedawca":
                    return UserRole.Dealer;
                default:
                    return UserRole.Unknown;
            }
        }
    }
}
