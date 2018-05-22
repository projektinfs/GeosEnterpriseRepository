using System;
using System.Linq;
using GeosEnterprise.DBO;
using GeosEnterprise.DTO;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using GeosEnterprise.Commands;
using GeosEnterprise.Validators;
using System.Security.Cryptography;
using System.Text;

namespace GeosEnterprise.ViewModels
{
    public class EmployeesAddViewModel : INotifyPropertyChanged
    {

        public ICommand OKButtonCommand { get; set; }
        public ICommand CancelButtonCommand { get; set; }
        public EmployeeDTO BindingItem { get; set; }
        public bool IsAdminMode { get; set; }
        public int PositionIndex {get; set; }
        public string Password { get; set; }

        public EmployeesAddViewModel(int? employeeID)
        {
            if (employeeID != null)
            {
                BindingItem = EmployeeDTO.ToDTO(Repositories.EmployeeRepository.GetById((int)employeeID));
                PositionIndex = Position(BindingItem.Position);
            }
            else
            {
                BindingItem = new EmployeeDTO();
                Password = NewPassword(20);
                BindingItem.Adress = new AdressDTO();
                BindingItem.EmployeeContact = new EmployeeContactDTO();
                PositionIndex = -1;
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

        private string _EmailTaken;
        public string EmailTaken
        {
            get { return _EmailTaken; }
            set
            {
                _EmailTaken = value;
                NotifyPropertyChanged("EmailTaken");
            }
        }

        public void Save(Window window)
        {
            string errors = DoValidation();

            if (string.IsNullOrEmpty(errors))
            {
                if (BindingItem.ID <= 0)
                {
                    if (Repositories.EmployeeRepository.GetByEmail(BindingItem.Email.ToString()) != null)
                    {
                        EmailTaken = "Podany email jest już zajęty!";
                        return;
                    }
                    else
                    {
                        SHA256 SHA256 = SHA256Managed.Create();
                        Byte[] HasedPassword;
                        if (string.IsNullOrEmpty(Password))
                        {
                            HasedPassword = EmployeeDTO.ToDTO(Repositories.EmployeeRepository.GetById((int)BindingItem.ID)).Password;
                        }
                        else
                        {
                            Byte[] InBytePassword = Encoding.UTF8.GetBytes(Password);
                            HasedPassword = SHA256.ComputeHash(InBytePassword);
                        }

                        Repositories.EmployeeRepository.Add(new Employee
                        {
                            Email = BindingItem.Email,
                            Password = HasedPassword,
                            Name = BindingItem.Name,
                            Surname = BindingItem.Surname,
                            //Position = BindingItem.Position,
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
                }

                else
                {

                    SHA256 SHA256 = SHA256Managed.Create();
                    Byte[] HasedPassword;
                    if (string.IsNullOrEmpty(Password))
                    {
                        HasedPassword = EmployeeDTO.ToDTO(Repositories.EmployeeRepository.GetById((int)BindingItem.ID)).Password;
                    }
                    else
                    {
                        Byte[] InBytePassword = Encoding.UTF8.GetBytes(Password);
                        HasedPassword = SHA256.ComputeHash(InBytePassword);
                    }

                    Employee updatedEmployee = new Employee
                    {
                        ID = (int)BindingItem.ID,
                        Email = BindingItem.Email,
                        Password = HasedPassword,
                        Name = BindingItem.Name,
                        Surname = BindingItem.Surname,
                        //Position = BindingItem.Position,
                        UserRole = PositionToUserRole(BindingItem.Position.ToString()),
                        Adress = new Adress
                        {
                            ID = (int)BindingItem.Adress.ID,
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
                            ID = (int)BindingItem.EmployeeContact.ID,
                            Phone = BindingItem.EmployeeContact.Phone,
                            Fax = BindingItem.EmployeeContact.Fax,
                            Www = BindingItem.EmployeeContact.Www
                        }
                    };
                    Repositories.EmployeeRepository.Edit(updatedEmployee);


                    if (BindingItem.ID == Authorization.AcctualEmployee.ID)
                    {
                        Authorization.AcctualEmployee = updatedEmployee;
                    }
                }
            }
            else
            {
                Config.MsgBoxValidationMessage(errors);
                return;
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

        private int Position(string position)
        {

            switch (position)
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


        private static string NewPassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string DoValidation()
        {
            var validationErrors1 = ValidatorTools.DoValidation(BindingItem, new EmployeeValidator());
           
         //   if (string.IsNullOrEmpty(validationErrors1) && string.IsNullOrEmpty(Password))
            if (string.IsNullOrEmpty(validationErrors1) && (Password.Length >= 6))

            {
                    return String.Empty;
            }
            else
            {
                //if (Password.Length >= 6)
                //    return String.Empty;

                string returnString = $"{validationErrors1}\r\n";
                if (Password.Length < 6)
                {
                    returnString += $"Haslo powinno posiadać co najmniej 6 znaków !";
                }

                returnString += "\r\n".Trim();

                return returnString;
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
