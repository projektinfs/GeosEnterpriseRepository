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
    public class ClientsInfoViewModel : ViewModelBase
    {
        public ICommand CancelButtonCommand { get; set; }
        public ClientDTO BindingItem { get; set; }

        public ClientsInfoViewModel(int? clientID)
        {
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
            CancelButtonCommand = new RelayCommand<Window>(Cancel);
        }

        public void Cancel(Window window)
        {
            window?.Close();
        }
    }
}