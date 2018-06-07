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
using GeosEnterprise.Commands;
using GeosEnterprise.DBO;
using System.ComponentModel;
using GalaSoft.MvvmLight;

namespace GeosEnterprise.ViewModel
{
    public class StartPanelViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public ICommand ChangePreferences { get; set; }

        public Employee currentEmployee { get; set; }
        public String Name { get; set; }
        public String _Address;
        public String Address
        {
            get
            {
                return _Address;
            }

            set
            {
                _Address = currentEmployee?.Adress?.City + " " + currentEmployee?.Adress?.PostCode;
                RaisePropertyChanged("Address");
            }
           
        }

        public String _BuildingAndApartamentNumber;
        public String BuildingAndApartamentNumber
        {
            get
            {
                return _BuildingAndApartamentNumber;
            }

            set
            {
                _BuildingAndApartamentNumber = currentEmployee.Adress.Street + ", " + currentEmployee.Adress.BuildingNumber;
                if (  !String.IsNullOrEmpty(currentEmployee.Adress.AppartamentNumber) )
                {
                    _BuildingAndApartamentNumber += "\\" + currentEmployee.Adress.AppartamentNumber;
                }
                RaisePropertyChanged("BuildingAndApartamentNumber");
            }
        }

        public StartPanelViewModel()
        {
            currentEmployee = Authorization.AcctualEmployee;
            Address = "Changed";
            BuildingAndApartamentNumber = "Changed";
            Name = Authorization.AcctualEmployee.Name + " " + Authorization.AcctualEmployee.Surname;
            ChangePreferences = new RelayCommand<object>(Change);
        }

        private void Change(object obj)
        {
            Window addNewEmployeeWindow = new EmployeesAdd( Authorization.AcctualEmployee.ID, false );
            addNewEmployeeWindow.Closed += AddNewEmployeeWindowClosed;

            addNewEmployeeWindow.Show();
        }

        private void AddNewEmployeeWindowClosed(object sender, EventArgs e)
        {
            currentEmployee = Authorization.AcctualEmployee;
            Address = "Changed";
            BuildingAndApartamentNumber = "Changed";
            RaisePropertyChanged("currentEmployee");
        }
    }
}
