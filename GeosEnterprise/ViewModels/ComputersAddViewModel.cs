using GeosEnterprise.DBO;
using GeosEnterprise.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GeosEnterprise.ViewModels
{
    public class ComputersAddViewModel
    {
        public ICommand OKButtonCommand { get; set; }
        public ICommand CancelButtonCommand { get; set; }
        public RepairDTO BindingItem { get; set; }

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
            }
            OKButtonCommand = new RelayCommand<Window>(OK);
            CancelButtonCommand = new RelayCommand<Window>(Cancel);
        }

        public void OK(Window window)
        {
            if (BindingItem.ID > 0)
            {
                Repositories.RepairsRepository.Edit(new Repair
                {
                    ID = (int)BindingItem.ID,
                    Description = BindingItem.Description,
                    ComputerID = BindingItem.ComputerID,
                    Computer = new Computer
                    {
                        SerialNumber = BindingItem.Computer.SerialNumber,
                        Name = BindingItem.Computer.Name
                    }
                });
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
    }
}