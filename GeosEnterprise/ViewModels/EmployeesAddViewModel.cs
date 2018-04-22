using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeosEnterprise.DBO;
using GeosEnterprise.DTO;
using System.Windows;
using System.Windows.Input;


namespace GeosEnterprise.ViewModels
{
    public class EmployeesAddViewModel
    {

        public ICommand OKButtonCommand { get; set; }
        public ICommand CancelButtonCommand { get; set; }
        public EmployeeDTO BindingItem { get; set; }

        public bool IsAdminMode { get; set; }

        public EmployeesAddViewModel(int? employeeID)
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
            OKButtonCommand = new RelayCommand<Window>(Save);
            CancelButtonCommand = new RelayCommand<Window>(Cancel);
            IsAdminMode = true;
        }

        public EmployeesAddViewModel(int employeeID, bool IsAdminMode)
        {
            BindingItem = EmployeeDTO.ToDTO(Repositories.EmployeeRepository.GetById((int)employeeID));
            OKButtonCommand = new RelayCommand<Window>(Save);
            CancelButtonCommand = new RelayCommand<Window>(Cancel);
            this.IsAdminMode = IsAdminMode;
        }

        public void Save(Window window)
        {
            if (BindingItem.ID <=0)
            {
                Repositories.EmployeeRepository.Add(new Employee
                {
                    Email = BindingItem.Email,
                    Password = BindingItem.Password,
                    Name = BindingItem.Name,
                    Surname = BindingItem.Surname,
                    Position = BindingItem.Position,
                    UserRole = PositionToUserRole(BindingItem.Position.ToString()),
                    Adress = new Adress
                    {
                        City = BindingItem.Adress.City,
                        Voivodeship = BindingItem.Adress.Voivodeship,
                        District = BindingItem.Adress.District,
                        PostCode = BindingItem.Adress.PostCode,
                        Street = BindingItem.Adress.Street,
                        BuildingNumber = BindingItem.Adress.BuildingNumber,
                        AppartamentNumber = BindingItem.Adress.AppartamentNumber
                    },
                    EmployeeContact = new EmployeeContact
                    {
                        Phone = BindingItem.EmployeeContact.Phone,
                        Fax = BindingItem.EmployeeContact.Fax,
                        Www = BindingItem.EmployeeContact.Www
                    }
                });

            }

            else
            {
                Repositories.EmployeeRepository.Edit(new Employee
                {
                    ID = (int)BindingItem.ID,
                    Email = BindingItem.Email,
                    Password = BindingItem.Password,
                    Name = BindingItem.Name,
                    Surname = BindingItem.Surname,
                    Position = BindingItem.Position,
                    UserRole = PositionToUserRole(BindingItem.Position.ToString()),
                    Adress = new Adress
                    {
                        City = BindingItem.Adress.City,
                        Voivodeship = BindingItem.Adress.Voivodeship,
                        District = BindingItem.Adress.District,
                        PostCode = BindingItem.Adress.PostCode,
                        Street = BindingItem.Adress.Street,
                        BuildingNumber = BindingItem.Adress.BuildingNumber,
                        AppartamentNumber = BindingItem.Adress.AppartamentNumber
                    },
                    EmployeeContact = new EmployeeContact
                    {
                        Phone = BindingItem.EmployeeContact.Phone,
                        Fax = BindingItem.EmployeeContact.Fax,
                        Www = BindingItem.EmployeeContact.Www
                    }
                });
            }

            window?.Close();

        }

        public void Cancel(Window window)
        {
            window?.Close();
            
        }

        private UserRole PositionToUserRole(string position)
        {
            switch (position)
            {
                case "Kierownik":
                    return UserRole.Manager;
                case "Księgowy":
                    return UserRole.Accountant;
                case "Serwisant":
                    return UserRole.Serviceman;
                case "Sprzedawca":
                    return UserRole.Dealer;
                default:
                    return UserRole.Unknown;
            }
        }

    }
}
