using GeosEnterprise.DBO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GeosEnterprise
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static EntitiesContext DB = new EntitiesContext();

        public static void InitializeDatabase()
        {
            if (!App.DB.Database.Exists())
            {
                App.DB.Database.Create();
                SeedDatabase();
            }
            else if (!App.DB.Database.CompatibleWithModel(false) && Config.DropAndCreateWhenModelChanges)
            {
                App.DB.Database.Delete();
                App.DB.Database.Create();
                SeedDatabase();
            }
            else if (!App.DB.Computers.Any())
            {
                SeedDatabase();
            }
            App.DB.Computers.Any();
        }

        private static void SeedDatabase()
        {
            foreach (var client in DBO.Client.ForSeedToDatabase())
            {
                Repositories.ClientRepository.Insert(client);
            }

            foreach (var employee in DBO.Employee.ForSeedToDatabase())
            {
                Repositories.EmployeeRepository.Insert(employee);
            }

            foreach (var repair in DBO.Repair.ForSeedToDatabase())
            {
                Repositories.RepairsRepository.Insert(repair);
            }

            foreach (var activity in DBO.EmployeeActivity.ForSeedToDatabase())
            {
                Repositories.EmployeeActivityRepository.Insert(activity);
            }
        }
    }

    /// <summary>
    /// Przechowywanie danych dotyczących aktualnej sesji zalogowanego użytkownika
    /// </summary>
    public class Session
    {
        public static string Username = Authorization.AcctualUser;
        public static UserRole UserRole = Authorization.AcctualEmployee.UserRole;
    }
}
