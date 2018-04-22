using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeosEnterprise.DBO;

namespace GeosEnterprise.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>
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
                || p.Surname.Contains(filter) || p.Email.Contains(filter) || p.EmployeeContact.Phone.Contains(filter)).ToList();
            });
        }

        public static IList<Employee> GetByTimeAndDescription(string filter, DateTime? timeFrom, DateTime? timeTo)
        {
            return ExecuteQuery(() =>
            {
                IList<Employee> employees = Where(p => p.CreatedDate >= timeFrom
                    && p.CreatedDate <= timeTo).ToList();

                return employees.Where(p => p.Name.Contains(filter)
                || p.Surname.Contains(filter) || p.Email.Contains(filter) || p.EmployeeContact.Phone.Contains(filter)).ToList();
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
                Update(employee);
            });
        }

        public static void Delete(int id)
        {
            ExecuteQuery(() =>
            {
                Delete(GetById(id));
            });
        }
    }
}
