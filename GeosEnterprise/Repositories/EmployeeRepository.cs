using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeosEnterprise.DBO;

namespace GeosEnterprise.Repositories
{
    public static class EmployeeRepository
    {
        public static Employee GetByEmail(string Email)
        {
            return App.DB.Employees.Where(p => p.Email == Email).FirstOrDefault();
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
            var added = App.DB.Employees.Add(employee);
            App.DB.SaveChanges();
            return added;
        }
    }
}
