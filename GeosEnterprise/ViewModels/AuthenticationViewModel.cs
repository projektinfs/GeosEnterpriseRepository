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

namespace GeosEnterprise.ViewModels
{
    public class AuthenticationViewModel
    {

        public ICommand SignInCommand { get; private set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public AuthenticationViewModel()
        {
            SignInCommand = new SignInCommand(
                SignIn,
                (object parameters) => true
                );
        }

        private void SignIn(object parameter)
        {
            
            if (EmployeeRepository.ValidateData(Email, Password))
            {
                MessageBox.Show("Zalogowano !");
            }
            else
            {
                MessageBox.Show("Nie zalogowano !");
            }
        }

    }
}
