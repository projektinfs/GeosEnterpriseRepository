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
using System.Net.Mail;
using System.Net;
using GeosEnterprise.Views;

namespace GeosEnterprise.ViewModel
{
    public class ComputersServicemanViewModel : ViewModelBase
    {
        public ICommand OKButtonCommand { get; set; }
        public ICommand CancelButtonCommand { get; set; }
        public ICommand CompletedButtonCommand { get; set; }
        public ICommand CompletedNoMailButtonCommand { get; set; }
        public RepairDTO BindingItem { get; set; }
        public string RepairCosts { get; set; }
        public string ReplacementsCosts { get; set; }
        public string RepairDescription { get; set; }
        public string ServicemanNote { get; set; }
        private bool DataValidated;
        public DateTime? EstimatedTime { get; set; }


        public object SelectedItem { get; set; }

        public ComputersServicemanViewModel(int? repairID)
        {
            if (repairID != null)
            {
                BindingItem = RepairDTO.ToDTO(Repositories.RepairsRepository.GetById((int)repairID));
                RepairCosts = BindingItem.RepairCosts.ToString();
                ReplacementsCosts = BindingItem.ReplacementsCosts.ToString();
                ServicemanNote = BindingItem.ServicemanNote?.Trim();
                EstimatedTime = BindingItem.EstimatedDate;

            }
            else
            {
                BindingItem = new RepairDTO();
                BindingItem.Computer = new ComputerDTO();
            }

            OKButtonCommand = new RelayCommand<Window>(OK);
            CancelButtonCommand = new RelayCommand<Window>(Cancel);
            CompletedButtonCommand = new RelayCommand<Window>(Completed);
            CompletedNoMailButtonCommand = new RelayCommand<Window>(CompletedNoMail);

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
                    Repositories.RepairsRepository.Update(BindingItem);
                }
                else
                {
                    Repositories.RepairsRepository.Add(RepairDTO.FromDTO(BindingItem));
                }
            }
            else
            {
                DataValidated = false;
                Config.MsgBoxValidationMessage(errors);
                return;
            }
            DataValidated = true;
            window.DialogResult = true;
            window?.Close();
        }

        public void Cancel(Window window)
        {
            window.DialogResult = false;
            window?.Close();
        }

        public void Completed(Window window)
        {

            OK(window);
            if (DataValidated)
            {
                Window emailWindow = new ConfrmationEmail(BindingItem.ID);
                bool? emailWindowStatus = emailWindow.ShowDialog();
                if (emailWindowStatus == true)
                {
                    BindingItem.Status = DBO.RepairStatus.Completed;
                    Repositories.RepairsRepository.Update(BindingItem);
                }
            }
        }

        public void CompletedNoMail(Window window)
        {
            BindingItem.Status = DBO.RepairStatus.Completed;
            Repositories.RepairsRepository.Update(BindingItem);
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
            if (string.IsNullOrEmpty(ReplacementsCosts))
            {
                ReplacementsCosts = "0";
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