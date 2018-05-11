using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static IList<Employee> GetByTime(DateTime? timeFrom, DateTime? timeTo)
        {
            return ExecuteQuery(() =>
            {
                return Where(p => p.CreatedDate >= timeFrom
                    && p.CreatedDate <= timeTo).ToList();
            });
        }

        public static IList<Employee> GetByDescription(string filter)
        {
            return ExecuteQuery(() =>
            {
                return Where(p => p.Name.Contains(filter)
                || p.Surname.Contains(filter) 
                || p.Email.Contains(filter) 
                || p.EmployeeContact.Phone.Contains(filter)
                || p.Position.Contains(filter)).ToList();
            });
        }

        public static IList<Employee> GetByTimeAndDescription(string filter, DateTime? timeFrom, DateTime? timeTo)
        {
            return ExecuteQuery(() =>
            {
                IList<Employee> employees = Where(p => p.CreatedDate >= timeFrom
                    && p.CreatedDate <= timeTo).ToList();

                return employees.Where(p => p.Name.Contains(filter)
                || p.Surname.Contains(filter)
                || p.Email.Contains(filter)
                || p.EmployeeContact.Phone.Contains(filter)
                || p.Position.Contains(filter)).ToList();
            });
        }

        public static IList<Employee> GetAll(string filter, DateTime? TimeFrom, DateTime? TimeTo)
        {
            if (filter == "Wpisz tekst...")
            {
                if (TimeFrom.HasValue == false || TimeTo.HasValue == false)
                    return GetAllCurrent();
                else
                    return GetByTime(TimeFrom, TimeTo);
            }
            else
            {
                if (TimeFrom.HasValue == false || TimeTo.HasValue == false)
                    return GetByDescription(filter);
                else
                    return GetByTimeAndDescription(filter, TimeFrom, TimeTo);
            }
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
