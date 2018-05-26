using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GeosEnterprise.ViewModel;
using GeosEnterprise.DBO;
using System.Security.Principal;
using System.Threading;
using System.Security;

namespace GeosEnterprise
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeDatabase();
        }

        private void InitializeDatabase()
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
            App.DB.Computers.Any();
        }

        private void SeedDatabase()
        {
            foreach (var client in DBO.Client.ForSeedToDatabase())
            {
                Repositories.ClientRepository.Add(client);
            }

            foreach (var employee in DBO.Employee.ForSeedToDatabase())
            {
                Repositories.EmployeeRepository.Add(employee);
            }

            foreach (var repair in DBO.Repair.ForSeedToDatabase())
            {
                Repositories.RepairsRepository.Add(repair);
            }
        }
    }
}
