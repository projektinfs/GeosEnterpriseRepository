using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using GeosEnterprise.DTO;
using GeosEnterprise.Repositories;
using GeosEnterprise.Views;
using GeosEnterprise.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Prism.Commands;
using System.Runtime.CompilerServices;
using System.Windows.Data;

namespace GeosEnterprise.ViewModels
{
    public class EmployeesListViewModel : NotificationObject, INotifyPropertyChanged
    {
        public EmployeesListViewModel()
        {
            DateTimeNowButtonCommand = new RelayCommand<object>(Now);
            ResetButtonCommand = new RelayCommand<object>(Reset);
            AddButtonCommand = new RelayCommand<object>(Add);
            DeleteButtonCommand = new RelayCommand<object>(Delete);
            EditButtonCommand = new RelayCommand<object>(Edit);
            InfoButtonCommand = new RelayCommand<object>(Info);
            SearchButtonCommand = new DelegateCommand(OnSearch);
            _myDataSource = new ObservableCollection<EmployeeDTO>(EmployeeRepository.GetAllCurrent().Select(p => EmployeeDTO.ToDTO(p)));
        }

        public ICommand DateTimeNowButtonCommand { get; set; }
        public ICommand ResetButtonCommand { get; set; }
        public ICommand AddButtonCommand { get; set; }
        public ICommand DeleteButtonCommand { get; set; }
        public ICommand EditButtonCommand { get; set; }
        public ICommand InfoButtonCommand { get; set; }
        public ICommand SearchButtonCommand { get; private set; }

        public ObservableCollection<EmployeeDTO> _myDataSource = new ObservableCollection<EmployeeDTO>();

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
            set { _searchString = value; RaisePropertyChanged(() => SearchButtonCommand); }
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

        private void Add(object obj)
        {
            Window addNewEmployeeWindow = new EmployeesAdd();
            if (addNewEmployeeWindow.ShowDialog() == true)
            {
                OnPropertyChanged("Items");
            }

        }

        private void Delete(object obj)
        {
            var employeeDTO = SelectedItem as EmployeeDTO;
            if (employeeDTO != null)
            {
                if (MessageBox.Show($"Czy na pewno chcesz usunąć pracownika\r\n{employeeDTO.Name} {employeeDTO.Surname}",
                    "Usunięcie pracownika", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    Repositories.EmployeeRepository.Delete(employeeDTO.ID);
                    OnPropertyChanged("Items");
                }
                              
            }
           else
            {
                   Config.MsgBoxNothingSelectedMessage();
            }
            

        }

        private void Edit(object obj)
        {
            var employeeDTO = SelectedItem as EmployeeDTO;
            if (employeeDTO != null)
            {
                Window addNewEmployeeWindow = new EmployeesAdd(employeeDTO.ID);
                if (addNewEmployeeWindow.ShowDialog() == true)
                {
                    OnPropertyChanged("Items");
                }

            }
            else
            {
                Config.MsgBoxNothingSelectedMessage();
            }
        }

        private void Info(object obj)
        {
            var employeeDTO = SelectedItem as EmployeeDTO;
            if (employeeDTO != null)
            {
                Window addNewEmployeeWindow = new EmployeesInfo(employeeDTO.ID);
                addNewEmployeeWindow.ShowDialog();
            }
            else
            {
                Config.MsgBoxNothingSelectedMessage();
            }
        }

        private void OnSearch()
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                if (TimeFromBindingItem == null || TimeToBindingItem == null)
                {
                    Items.Filter = (item) => {
                        return ((item as EmployeeDTO).Name.Contains(SearchString)
                            || (item as EmployeeDTO).Surname.Contains(SearchString)
                            || (item as EmployeeDTO).Email.Contains(SearchString)
                            || (item as EmployeeDTO).EmployeeContact.Phone.Contains(SearchString)
                            || (item as EmployeeDTO).Position.Contains(SearchString));
                    };
                }
                else
                {
                    Items.Filter = (item) => {

                        return ((item as EmployeeDTO).Name.Contains(SearchString)
                            || (item as EmployeeDTO).Surname.Contains(SearchString)
                            || (item as EmployeeDTO).Email.Contains(SearchString)
                            || (item as EmployeeDTO).EmployeeContact.Phone.Contains(SearchString)
                            || (item as EmployeeDTO).Position.Contains(SearchString))
                            && ((item as EmployeeDTO).CreatedDate >= timeFromBindingItem.Value)
                            && ((item as EmployeeDTO).CreatedDate <= timeToBindingItem.Value);
                    };
                }
            }
            else
            {
                if (TimeFromBindingItem == null || TimeToBindingItem == null)
                {
                    Items.Filter = (item) => {
                        return (item as EmployeeDTO).DeletedDate == null;
                    };
                }
                else
                {
                    Items.Filter = (item) => {
                        return ((item as EmployeeDTO).CreatedDate >= timeFromBindingItem.Value)
                        && ((item as EmployeeDTO).CreatedDate <= timeToBindingItem.Value);
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

