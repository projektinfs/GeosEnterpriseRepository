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
    public class ComputersAddViewModel
    {
        public ICommand OKButtonCommand { get; set; }
        public ICommand CancelButtonCommand { get; set; }
        public ICommand NewClientButtonCommand { get; set; }
        public RepairDTO BindingItem { get; set; }

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
           
            window?.Close();
        }

        public void Cancel(Window window)
        {
            window?.Close();
        }

        public void NewClient(Window window)
        {
            var newClientAdd = new Views.ClientsAdd();
            newClientAdd.Show();
        }

        private string DoValidation()
        {
            var validationErrors1 = ValidatorTools.DoValidation(BindingItem.Computer, new ComputerValidator());
            var validationErrors2 = ValidatorTools.DoValidation(BindingItem, new RepairValidator());

            if (string.IsNullOrEmpty(validationErrors1) && string.IsNullOrEmpty(validationErrors2))
            {
                return String.Empty;
            }
            else
            {
                return validationErrors1 + "\r\n" + validationErrors2;
            }
        }
    }
}