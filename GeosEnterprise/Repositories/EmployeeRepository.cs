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

        public static Employee Edit(Employee employee)
        {

            var toEdit = App.DB.Employees.Where(p => p.ID == employee.ID).FirstOrDefault();
            toEdit.ModifiedBy = "admin";
            toEdit.ModifiedDate = DateTime.Now;
            toEdit.Email = employee.Email;
            toEdit.Password = employee.Password;
            toEdit.Name = employee.Name;
            toEdit.Surname = employee.Surname;
            toEdit.Position = employee.Position;
            toEdit.Adress.City = employee.Adress.City;
            toEdit.Adress.Voivodeship = employee.Adress.Voivodeship;
            toEdit.Adress.District = employee.Adress.District;
            toEdit.Adress.PostCode = employee.Adress.PostCode;
            toEdit.Adress.Street = employee.Adress.Street;
            toEdit.Adress.BuildingNumber = employee.Adress.BuildingNumber;
            toEdit.Adress.AppartamentNumber = employee.Adress.AppartamentNumber;
            toEdit.EmployeeContact.Phone= employee.EmployeeContact.Phone;
            toEdit.EmployeeContact.Fax = employee.EmployeeContact.Fax;
            toEdit.EmployeeContact.Www = employee.EmployeeContact.Www;

            App.DB.SaveChanges();
            return toEdit;
        }

        public static Employee GetById(int id)
        {
            return App.DB.Employees.Where(p => p.ID == id).FirstOrDefault();
        }

        public static IList<Employee> GetAllCurrent()
        {
            return App.DB.Employees.ToList();
        }

        public static void Delete(int id)
        {
            App.DB.Employees.Remove(GetById(id));
            App.DB.SaveChanges();
        }



    }
}
