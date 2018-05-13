using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.ComponentModel;
using GeosEnterprise.DTO;
using GeosEnterprise.Repositories;
using GeosEnterprise.Commands;
using System.Windows.Data;
using System.Runtime.CompilerServices;

namespace GeosEnterprise.ViewModels
{
    public class ClientsListViewModel : INotifyPropertyChanged
    {
        public ClientsListViewModel()
        {
            DateTimeNowButtonCommand = new RelayCommand<object>(Now);
            ResetButtonCommand = new RelayCommand<object>(Reset);
            SearchButtonCommand = new RelayCommand<object>(OnSearch);
            _myDataSource = new ObservableCollection<ClientDTO>(ClientRepository.GetAllCurrent().Select(p => DTO.ClientDTO.ToDTO(p)));
        }

        public ICommand DateTimeNowButtonCommand { get; set; }
        public ICommand ResetButtonCommand { get; set; }
        public ICommand SearchButtonCommand { get; private set; }

        public ObservableCollection<ClientDTO> _myDataSource = new ObservableCollection<ClientDTO>();

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
                    OnPropertyChanged("TimeToBindingItem");
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
                    OnPropertyChanged("TimeFromBindingItem");
                }
            }
        }

        private string _searchString;
        public string SearchString
        {
            get { return _searchString; }
            set { _searchString = value; OnPropertyChanged("SearchString"); }
        }

        private ICollectionView _items;
        public ICollectionView Items
        {
            get { return CollectionViewSource.GetDefaultView(_myDataSource); }
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

