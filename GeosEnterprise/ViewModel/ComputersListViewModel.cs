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
    public class ComputersListViewModel : ViewModelBase
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
            CurrentButtonCommand = new RelayCommand<object>(Current);
            ReportedButtonCommand = new RelayCommand<object>(Reported);
            RepairInfoButtonCommand = new RelayCommand<object>(RepairInfo);
            AcceptedButtonCommand = new RelayCommand<object>(Accepted);
            CompletedButtonCommand = new RelayCommand<object>(Completed);
            AllButtonCommand = new RelayCommand<object>(All);


            if (Authorization.AcctualEmployee.UserRole == UserRole.Dealer)
            {
                Dealer = "False";
            }
            else Dealer = "True";
            if (Authorization.AcctualEmployee.UserRole == UserRole.Serviceman)
            {
                Serviceman = "False";
            }
            else Serviceman = "True";


            RepairInfoVisibility = "Hidden";
            AcceptedVisibility = "Hidden";

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
        public ICommand CurrentButtonCommand { get; set; }
        public ICommand ReportedButtonCommand { get; set; }
        public ICommand RepairInfoButtonCommand { get; set; }
        public ICommand AcceptedButtonCommand { get; set; }
        public ICommand CompletedButtonCommand { get; set; }
        public ICommand AllButtonCommand { get; set; }


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

        private string _serviceman;
        public string Serviceman
        {
            get { return _serviceman; }
            set { _serviceman = value; RaisePropertyChanged("Serviceman"); }
        }

        private string _dealer;
        public string Dealer
        {
            get { return _dealer; }
            set { _dealer = value; RaisePropertyChanged("Dealer"); }
        }

        private string _searchString;
        public string SearchString
        {
            get { return _searchString; }
            set { _searchString = value; RaisePropertyChanged("SearchString"); }
        }

        private string _repairInfoVisibility;
        public string RepairInfoVisibility
        {
            get { return _repairInfoVisibility; }
            set { _repairInfoVisibility = value; RaisePropertyChanged("RepairInfoVisibility"); }
        }

        private string _acceptedVisibility;
        public string AcceptedVisibility
        {
            get { return _acceptedVisibility; }
            set { _acceptedVisibility = value; RaisePropertyChanged("AcceptedVisibility"); }
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
                RaisePropertyChanged("Items");
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

                Repositories.RepairsRepository.Update(repairDTO);
                _myDataSource = DataSourceHelper;
                RaisePropertyChanged("Items");
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
                    RaisePropertyChanged("Items");
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
                    Repositories.RepairsRepository.Delete(repairDTO);
                    _myDataSource = DataSourceHelper;
                    RaisePropertyChanged("Items");
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
                if (TimeFromBindingItem == null && TimeToBindingItem == null)
                {
                    Items.Filter = (item) =>
                    {
                        return ((item as RepairDTO).Computer.SerialNumber.Contains(SearchString)
                            || (item as RepairDTO).Computer.Name.Contains(SearchString)
                            || (item as RepairDTO).Computer.Name.Contains(SearchString)
                            || (item as RepairDTO).Client.Name.Contains(SearchString)
                            || (item as RepairDTO).Client.Surname.Contains(SearchString)
                            || (item as RepairDTO).Client.ClientContact.Email.Contains(SearchString)
                            || (item as RepairDTO).OrderNumber.Contains(SearchString));
                    };
                }
                else if (TimeFromBindingItem == null && TimeToBindingItem != null)
                {
                    Items.Filter = (item) =>
                    {

                        return ((item as RepairDTO).Computer.SerialNumber.Contains(SearchString)
                            || (item as RepairDTO).Computer.Name.Contains(SearchString)
                            || (item as RepairDTO).Client.Name.Contains(SearchString)
                            || (item as RepairDTO).Client.Surname.Contains(SearchString)
                            || (item as RepairDTO).Client.ClientContact.Email.Contains(SearchString)
                            || (item as RepairDTO).OrderNumber.Contains(SearchString))
                            && ((item as RepairDTO).CreatedDate <= timeToBindingItem.Value);
                    };
                }
                else
                {
                    Items.Filter = (item) =>
                    {

                        return ((item as RepairDTO).Computer.SerialNumber.Contains(SearchString)
                            || (item as RepairDTO).Computer.Name.Contains(SearchString)
                            || (item as RepairDTO).Client.Name.Contains(SearchString)
                            || (item as RepairDTO).Client.Surname.Contains(SearchString)
                            || (item as RepairDTO).Client.ClientContact.Email.Contains(SearchString)
                            || (item as RepairDTO).OrderNumber.Contains(SearchString))
                            && ((item as RepairDTO).CreatedDate >= timeFromBindingItem.Value)
                            && ((item as RepairDTO).CreatedDate <= timeToBindingItem.Value);
                    };
                }
            }
            else
            {
                if (TimeFromBindingItem == null && TimeToBindingItem == null)
                {
                    Items.Filter = (item) =>
                    {
                        return (item as RepairDTO).RealizationDate == null;
                    };
                }
                else if (TimeFromBindingItem == null && TimeToBindingItem != null)
                {
                    Items.Filter = (item) =>
                    {
                        return ((item as RepairDTO).CreatedDate <= timeToBindingItem.Value);
                    };
                }
                else
                {
                    Items.Filter = (item) =>
                    {
                        return ((item as RepairDTO).CreatedDate >= timeFromBindingItem.Value)
                        && ((item as RepairDTO).CreatedDate <= timeToBindingItem.Value);
                    };
                }
            }
        }

        private ObservableCollection<RepairDTO> CurrentRepairs
        {
            get
            {
                return new ObservableCollection<RepairDTO>(Repositories.RepairsRepository
                .GetAllCurrent()
                .Where(p => p.Status == DBO.RepairStatus.InProcess)
                .Where(p => p.ServicemanID == Authorization.AcctualEmployee.ID)
                .Select(p => DTO.RepairDTO.ToDTO(p)));
            }
        }

        private ObservableCollection<RepairDTO> CompletedRepairs
        {
            get
            {
                return new ObservableCollection<RepairDTO>(Repositories.RepairsRepository
                .GetAllCurrent()
                .Where(p => p.Status == DBO.RepairStatus.Completed)
                .Select(p => DTO.RepairDTO.ToDTO(p)));
            }
        }

        private ObservableCollection<RepairDTO> AllRepairs
        {
            get
            {
                return new ObservableCollection<RepairDTO>(Repositories.RepairsRepository
                .GetAll()
                .Select(p => DTO.RepairDTO.ToDTO(p)));
            }
        }

        public void Current(object obj)
        {
            _myDataSource = CurrentRepairs;
            RaisePropertyChanged("Items");
            _repairInfoVisibility = "Visible";
            RaisePropertyChanged("RepairInfoVisibility");
            _acceptedVisibility = "Hidden";
            RaisePropertyChanged("AcceptedVisibility");
        }

        public void Reported(object obj)
        {
            _myDataSource = DataSourceHelper;
            RaisePropertyChanged("Items");
            _repairInfoVisibility = "Hidden";
            RaisePropertyChanged("RepairInfoVisibility");
            _acceptedVisibility = "Hidden";
            RaisePropertyChanged("AcceptedVisibility");
        }



        public void RepairInfo(object obj)
        {
            var repairDTO = SelectedItem as RepairDTO;

            if (repairDTO != null)
            {
                Window repairInfoWindow = new ComputersServiceman(repairDTO.ID);
                if (repairInfoWindow.ShowDialog() == true)
                {
                    _myDataSource = DataSourceHelper;
                    RaisePropertyChanged("Items");
                    Current(this);

                }
            }
            else
            {
                Config.MsgBoxNothingSelectedMessage();
            }
        }

        public void Completed(object obj)
        {
            _myDataSource = CompletedRepairs;
            RaisePropertyChanged("Items");
            _acceptedVisibility = "Visible";
            RaisePropertyChanged("AcceptedVisibility");
            _repairInfoVisibility = "Hidden";
            RaisePropertyChanged("RepairInfoVisibility");
        }

        public void All(object obj)
        {
            _myDataSource = AllRepairs;
            RaisePropertyChanged("Items");
            _acceptedVisibility = "Hidden";
            RaisePropertyChanged("AcceptedVisibility");
            _repairInfoVisibility = "Hidden";
            RaisePropertyChanged("RepairInfoVisibility");
        }

        public void Accepted(object obj)
        {
            var repairDTO = SelectedItem as RepairDTO;
            if (repairDTO != null)
            {
                if (MessageBox.Show($"Odbiór komputera: {repairDTO.Computer.Name}",
                    "Zamknięcie zlecenia", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    repairDTO.Status = DBO.RepairStatus.AcceptedByClient;
                    repairDTO.RealizationDate = DateTime.Now;
                    Repositories.RepairsRepository.Update(repairDTO);
                    _myDataSource = CompletedRepairs;
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