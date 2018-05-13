using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using GeosEnterprise.DTO;
using GeosEnterprise.Views;
using GeosEnterprise.Commands;
using System.Windows.Data;
using System.Collections.ObjectModel;

namespace GeosEnterprise.ViewModels
{
    public class ComputersListViewModel : INotifyPropertyChanged
    {
        public ComputersListViewModel()
        {
            AddButtonCommand = new RelayCommand<object>(Add);
            EditButtonCommand = new RelayCommand<object>(Edit);
            DeleteButtonCommand = new RelayCommand<object>(Delete);
            InfoButtonCommand = new RelayCommand<object>(Info);
            DateTimeNowButtonCommand = new RelayCommand<object>(Now);
            ResetButtonCommand = new RelayCommand<object>(Reset);
            SearchButtonCommand = new RelayCommand<object>(OnSearch);
            _myDataSource = new ObservableCollection<RepairDTO>(Repositories.RepairsRepository.GetAllCurrent().Select(p => DTO.RepairDTO.ToDTO(p)));

        }

        public ICommand AddButtonCommand { get; private set; }
        public ICommand EditButtonCommand { get; private set; }
        public ICommand DeleteButtonCommand { get; set; }
        public ICommand InfoButtonCommand { get; set; }
        public ICommand DateTimeNowButtonCommand { get; set; }
        public ICommand ResetButtonCommand { get; set; }
        public ICommand SearchButtonCommand { get; private set; }

        public ObservableCollection<RepairDTO> _myDataSource = new ObservableCollection<RepairDTO>();

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

        public void Add(object obj)
        {
            Window addNewRepairWindow = new ComputersAdd();
            if (addNewRepairWindow.ShowDialog() == true)
            {
                _myDataSource = new ObservableCollection<RepairDTO>(Repositories.RepairsRepository.GetAllCurrent().Select(p => DTO.RepairDTO.ToDTO(p)));
                OnPropertyChanged("Items");
            }
        }

        public void Edit(object obj)
        {
            var repairDTO = SelectedItem as RepairDTO;

            if (repairDTO != null)
            {
                Window editRepairWindow = new ComputersAdd(repairDTO.ID);
                if (editRepairWindow.ShowDialog() == true)
                {
                    _myDataSource = new ObservableCollection<RepairDTO>(Repositories.RepairsRepository.GetAllCurrent().Select(p => DTO.RepairDTO.ToDTO(p)));
                    OnPropertyChanged("Items");
                }
            }
            else
            {
                Config.MsgBoxNothingSelectedMessage();
            }
        }

        public void Delete(object obj)
        {
            var repairDTO = SelectedItem as RepairDTO;
            if (repairDTO != null)
            {
                if (MessageBox.Show($"Czy na pewno chcesz usunąć zlecenie naprawy\r\n(Nazwa komputera: {repairDTO.Computer.Name})",
                    "Usunięcie zlecenia", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    Repositories.RepairsRepository.Delete(repairDTO.ID);
                    _myDataSource = new ObservableCollection<RepairDTO>(Repositories.RepairsRepository.GetAllCurrent().Select(p => DTO.RepairDTO.ToDTO(p)));
                    OnPropertyChanged("Items");
                }
            }
            else
            {
                Config.MsgBoxNothingSelectedMessage();
            }
        }


        public void Info(object obj)
        {
            var repairDTO = SelectedItem as RepairDTO;
            if (repairDTO != null)
            {
                Window infoRepairWindow = new ComputersInfo(repairDTO.ID);
                infoRepairWindow.Show();
            }
            else
            {
                Config.MsgBoxNothingSelectedMessage();
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

        private void OnSearch(object obj)
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                if (TimeFromBindingItem == null || TimeToBindingItem == null)
                {
                    Items.Filter = (item) => {
                        return ((item as RepairDTO).Computer.SerialNumber.Contains(SearchString)
                            || (item as RepairDTO).Computer.Name.Contains(SearchString)
                            || (item as RepairDTO).Client.Name.Contains(SearchString)
                            || (item as RepairDTO).OrderNumber.Contains(SearchString));
                    };
                }
                else
                {
                    Items.Filter = (item) => {

                        return ((item as RepairDTO).Computer.SerialNumber.Contains(SearchString)
                            || (item as RepairDTO).Computer.Name.Contains(SearchString)
                            || (item as RepairDTO).Client.Name.Contains(SearchString)
                            || (item as RepairDTO).OrderNumber.Contains(SearchString)) 
                            && ((item as RepairDTO).CreatedDate >= timeFromBindingItem.Value)
                            && ((item as RepairDTO).CreatedDate <= timeToBindingItem.Value); 
                    };
                }     
            }
            else
            {
                if( TimeFromBindingItem == null || TimeToBindingItem == null )
                {
                    Items.Filter = (item) => {
                        return (item as RepairDTO).RealizationDate == null;
                    };
                }
                else
                {
                    Items.Filter = (item) => {
                        return ((item as RepairDTO).CreatedDate >= timeFromBindingItem.Value)
                        && ((item as RepairDTO).CreatedDate <= timeToBindingItem.Value);
                    };
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}