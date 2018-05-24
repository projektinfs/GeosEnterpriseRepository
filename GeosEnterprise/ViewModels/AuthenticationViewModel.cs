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

namespace GeosEnterprise.ViewModels
{
    public class AuthenticationViewModel : INotifyPropertyChanged
    {

        public ICommand SignInCommand { get; private set; }
        public Employee currentEmployee { get; set; }


        private bool _IsAuthenticated;
        public bool IsAuthenticated
        {
            get { return _IsAuthenticated; }
            set
            {
                _IsAuthenticated = value;
                NotifyPropertyChanged("IsAuthenticated");
            }
        }

        private string _MessageForUser;
        public string MessageForUser
        {
            get { return _MessageForUser; }
            set
            {
                _MessageForUser = value;
                NotifyPropertyChanged("MessageForUser");
            }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                NotifyPropertyChanged("Name");
            }
        }

        private string _IsVisible;
        public string IsVisible
        {
            get { return _IsVisible; }
            set
            {
                _IsVisible = value;
                NotifyPropertyChanged("IsVisible");
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

                Employee employee = new Employee
                {
                    Email = "admin@admin.pl",
                    Password = HasedPassword,
                    //Position = "Administrator",
                    Adress = new Adress
                    {
                        PostCode = "00:000",

                    },
                    EmployeeContact = new EmployeeContact
                    {
                        Www = "www.notexist.pl",
                    }
                                       
                };

                EmployeeRepository.Add(employee);
            }

            IsVisible = "Visible";
            Name = "";

            IsAuthenticated = false;
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
                Authorization.AcctualEmployee = EmployeeRepository.GetByEmail("admin@admin.pl");
                Authorization.AcctualUser = "admin@admin.pl";
                IsAuthenticated = true;
                IsVisible = "Hidden";
                Name = Authorization.AcctualEmployee.Name + " " + Authorization.AcctualEmployee.Surname;
                passwordBox.Clear();
                currentEmployee = Authorization.AcctualEmployee;
                return;
            }

            if (EmployeeRepository.ValidateData(Email, Password) && !String.IsNullOrEmpty(Email) && !String.IsNullOrEmpty(Password))
            {
                Authorization.AcctualUser = Email;
                Authorization.AcctualEmployee = EmployeeRepository.GetByEmail(Email);
                IsAuthenticated = true;
                MessageForUser = "";
                IsVisible = "Hidden";
                passwordBox.Clear();
                currentEmployee = EmployeeRepository.GetByEmail("admin@admin.pl");
                Name = Authorization.AcctualEmployee.Name + " " + Authorization.AcctualEmployee.Surname;


            }
            else
            {
                MessageForUser = "Błędny login lub hasło !";
            }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
