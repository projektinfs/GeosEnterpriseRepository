using GalaSoft.MvvmLight;
using GeosEnterprise.Commands;
using GeosEnterprise.DBO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace GeosEnterprise.ViewModel
{
    public class LogsListViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public LogsListViewModel()
        {
            DateTimeNowButtonCommand = new RelayCommand<object>(Now);
            ResetButtonCommand = new RelayCommand<object>(Reset);
            SearchButtonCommand = new RelayCommand<object>(OnSearch);
            _myDataSource = new ObservableCollection<Log>(Repositories.LogsRepository.GetAllCurrent());

        }

        public object SelectedItem { get; set; }
        public ICommand DateTimeNowButtonCommand { get; set; }
        public ICommand ResetButtonCommand { get; set; }
        public ICommand SearchButtonCommand { get; private set; }


        private ICollectionView _items;
        public ICollectionView Items
        {
            get { return CollectionViewSource.GetDefaultView(_myDataSource); }
        }

        public ObservableCollection<Log> _myDataSource = new ObservableCollection<Log>();

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

        public void Now(object obj)
        {
            TimeToBindingItem = DateTime.Now;
        }

        public void Reset(object obj)
        {
            TimeToBindingItem = null;
            TimeFromBindingItem = null;
        }

        private UserRole MapStringToUserRole(string value)
        {
            if ("kierownik".Contains(value))
                return UserRole.Manager;
            else if ("księgowy".Contains(value) || "ksiegowy".Contains(value))
                return UserRole.Accountant;
            else if ("serwisant".Contains(value))
                return UserRole.Serviceman;
            else if ("sprzedawca".Contains(value))
                return UserRole.Dealer;
            else if ("administrator".Contains(value))
                return UserRole.Administrator;
            else
                return UserRole.Unknown;
        }

        private void OnSearch(object obj)
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                var searchString = SearchString.ToLower();
                List<string> employees = new List<string>();
                var position = MapStringToUserRole(searchString);
                if (position != UserRole.Unknown)
                {
                    employees = Repositories.EmployeeRepository.GetAllCurrent().Where(p => p.UserRole == position).Select(p => p.Email).ToList();
                }
                if (TimeFromBindingItem == null && TimeToBindingItem == null)
                {
                    Items.Filter = (item) =>
                    {
                        return ((item as Log).Value.ToLower().Contains(searchString)
                            || (item as Log).CreatedBy.ToLower().Contains(searchString)
                            || employees.Contains((item as Log).CreatedBy));
                    };
                }
                else
                {
                    if (timeFromBindingItem == null)
                    {
                        timeFromBindingItem = DateTime.Now.AddYears(-10);
                    }
                    if (timeToBindingItem == null)
                    {
                        timeToBindingItem = DateTime.Now.AddYears(10);
                    }
                    Items.Filter = (item) =>
                    {
                        return (((item as Log).Value.ToLower().Contains(searchString)
                            || (item as Log).CreatedBy.ToLower().Contains(searchString)
                            || employees.Contains((item as Log).CreatedBy))
                            && ((item as Log).CreatedDate >= timeFromBindingItem.Value)
                            && ((item as Log).CreatedDate <= timeToBindingItem.Value));
                    };
                }
            }
            else
            {
                if (TimeFromBindingItem != null || TimeToBindingItem != null)
                {
                    if (timeFromBindingItem == null)
                    {
                        timeFromBindingItem = DateTime.Now.AddYears(-10);
                    }
                    if (timeToBindingItem == null)
                    {
                        timeToBindingItem = DateTime.Now.AddYears(10);
                    }
                    Items.Filter = (item) =>
                    {
                        return ((item as Log).CreatedDate >= timeFromBindingItem.Value)
                        && ((item as Log).CreatedDate <= timeToBindingItem.Value);
                    };
                }
                else
                {
                    Items.Filter = (item) =>
                    {
                        return ((item as Log).CreatedBy != null);
                    };
                }
            }
        }
    }
}
