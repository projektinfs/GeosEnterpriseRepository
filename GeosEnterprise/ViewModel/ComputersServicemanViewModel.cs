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
using GeosEnterprise.Validators;


namespace GeosEnterprise.ViewModel
{
    public class ComputersServicemanViewModel : ViewModelBase
    {
        public ICommand OKButtonCommand { get; set; }
        public ICommand CancelButtonCommand { get; set; }
        public ICommand CompletedButtonCommand { get; set; }
        public RepairDTO BindingItem { get; set; }
        public string RepairCosts { get; set; }
        public string ReplacementsCosts { get; set; }
        public string RepairDescription { get; set; }
        public string ServicemanNote { get; set; }

        public object SelectedItem { get; set; }


        public ComputersServicemanViewModel(int? repairID)
        {
            if (repairID != null)
            {
                BindingItem = RepairDTO.ToDTO(Repositories.RepairsRepository.GetById((int)repairID));
                RepairCosts = BindingItem.RepairCosts.ToString();
                ReplacementsCosts = BindingItem.ReplacementsCosts.ToString();
                ServicemanNote = BindingItem.ServicemanNote?.Trim();

            }
            else
            {
                BindingItem = new RepairDTO();
                BindingItem.Computer = new ComputerDTO();
            }

            OKButtonCommand = new RelayCommand<Window>(OK);
            CancelButtonCommand = new RelayCommand<Window>(Cancel);
            CompletedButtonCommand = new RelayCommand<Window>(Completed);
        }

        public void OK(Window window)
        {
            string errors = DoValidation();
            if (string.IsNullOrEmpty(errors))
            {
                BindingItem.Description = BindingItem.Description?.Trim();
                BindingItem.Computer.SerialNumber = BindingItem.Computer.SerialNumber.ToUpper();
                BindingItem.RepairCosts = decimal.Parse(RepairCosts?.Replace(".", ","));
                BindingItem.ReplacementsCosts = decimal.Parse(ReplacementsCosts?.Replace(".", ","));
                BindingItem.ServicemanNote = ServicemanNote?.Trim();
                BindingItem.Dealer = EmployeeDTO.ToDTO(Authorization.AcctualEmployee);
                BindingItem.DealerID = Authorization.AcctualEmployee.ID;

                if (BindingItem.ID > 0)
                {
                    Repositories.RepairsRepository.Edit(BindingItem);
                }
                else
                {
                    Repositories.RepairsRepository.Add(RepairDTO.FromDTO(BindingItem));
                }
            }
            else
            {
                Config.MsgBoxValidationMessage(errors);
                return;
            }

            window.DialogResult = true;
            window?.Close();
        }

        public void Cancel(Window window)
        {
            window.DialogResult = false;
            window?.Close();
        }

        //TODO: mailowe powiadomienie o zakończonej naprawie
        public void Completed(Window window)
        {
            if (MessageBox.Show($"Naprawa komputera: {BindingItem.Computer.Name} została zakończona, czy powiadomić klienta?",
                    "Naprawa zakończona", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                BindingItem.Status = DBO.RepairStatus.Completed;
                //BindingItem.RealizationDate = DateTime.Now;
                OK(window);
            }
            

        }

        private string DoValidation()
        {
            decimal repairCosts = 0;
            decimal replacementsCost = 0;
            string validationErrors3 = String.Empty;
            string validationErrors4 = String.Empty;

            if (!decimal.TryParse(RepairCosts?.Replace(".", ","), out repairCosts))
            {
                validationErrors3 = "Niepoprawny format kosztu naprawy.";
            }

            if (!decimal.TryParse(ReplacementsCosts?.Replace(".", ","), out replacementsCost))
            {
                validationErrors4 = "Niepoprawny format kosztu części.";
            }

          

            if ( string.IsNullOrEmpty(validationErrors3) && string.IsNullOrEmpty(validationErrors4))
            {
                return String.Empty;
            }
            else
            {
                string returnString = $"{validationErrors3}\r\n{validationErrors4}".Trim();
                return returnString;
            }
        }
    }
}