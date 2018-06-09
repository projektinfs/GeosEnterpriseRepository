using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeosEnterprise.DBO;
using GeosEnterprise.DTO;
using GeosEnterprise.Repositories;
using System.Windows;
using System.Windows.Input;
using GeosEnterprise.Commands;
using System.ComponentModel;
using System.Windows.Controls;
using System.Security.Cryptography;
using System.Runtime.Remoting.Messaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace GeosEnterprise.ViewModel
{
    public class AuthenticationViewModel : ViewModelBase, INotifyPropertyChanged
    {

        public ICommand SignInCommand { get; private set; }
        public Employee currentEmployee { get; set; }
        public String currentName { get; set; }

        private string _MessageForUser;
        public string MessageForUser
        {
            get { return _MessageForUser; }
            set
            {
                _MessageForUser = value;
                RaisePropertyChanged("MessageForUser");
            }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                RaisePropertyChanged("Name");
            }
        }

        private string _IsVisible;
        public string IsVisible
        {
            get { return _IsVisible; }
            set
            {
                _IsVisible = value;
                RaisePropertyChanged("IsVisible");
            }
        }

        public string Email { get; set; }
        public string Password { get; set; }

        public AuthenticationViewModel()
        {
            if (EmployeeRepository.GetByEmail("admin@admin.pl") == null)
            {
                SHA256 SHA256 = SHA256Managed.Create();

                Byte[] InBytePassword = Encoding.UTF8.GetBytes("admin123");
                Byte[] HasedPassword = SHA256.ComputeHash(InBytePassword);

                EmployeeRepository.Insert(new Employee
                {
                    Email = "admin@admin.pl",
                    Password = HasedPassword,
                    Name ="Admin",
                    Surname = "Adminowski",
                    Position = "Administrator",
                    UserRole = UserRole.Administrator,
                    Adress = new Adress
                    {
                        City = "Krakow",
                        Voivodeship = "Malopolskie",
                        District = "Krakow",
                        PostCode = "30-031",
                        Street = "Tokarskiego",
                        BuildingNumber = "6",
                        AppartamentNumber = "310"
                    },
                    EmployeeContact = new EmployeeContact
                    {
                        Phone = "123456789",
                        Fax = "987654321",
                        Www = "www.geosenterprise.com"
                    }
                });
            }

            IsVisible = "Visible";
            Name = "";
            Messenger.Default.Send("Hidden");

            SignInCommand = new SignInCommand(
                SignIn,
                (object parameters) => true
            );

        }

        private void SignIn(object parameter)
        {
            var passwordBox = parameter as System.Windows.Controls.PasswordBox;
            Password = passwordBox.Password;

            if (System.Diagnostics.Debugger.IsAttached && Config.IgnoreAuthentication)
            {
                var admin = EmployeeRepository.GetByEmail("admin@admin.pl");
                if (admin.UserRole != UserRole.Administrator)
                {
                    admin.UserRole = UserRole.Administrator;
                    Repositories.EmployeeRepository.Update(admin);
                }
                Authorization.AcctualEmployee = admin;
                
                Authorization.AcctualUser = "admin@admin.pl";
                IsVisible = "Hidden";
                Name = Authorization.AcctualEmployee?.Name + " " + Authorization.AcctualEmployee?.Surname;
                passwordBox.Clear();
                currentEmployee = Authorization.AcctualEmployee;
                Access();
                currentName = Authorization.AcctualEmployee?.Name + " " + Authorization.AcctualEmployee?.Surname;
                Messenger.Default.Send<ViewModelBase>(new StartPanelViewModel());
                Messenger.Default.Send("Visible");
                Messenger.Default.Send(Name);
                return;
            }

            if (EmployeeRepository.ValidateData(Email, Password) && !String.IsNullOrEmpty(Email) && !String.IsNullOrEmpty(Password))
            {
                Authorization.AcctualUser = Email;
                Authorization.AcctualEmployee = EmployeeRepository.GetByEmail(Email);
                MessageForUser = "";
                IsVisible = "Hidden";
                passwordBox.Clear();
                //currentEmployee = EmployeeRepository.GetByEmail("admin@admin.pl");
                currentEmployee = Authorization.AcctualEmployee;
                Access();
                Name = Authorization.AcctualEmployee.Name + " " + Authorization.AcctualEmployee.Surname;
                Messenger.Default.Send<ViewModelBase>(new StartPanelViewModel());
                Messenger.Default.Send("Visible");
                Messenger.Default.Send(Name);

            }
            else
            {
                MessageForUser = "Błędny login lub hasło !";
            }
        }

        private void Access()
        {
            Dictionary<string, bool> Permissions = new Dictionary<string, bool>();

            switch (currentEmployee.UserRole)
            {
                case UserRole.Administrator:
                    Permissions["ComputerList"] = true;
                    Permissions["EmployeeList"] = true;
                    Permissions["ClientsList"] = true;
                    Permissions["AccountantPanel"] = true;
                    Permissions["EmployeeScheduler"] = true;
                    Permissions["RepairScheduler"] = true;
                    Permissions["Logs"] = true;
                    break;
                case UserRole.Manager:
                    Permissions["ComputerList"] = true;
                    Permissions["EmployeeList"] = true;
                    Permissions["ClientsList"] = true;
                    Permissions["AccountantPanel"] = false;
                    Permissions["EmployeeScheduler"] = true;
                    Permissions["RepairScheduler"] = true;
                    Permissions["Logs"] = true;
                    break;
                case UserRole.Dealer:
                    Permissions["ComputerList"] = true;
                    Permissions["EmployeeList"] = true;
                    Permissions["ClientsList"] = true;
                    Permissions["AccountantPanel"] = false;
                    Permissions["EmployeeScheduler"] = true;
                    Permissions["RepairScheduler"] = true;
                    Permissions["Logs"] = true;
                    break;
                case UserRole.Serviceman:
                    Permissions["ComputerList"] = true;
                    Permissions["EmployeeList"] = false;
                    Permissions["ClientsList"] = true;
                    Permissions["AccountantPanel"] = false;
                    Permissions["EmployeeScheduler"] = true;
                    Permissions["RepairScheduler"] = true;
                    Permissions["Logs"] = true;
                    break;
                case UserRole.Accountant:
                    Permissions["ComputerList"] = false;
                    Permissions["EmployeeList"] = true;
                    Permissions["ClientsList"] = true;
                    Permissions["AccountantPanel"] = true;
                    Permissions["EmployeeScheduler"] = false;
                    Permissions["RepairScheduler"] = true;
                    Permissions["Logs"] = true;
                    break;
                default:
                    Permissions["ComputerList"] = false;
                    Permissions["EmployeeList"] = false;
                    Permissions["ClientsList"] = false;
                    Permissions["AccountantPanel"] = false;
                    Permissions["EmployeeScheduler"] = false;
                    Permissions["RepairScheduler"] = true;
                    Permissions["Logs"] = true;
                    break;
            }

            Messenger.Default.Send(Permissions);
        }
    }
}
