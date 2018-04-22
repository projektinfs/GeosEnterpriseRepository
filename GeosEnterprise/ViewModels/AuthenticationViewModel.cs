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

namespace GeosEnterprise.ViewModels
{
    public class AuthenticationViewModel : INotifyPropertyChanged
    {

        public ICommand SignInCommand { get; private set; }

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

                Employee employee = new Employee
                {
                    Email = "admin@admin.pl",
                    Password = "admin123",
                    Position = "Administrator"
                };

                EmployeeRepository.Add(employee);
            }

            IsVisible = "Visible";
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
           

            if (EmployeeRepository.ValidateData(Email, Password) && !String.IsNullOrEmpty(Email) && !String.IsNullOrEmpty(Password))
            {
                Authorization.AcctualUser = Email;
                Authorization.AcctualEmployee = EmployeeRepository.GetByEmail(Email);
                IsAuthenticated = true;
                MessageForUser = "";
                IsVisible = "Hidden";
            }
            else
            {
                MessageForUser = "Błędy login lub hasło !";
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
