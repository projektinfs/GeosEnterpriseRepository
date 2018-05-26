using GeosEnterprise.DBO;
using GeosEnterprise.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GeosEnterprise.Commands;
using GalaSoft.MvvmLight;

namespace GeosEnterprise.ViewModel
{
    public class EmployeesInfoViewModel : ViewModelBase
    {
        public ICommand CancelButtonCommand { get; set; }
        public EmployeeDTO BindingItem { get; set; }

        public EmployeesInfoViewModel(int? employeeID)
        {
            if (employeeID != null)
            {
                BindingItem = EmployeeDTO.ToDTO(Repositories.EmployeeRepository.GetById((int)employeeID));
            }
            else
            {
                BindingItem = new EmployeeDTO();
                BindingItem.Adress = new AdressDTO();
                BindingItem.EmployeeContact = new EmployeeContactDTO();

            }


            CancelButtonCommand = new RelayCommand<Window>(Cancel);
        }

        public void Cancel(Window window)
        {
            window?.Close();
        }

        public int PositionId()
        {
           
            switch (BindingItem.Position)
            {
                case "Kierownik":
                    return 0;
                case "Księgowy":
                    return 1;
                case "Serwisant":
                    return 2;
                case "Sprzedawca":
                    return 3;
                default:
                    return -1;
            }
        }
    }
}