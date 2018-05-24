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
        public int PositionIndex { get; set; }
        public string Password { get; set; }

        public EmployeesAddViewModel(int? employeeID)
        {
            if (employeeID != null)
            {
                BindingItem = EmployeeDTO.ToDTO(Repositories.EmployeeRepository.GetById((int)employeeID));
                Password = "";
                PositionIndex = Position(BindingItem.Position);
                PasswordMessage = "Visible";
                PasswordLabel = "Hasło";
            }
            else
            {
                BindingItem = new EmployeeDTO();
                Password = NewPassword(20);
                BindingItem.Adress = new AdressDTO();
                BindingItem.EmployeeContact = new EmployeeContactDTO();
                PositionIndex = -1;
                PasswordMessage = "Hidden";
                PasswordLabel = "Hasło*";
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

        private string _PasswordMessage;
        public string PasswordMessage
        {
            get { return _PasswordMessage; }
            set
            {
                _PasswordMessage = value;
                NotifyPropertyChanged("PasswordMessage");
            }
        }

        private string _PasswordLabel;
        public string PasswordLabel
        {
            get { return _PasswordLabel; }
            set
            {
                _PasswordLabel = value;
                NotifyPropertyChanged("PasswordLabel");
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
                            Email = BindingItem.Email.ToLower(),
                            Password = HasedPassword,
                            Name = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(BindingItem.Name.ToLower()),
                            Surname = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(BindingItem.Surname.ToLower()),
                            Position = BindingItem.Position,
                            UserRole = PositionToUserRole(BindingItem.Position),
                            Adress = new Adress
                            {
                                City = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(BindingItem.Adress.City.ToLower()),
                                Voivodeship = ConvertIfNotEmpty(BindingItem.Adress.Voivodeship),
                                District = ConvertIfNotEmpty(BindingItem.Adress.District),
                                PostCode = BindingItem.Adress.PostCode,
                                Street = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(BindingItem.Adress.Street.ToLower()),
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
                        HasedPassword = BindingItem.Password;
                    }
                    else
                    {
                        Byte[] InBytePassword = Encoding.UTF8.GetBytes(Password);
                        HasedPassword = SHA256.ComputeHash(InBytePassword);
                    }

                    Employee updatedEmployee = new Employee
                    {
                        ID = (int)BindingItem.ID,
                        Email = BindingItem.Email.ToLower(),
                        Password = HasedPassword,
                        Name = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(BindingItem.Name.ToLower()),
                        Surname = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(BindingItem.Surname.ToLower()),
                        Position = BindingItem.Position,
                        UserRole = PositionToUserRole(BindingItem.Position),
                        Adress = new Adress
                        {
                            ID = (int)BindingItem.Adress.ID,
                            City = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(BindingItem.Adress.City.ToLower()),
                            Voivodeship = ConvertIfNotEmpty(BindingItem.Adress.Voivodeship),
                            District = ConvertIfNotEmpty(BindingItem.Adress.District),
                            PostCode = BindingItem.Adress.PostCode,
                            Street = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(BindingItem.Adress.Street.ToLower()),
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
            if (BindingItem.ID <= 0)
            {
                if (string.IsNullOrEmpty(validationErrors1) && (Password.Length >= 6))

                {
                    return String.Empty;
                }
                else
                {
                    string returnString = $"{validationErrors1}\r\n";
                    if (Password.Length < 6)
                    {
                        returnString += $"Haslo powinno posiadać co najmniej 6 znaków !";
                    }

                    returnString += "\r\n".Trim();

                    return returnString;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(validationErrors1) && ((Password.Length >= 6 || string.IsNullOrEmpty(Password))))

                {
                    return String.Empty;
                }
                else
                {
                    string returnString = $"{validationErrors1}\r\n";
                    if (!string.IsNullOrEmpty(Password) && Password.Length < 6)
                    {
                        returnString += $"Haslo powinno posiadać co najmniej 6 znaków !";
                    }

                    returnString += "\r\n".Trim();

                    return returnString;
                }
            }


        }

        private String ConvertIfNotEmpty(String name)
        {
            if (string.IsNullOrEmpty(name)) return name;
            else
            {
                return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());
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
