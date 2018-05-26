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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using GeosEnterprise.DBO;
using GalaSoft.MvvmLight;

namespace GeosEnterprise.ViewModel
{
    public class ComputersListViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public String Name { get; set; }


        public ComputersListViewModel()
        {
            AddButtonCommand = new RelayCommand<object>(Add);
            EditButtonCommand = new RelayCommand<object>(Edit);
            DeleteButtonCommand = new RelayCommand<object>(Delete);
            InfoButtonCommand = new RelayCommand<object>(Info);
            TakeButtonCommand = new RelayCommand<object>(Take);
            ReportButtonCommand = new RelayCommand<object>(GenerateReport);
            DateTimeNowButtonCommand = new RelayCommand<object>(Now);
            ResetButtonCommand = new RelayCommand<object>(Reset);
            SearchButtonCommand = new RelayCommand<object>(OnSearch);
            _myDataSource = DataSourceHelper;
            Name = Authorization.AcctualEmployee.Name + " " + Authorization.AcctualEmployee.Surname;



        }

        public ICommand AddButtonCommand { get; private set; }
        public ICommand EditButtonCommand { get; private set; }
        public ICommand DeleteButtonCommand { get; set; }
        public ICommand InfoButtonCommand { get; set; }
        public ICommand TakeButtonCommand { get; set; }
        public ICommand ReportButtonCommand { get; set; }
        public ICommand DateTimeNowButtonCommand { get; set; }
        public ICommand ResetButtonCommand { get; set; }
        public ICommand SearchButtonCommand { get; private set; }

        public ObservableCollection<RepairDTO> _myDataSource = new ObservableCollection<RepairDTO>();

        public object SelectedItem { get; set; }

        private ObservableCollection<RepairDTO> DataSourceHelper
        {
            get
            {
                return new ObservableCollection<RepairDTO>(Repositories.RepairsRepository
                .GetAllCurrent()
                .Where(p => p.Status == DBO.RepairStatus.Reported)
                .Select(p => DTO.RepairDTO.ToDTO(p)));
            }
        }

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
                    NotifyPropertyChanged("TimeToBindingItem");
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
                    NotifyPropertyChanged("TimeFromBindingItem");
                }
            }
        }

        private string _searchString;
        public string SearchString
        {
            get { return _searchString; }
            set { _searchString = value; NotifyPropertyChanged("SearchString"); }
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
                _myDataSource = DataSourceHelper;
                NotifyPropertyChanged("Items");
            }
        }

        public void Take(object obj)
        {
            var repairDTO = SelectedItem as RepairDTO;

            if (repairDTO != null)
            {
                // to trzeba będzie przenieść, żeby dopiero po wpisaniu w grafik się aktualizowało
                repairDTO.Serviceman = EmployeeDTO.ToDTO(Authorization.AcctualEmployee);
                repairDTO.ServicemanID = repairDTO.Serviceman.ID;
                repairDTO.Status = DBO.RepairStatus.InProcess;

                Repositories.RepairsRepository.Edit(repairDTO);
                _myDataSource = DataSourceHelper;
                NotifyPropertyChanged("Items");
            }
            else
            {
                Config.MsgBoxNothingSelectedMessage();
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
                    _myDataSource = DataSourceHelper;
                    NotifyPropertyChanged("Items");
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
                    _myDataSource = DataSourceHelper;
                    NotifyPropertyChanged("Items");
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

        public void GenerateReport(object obj)
        {
            var repairDTO = SelectedItem as RepairDTO;
            if (repairDTO != null)
            {
                string pdfPath = $"{repairDTO.OrderNumber.Replace("/", "-")}.pdf";

                var text = Properties.Resources.OrderConfirmation
                    .Replace("{{numer_zlecenia}}", repairDTO.OrderNumber)
                    .Replace("{{nazwa}}", repairDTO.Computer.Name)
                    .Replace("{{nr_seryjny}}", repairDTO.Computer.SerialNumber)
                    .Replace("{{klient}}", $"\r\n{repairDTO.Client.FullName}")
                    .Replace("{{koszt}}", repairDTO.FinalCosts.ToString())
                    .Replace("{{pracownik}}", repairDTO.Dealer.FullName)
                    .Replace("{{data}}", repairDTO.CreatedDate.Value.Date.ToShortDateString())
                    .Replace("{{opis}}", repairDTO.Description);

                Util.CreatePDF(text, pdfPath);
                System.Diagnostics.Process.Start(pdfPath);
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

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}