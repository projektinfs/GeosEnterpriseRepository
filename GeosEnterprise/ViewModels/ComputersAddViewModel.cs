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
using GeosEnterprise.Validators;
using FluentValidation;
using FluentValidation.Results;

namespace GeosEnterprise.ViewModels
{
    public class ComputersAddViewModel : PropertyChangedBase
    {
        public ICommand OKButtonCommand { get; set; }
        public ICommand CancelButtonCommand { get; set; }
        public ICommand NewClientButtonCommand { get; set; }
        public RepairDTO BindingItem { get; set; }
        public int SelectedClientIndex { get; set; }

        public IList<ClientDTO> Clients
        {
            get
            {
                return Repositories.ClientRepository.GetAllCurrent().Select(p => ClientDTO.ToDTO(p)).ToList();
            }
        }

        public ComputersAddViewModel(int? repairID)
        {
            if (repairID != null)
            {
                BindingItem = RepairDTO.ToDTO(Repositories.RepairsRepository.GetById((int)repairID));
                SelectedClientIndex = Clients.ToList().FindIndex(p => p.ID == BindingItem.Client.ID);
            }
            else
            {
                BindingItem = new RepairDTO();
                BindingItem.Computer = new ComputerDTO();
                BindingItem.Client = new ClientDTO();
            }
            OKButtonCommand = new RelayCommand<Window>(OK);
            CancelButtonCommand = new RelayCommand<Window>(Cancel);
            NewClientButtonCommand = new RelayCommand<Window>(NewClient);
        }

        public void OK(Window window)
        {
            string errors = DoValidation();
            if (string.IsNullOrEmpty(errors))
            {
                if (BindingItem.ID > 0)
                {
                    Repositories.RepairsRepository.Edit(BindingItem);
                }
                else
                {
                    Repositories.RepairsRepository.Add(RepairDTO.FromDTO(BindingItem));
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
            window.DialogResult = false;
            window?.Close();
        }

        public void NewClient(Window window)
        {
            var newClientAdd = new Views.ClientsAdd();
            if (newClientAdd.ShowDialog() == true)
            {
                OnPropertyChanged("Clients");
                var clientsID = Clients.Last()?.ID;
                SelectedClientIndex = Clients.ToList().FindIndex(p => p.ID == clientsID);
            }
        }

        private string DoValidation()
        {
            var validationErrors1 = ValidatorTools.DoValidation(BindingItem.Computer, new ComputerValidator());
            string validationErrors2 = String.Empty;

            if (BindingItem.Client.ID == 0)
            {
                validationErrors2 = "Nie wybrano klienta.";
            }

            if (string.IsNullOrEmpty(validationErrors1) && string.IsNullOrEmpty(validationErrors2))
            {
                return String.Empty;
            }
            else
            {
                string returnString = $"{validationErrors1}\r\n{validationErrors2}".Trim();
                return returnString;
            }
        }
    }
}