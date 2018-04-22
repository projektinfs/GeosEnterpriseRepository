using GeosEnterprise.Repositories;
using GeosEnterprise.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace GeosEnterprise.ViewModels
{
    public class StartPanelViewModel : PropertyChangedBase
    {
        public ICommand ChangePreferences { get; set; }

        public StartPanelViewModel()
        {
            ChangePreferences = new RelayCommand<object>(Change);
        }

        private void Change(object obj)
        {
            Window addNewEmployeeWindow = new EmployeesAdd( Authorization.AcctualEmployee.ID, false );
            addNewEmployeeWindow.Show();
        }
    }
}
