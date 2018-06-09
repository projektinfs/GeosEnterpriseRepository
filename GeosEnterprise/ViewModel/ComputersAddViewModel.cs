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
using GalaSoft.MvvmLight;
using System.ComponentModel;

namespace GeosEnterprise.ViewModel
{
    public class ComputersAddViewModel : ViewModelBase
    {
        public ICommand OKButtonCommand { get; set; }
        public ICommand CancelButtonCommand { get; set; }
        public ICommand NewClientButtonCommand { get; set; }
        public RepairDTO BindingItem { get; set; }
        public int SelectedClientIndex { get; set; }
        public string RepairCosts { get; set; }
        public DateTime? EstimatedTime { get; set; }

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
                RepairCosts = BindingItem.RepairCosts.ToString();
                EstimatedTime = BindingItem.EstimatedDate;
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
                BindingItem.Description = BindingItem.Description?.Trim();
                BindingItem.Computer.SerialNumber = BindingItem.Computer.SerialNumber.ToUpper();
                BindingItem.RepairCosts = decimal.Parse(RepairCosts?.Replace(".", ","));
                BindingItem.Dealer = EmployeeDTO.ToDTO(Authorization.AcctualEmployee);
                BindingItem.DealerID = Authorization.AcctualEmployee.ID;
                BindingItem.EstimatedDate = EstimatedTime;
                if (BindingItem.ID > 0)
                {
                    Repositories.RepairsRepository.Update(BindingItem);
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
                RaisePropertyChanged("Clients");
                var clientsID = Clients.Last()?.ID;
                SelectedClientIndex = Clients.ToList().FindIndex(p => p.ID == clientsID);
            }
        }

        private string DoValidation()
        {
            decimal repairCosts = 0;
            string validationErrors3 = String.Empty;
            if (!decimal.TryParse(RepairCosts?.Replace(".", ","), out repairCosts))
            {
                validationErrors3 = "Niepoprawny format kosztu naprawy.";
            }

            var validationErrors1 = ValidatorTools.DoValidation(BindingItem.Computer, new ComputerValidator());
            string validationErrors2 = String.Empty;

            if (BindingItem.Client.ID == 0)
            {
                validationErrors2 = "Nie wybrano klienta.";
            }

            if (string.IsNullOrEmpty(validationErrors1) && string.IsNullOrEmpty(validationErrors2) && string.IsNullOrEmpty(validationErrors3))
            {
                return String.Empty;
            }
            else
            {
                string returnString = $"{validationErrors1}\r\n{validationErrors2}\r\n{validationErrors3}".Trim();
                return returnString;
            }
        }
    }
}