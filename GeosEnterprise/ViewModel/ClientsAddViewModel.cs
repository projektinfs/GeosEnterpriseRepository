using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeosEnterprise.DBO;
using GeosEnterprise.DTO;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using GeosEnterprise.Commands;
using GeosEnterprise.Validators;
using FluentValidation;
using FluentValidation.Results;
using GalaSoft.MvvmLight;

namespace GeosEnterprise.ViewModel
{
    public class ClientsAddViewModel :  ViewModelBase
    {

        public ICommand OKButtonCommand { get; set; }
        public ICommand CancelButtonCommand { get; set; }
        public ClientDTO BindingItem { get; set; }
        public bool IsAdminMode { get; set; }
        public int PositionIndex {get; set; }
        public Employee currentEmployee { get; set; }

        public ClientsAddViewModel(int? clientID)
        {
            currentEmployee = Authorization.AcctualEmployee;

            if (clientID != null)
            {
                BindingItem = ClientDTO.ToDTO(Repositories.ClientRepository.GetById((int)clientID));
            }
            else
            {
                BindingItem = new ClientDTO();
                BindingItem.ClientAdress = new ClientAdressDTO();
                BindingItem.ClientContact = new ClientContactDTO();
            }
            OKButtonCommand = new RelayCommand<Window>(Save);
            CancelButtonCommand = new RelayCommand<Window>(Cancel);
            IsAdminMode = true;
        }

        public ClientsAddViewModel(int clientID, bool IsAdminMode)
        {
            BindingItem = ClientDTO.ToDTO(Repositories.ClientRepository.GetById((int)clientID));
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
                RaisePropertyChanged("EmailTaken");
            }
        }

        public void Save(Window window)
        {
            string errors = DoValidation();

            if (string.IsNullOrEmpty(errors))
            {
                if (BindingItem.ID <= 0)
                {
                    if (Repositories.ClientRepository.GetByEmail(BindingItem.ClientContact.Email.ToString()) != null)
                    {
                        EmailTaken = "Podany email jest już zajęty!";
                        return;
                    }
                    else
                    {
                        Repositories.ClientRepository.Add(new Client
                        {
                            Name = Capitalize(BindingItem.Name) ,
                            Surname = Capitalize(BindingItem.Surname),
                            ClientAdress = new ClientAdress
                            {
                                City = Capitalize(BindingItem.ClientAdress.City),
                                Voivodeship = Capitalize(BindingItem.ClientAdress.Voivodeship),
                                District = Capitalize(BindingItem.ClientAdress.District),
                                PostCode = BindingItem.ClientAdress.PostCode,
                                Street = Capitalize(BindingItem.ClientAdress.Street),
                                BuildingNumber = BindingItem.ClientAdress.BuildingNumber,
                                AppartamentNumber = BindingItem.ClientAdress.AppartamentNumber
                            },
                            ClientContact = new ClientContact
                            {
                                Phone = BindingItem.ClientContact.Phone,
                                Fax = BindingItem.ClientContact.Fax,
                                Www = BindingItem.ClientContact.Www,
                                Email = BindingItem.ClientContact.Email
                            }
                        });
                    }
                }
                else
                {
                    Client updatedClient = new Client
                    {
                        ID = (int)BindingItem.ID,
                        Name = Capitalize(BindingItem.Name),
                        Surname = Capitalize(BindingItem.Surname),
                        ClientAdress = new ClientAdress
                        {
                            ID = (int)BindingItem.ClientAdress.ID,
                            City = Capitalize(BindingItem.ClientAdress.City),
                            Voivodeship = Capitalize(BindingItem.ClientAdress.Voivodeship),
                            District = Capitalize(BindingItem.ClientAdress.District),
                            PostCode = BindingItem.ClientAdress.PostCode,
                            Street = Capitalize(BindingItem.ClientAdress.Street),
                            BuildingNumber = BindingItem.ClientAdress.BuildingNumber,
                            AppartamentNumber = BindingItem.ClientAdress.AppartamentNumber
                        },
                        ClientContact = new ClientContact
                        {
                            ID = (int)BindingItem.ClientContact.ID,
                            Phone = BindingItem.ClientContact.Phone,
                            Fax = BindingItem.ClientContact.Fax,
                            Www = BindingItem.ClientContact.Www,
                            Email = BindingItem.ClientContact.Email
                        }
                    };
                    Repositories.ClientRepository.Edit(updatedClient);
                }
            }
            else
            {
                Config.MsgBoxValidationMessage(errors);
                return;
            }
            window.DialogResult = true;
            window?.Close();
        }

        public void Cancel(Window window)
        {
            window?.Close();
        }
        private string Capitalize(string input)
        {
            return input.First().ToString().ToUpper() + input.Substring(1);
        }

        private string DoValidation()
        {
            var validationErrors1 = ValidatorTools.DoValidation(BindingItem, new ClientValidator());

            if (string.IsNullOrEmpty(validationErrors1))
            {
                return String.Empty;
            }
            else
            {
                string returnString = $"{validationErrors1}\r\n".Trim();
                return returnString;
            }
        }
    }
}
