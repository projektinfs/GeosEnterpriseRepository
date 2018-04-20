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
    public class ComputersInfoViewModel
    {
        public ICommand CancelButtonCommand { get; set; }
        public RepairDTO BindingItem { get; set; }

        public ComputersInfoViewModel(int? repairID)
        {
            if (repairID != null)
            {
                BindingItem = RepairDTO.ToDTO(Repositories.RepairsRepository.GetById((int)repairID));
            }
            else
            {
                BindingItem = new RepairDTO();
                BindingItem.Computer = new ComputerDTO();
                BindingItem.Computer.Components = new List<ComponentDTO>();
            }


            CancelButtonCommand = new RelayCommand<Window>(Cancel);
        }

        public void Cancel(Window window)
        {
            window?.Close();
        }
    }
}