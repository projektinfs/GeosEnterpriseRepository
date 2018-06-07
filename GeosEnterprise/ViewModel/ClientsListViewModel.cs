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
using System.Windows.Data;

namespace GeosEnterprise.ViewModel
{
    public class ClientsListViewModel : ViewModelBase
    {
        public ICommand DateTimeNowButtonCommand { get; set; }
        public ICommand ResetButtonCommand { get; set; }
        public ICommand AddButtonCommand { get; set; }
        public ICommand DeleteButtonCommand { get; set; }
        public ICommand EditButtonCommand { get; set; }
        public ICommand SearchButtonCommand { get; private set; }
        public ICommand InfoButtonCommand { get; private set; }


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
                    RaisePropertyChanged("TimeToBindingItem");
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
                    RaisePropertyChanged("TimeFromBindingItem");
                }
            }
        }

        private string _searchString;
        public string SearchString
        {
            get { return _searchString; }
            set { _searchString = value; RaisePropertyChanged("SearchString"); }
        }

        private ICollectionView _items;
        public ICollectionView Items
        {
            get { return CollectionViewSource.GetDefaultView(_myDataSource); }
        }

        public ClientsListViewModel()
        {
            DateTimeNowButtonCommand = new RelayCommand<object>(Now);
            ResetButtonCommand = new RelayCommand<object>(Reset);
            SearchButtonCommand = new RelayCommand<object>(OnSearch);
            AddButtonCommand = new RelayCommand<object>(Add);
            DeleteButtonCommand = new RelayCommand<object>(Delete);
            EditButtonCommand = new RelayCommand<object>(Edit);
            InfoButtonCommand = new RelayCommand<object>(Info);

            Name = Authorization.AcctualEmployee.Name + " " + Authorization.AcctualEmployee.Surname;
            _myDataSource = new ObservableCollection<ClientDTO>(ClientRepository.GetAllCurrent().Select(p => ClientDTO.ToDTO(p)));

        }

        public ObservableCollection<ClientDTO> _myDataSource = new ObservableCollection<ClientDTO>();

        public void Now(object obj)
        {
            TimeToBindingItem = DateTime.Now;
        }

        public void Reset(object obj)
        {
            TimeToBindingItem = null;
            TimeFromBindingItem = null;
        }

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
                    Repositories.ClientRepository.Delete(clientDTO);
                    RaisePropertyChanged("Items");
                }
            }
            else
            {
                Config.MsgBoxNothingSelectedMessage();
            }
        }

        private void Info(object obj)
        {
            var clientDTO = SelectedItem as ClientDTO;
            if (clientDTO != null)
            {
                Window addNewClientWindow = new ClientsInfo(clientDTO.ID);
                addNewClientWindow.ShowDialog();
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

        private void OnSearch(object obj)
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                if (TimeFromBindingItem == null || TimeToBindingItem == null)
                {
                    Items.Filter = (item) => {
                        return ((item as ClientDTO).Name.Contains(SearchString)
                            || (item as ClientDTO).Surname.Contains(SearchString)
                            || (item as ClientDTO).ClientContact.Email.Contains(SearchString)
                            || (item as ClientDTO).ClientContact.Phone.Contains(SearchString));
                    };
                }
                else
                {
                    Items.Filter = (item) => {

                        return ((item as ClientDTO).Name.Contains(SearchString)
                            || (item as ClientDTO).Surname.Contains(SearchString)
                            || (item as ClientDTO).ClientContact.Email.Contains(SearchString)
                            || (item as ClientDTO).ClientContact.Phone.Contains(SearchString))
                            && ((item as ClientDTO).CreatedDate >= timeFromBindingItem.Value)
                            && ((item as ClientDTO).CreatedDate <= timeToBindingItem.Value);
                    };
                }
            }
            else
            {
                if (TimeFromBindingItem == null || TimeToBindingItem == null)
                {
                    Items.Filter = (item) => {
                        return (item as ClientDTO).DeletedDate == null;
                    };
                }
                else
                {
                    Items.Filter = (item) => {
                        return ((item as ClientDTO).CreatedDate >= timeFromBindingItem.Value)
                        && ((item as ClientDTO).CreatedDate <= timeToBindingItem.Value);
                    };
                }
            }
        }

    }
}
