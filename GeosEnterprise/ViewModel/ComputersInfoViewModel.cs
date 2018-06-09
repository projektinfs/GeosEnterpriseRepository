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
using GalaSoft.MvvmLight;

namespace GeosEnterprise.ViewModel
{
    public class ComputersInfoViewModel : ViewModelBase
    {
        public ICommand CancelButtonCommand { get; set; }
        public RepairDTO BindingItem { get; set; }
        public string ClientFullName { get; set; }
        public string RepairCosts { get; set; }
        public DateTime? EstimatedTime { get; set; }


        public ComputersInfoViewModel(int? repairID)
        {
            if (repairID != null)
            {
                BindingItem = RepairDTO.ToDTO(Repositories.RepairsRepository.GetById((int)repairID));
                ClientFullName = BindingItem.Client.Name + " " + BindingItem.Client.Surname;
                RepairCosts = BindingItem.RepairCosts.ToString();
                EstimatedTime = BindingItem.EstimatedDate;
            }
            else
            {
                BindingItem = new RepairDTO();
                BindingItem.Computer = new ComputerDTO();
            }


            CancelButtonCommand = new RelayCommand<Window>(Cancel);
        }

        public void Cancel(Window window)
        {
            window?.Close();
        }
    }
}