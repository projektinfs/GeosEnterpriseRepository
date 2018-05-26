using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using GeosEnterprise.DBO;
using GeosEnterprise.DTO;
using GeosEnterprise.Repositories;
using GeosEnterprise.Views;
using GeosEnterprise.Commands;
using GalaSoft.MvvmLight;

namespace GeosEnterprise.ViewModel
{
    public class ClientsListViewModel : ViewModelBase
    {
        public ICommand DateTimeNowButtonCommand { get; set; }
        public ICommand ResetButtonCommand { get; set; }
        public ICommand AddButtonCommand { get; set; }
        public ICommand DeleteButtonCommand { get; set; }
        public ICommand EditButtonCommand { get; set; }
        public String Name { get; set; }

        public object SelectedItem { get; set; }


        private DateTime? timeToBindingItem;
        public DateTime? TimeToBindingItem
        {
            get
            {
                return timeToBindingItem;
            }
            set
            {
                if (timeToBindingItem != value)
                {
                    timeToBindingItem = value;
                }
            }
        }

        private DateTime? timeFromBindingItem;
        public DateTime? TimeFromBindingItem
        {
            get
            {
                return timeFromBindingItem;
            }
            set
            {
                if (timeFromBindingItem != value)
                {
                    timeFromBindingItem = value;
                }
            }
        }

        public ClientsListViewModel()
        {
            DateTimeNowButtonCommand = new RelayCommand<object>(Now);
            ResetButtonCommand = new RelayCommand<object>(Reset);
            AddButtonCommand = new RelayCommand<object>(Add);
            DeleteButtonCommand = new RelayCommand<object>(Delete);
            EditButtonCommand = new RelayCommand<object>(Edit);
            Name = Authorization.AcctualEmployee.Name + " " + Authorization.AcctualEmployee.Surname;

        }

        public ObservableCollection<ClientDTO> Items
        {
            get
            {
                return new ObservableCollection<ClientDTO>(ClientRepository.GetAllCurrent().Select(p => DTO.ClientDTO.ToDTO(p)));
            }
        }

        public void Now(object obj)
        {
            TimeToBindingItem = DateTime.Now;
        }

        public void Reset(object obj)
        {
            TimeToBindingItem = null;
            TimeFromBindingItem = null;
        }

        //from ClientsList

        private void Add(object obj)
        {
            Window addNewClientWindow = new ClientsAdd();
            if (addNewClientWindow.ShowDialog() == true)
            {
                RaisePropertyChanged("Items");
            }
        }

        private void Delete(object obj)
        {
            var clientDTO = SelectedItem as ClientDTO;
            if (clientDTO != null)
            {
                if (MessageBox.Show($"Czy na pewno chcesz usunąć klienta\r\n{clientDTO.Name} {clientDTO.Surname}",
                    "Usunięcie klienta", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    Repositories.ClientRepository.Delete(clientDTO.ID);
                    RaisePropertyChanged("Items");
                }
            }
            else
            {
                Config.MsgBoxNothingSelectedMessage();
            }
        }

        private void Edit(object obj)
        {
            var clientDTO = SelectedItem as ClientDTO;
            if (clientDTO != null)
            {
                Window addNewClientWindow = new ClientsAdd(clientDTO.ID);
                if (addNewClientWindow.ShowDialog() == true)
                {
                    RaisePropertyChanged("Items");
                }
            }
            else
            {
                Config.MsgBoxNothingSelectedMessage();
            }
        }
    }
}
