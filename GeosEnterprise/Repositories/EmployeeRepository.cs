using System;
using System.Collections.Generic;
using System.Linq;
using GeosEnterprise.DBO;
using GeosEnterprise.DTO;


namespace GeosEnterprise.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee, EmployeeDTO>
    {

        public static Employee GetByEmail(string Email)
        {
            return Where(p => p.Email == Email).FirstOrDefault();
        }

        public static bool ValidateData(string Email, string Password)
        {
            Employee employee = GetByEmail(Email);
            if (employee != null)
                return employee.Password == Password;

            return false;
        }

        public static Employee Add(Employee employee)
        {
            return ExecuteQuery(() =>
            {
                return Insert(employee);
            });
        }


        public static void Edit(Employee employee)
        {
            ExecuteQuery(() =>
            {
                var toEdit = App.DB.Employees.Where(p => p.ID == employee.ID).FirstOrDefault();
                toEdit.ModifiedBy = Session.Username;
                toEdit.ModifiedDate = DateTime.Now;
                toEdit.Name = employee.Name;
                toEdit.Surname = employee.Surname;
                toEdit.Position = employee.Position;
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

        public static void Delete(int id)
        {
            ExecuteQuery(() =>
            {
                Delete(GetById(id));
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
