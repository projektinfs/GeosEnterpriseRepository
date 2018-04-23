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
                //BindingItem.Client = new ClientDTO();
            }
            OKButtonCommand = new RelayCommand<Window>(OK);
            CancelButtonCommand = new RelayCommand<Window>(Cancel);
            NewClientButtonCommand = new RelayCommand<Window>(NewClient);
        }

        public void OK(Window window)
        {
            if (BindingItem.ID > 0)
            {
                Repositories.RepairsRepository.Edit(BindingItem);
            }
            else
            {
                Repositories.RepairsRepository.Add(new Repair
                {
                    Description = BindingItem.Description,
                    Computer = new Computer
                    {
                        SerialNumber = BindingItem.Computer.SerialNumber,
                        Name = BindingItem.Computer.Name
                    }
                    
                });
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
    }
}