using GalaSoft.MvvmLight;
using GeosEnterprise.Commands;
using GeosEnterprise.DTO;
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
    public class AccountantPanelViewModel : ViewModelBase
    {
        public String Name { get; set; }

        public AccountantPanelViewModel()
        {
            DateTimeNowButtonCommand = new RelayCommand<object>(Now);
            ResetButtonCommand = new RelayCommand<object>(Reset);
            ReportButtonCommand = new RelayCommand<object>(GenerateReport);
            SearchButtonCommand = new RelayCommand<object>(OnSearch);
            _myDataSource = DataSourceHelper;
            Name = Authorization.AcctualEmployee.Name + " " + Authorization.AcctualEmployee.Surname;
        }

        public ICommand DateTimeNowButtonCommand { get; set; }
        public ICommand ResetButtonCommand { get; set; }
        public ICommand ReportButtonCommand { get; set; }
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


        public void Now(object obj)
        {
            TimeToBindingItem = DateTime.Now;
        }

        public void Reset(object obj)
        {
            TimeToBindingItem = null;
            TimeFromBindingItem = null;
        }

        public void GenerateReport(object obj)
        {
            var repairDTO = SelectedItem as RepairDTO;
            if (repairDTO != null)
            {
                string pdfPath = $"{repairDTO.OrderNumber.Replace("/", "-")}_VAT.pdf";

                var text = Properties.Resources.AccountantReport
                    .Replace("{{data}}", DateTime.Now.ToShortDateString())
                    .Replace("{{termin}}", DateTime.Now.AddDays(7).ToShortDateString())
                    .Replace("{{klient}}", $"\r\n{repairDTO.Client.FullName}")
                    .Replace("{{netto}}", (repairDTO.FinalCosts * (decimal)0.77).ToString())
                    .Replace("{{vat}}", (repairDTO.FinalCosts * (decimal)0.23).ToString())
                    .Replace("{{brutto}}", repairDTO.FinalCosts.ToString())
                    .Replace("{{pracownik}}", repairDTO.Dealer.FullName);

                Util.CreatePDF(text, pdfPath);
                System.Diagnostics.Process.Start(pdfPath);
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
                        return ((item as RepairDTO).Client.Name.Contains(SearchString)
                            || (item as RepairDTO).Client.Surname.Contains(SearchString)
                            || (item as RepairDTO).Client.ClientContact.Email.Contains(SearchString)
                            || (item as RepairDTO).OrderNumber.Contains(SearchString)
                            || (item as RepairDTO).FinalCosts.ToString().Contains(SearchString)
                            || (item as RepairDTO).ReplacementsCosts.ToString().Contains(SearchString));
                    };
                }
                else
                {
                    Items.Filter = (item) => {

                        return ((item as RepairDTO).Client.Name.Contains(SearchString)
                            || (item as RepairDTO).Client.Surname.Contains(SearchString)
                            || (item as RepairDTO).Client.ClientContact.Email.Contains(SearchString)
                            || (item as RepairDTO).OrderNumber.Contains(SearchString))
                            || (item as RepairDTO).FinalCosts.ToString().Contains(SearchString)
                            || (item as RepairDTO).ReplacementsCosts.ToString().Contains(SearchString)
                            && ((item as RepairDTO).CreatedDate >= timeFromBindingItem.Value)
                            && ((item as RepairDTO).CreatedDate <= timeToBindingItem.Value);
                    };
                }
            }
            else
            {
                if (TimeFromBindingItem == null || TimeToBindingItem == null)
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
    }
}
