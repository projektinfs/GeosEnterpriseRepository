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
using System.Windows.Controls;
using System.ComponentModel.DataAnnotations;

namespace GeosEnterprise.ViewModel
{
    public class ConfirmationEmailViewModel : ViewModelBase
    {
        public ICommand OKButtonCommand { get; set; }
        public ICommand CancelButtonCommand { get; set; }
        public RepairDTO BindingItem { get; set; }

        public String From { get; set; }
        public String To { get; set; }
        public String Password { get; set; }
        public String Subject { get; set; }
        public String Message { get; set; }



        public object SelectedItem { get; set; }


        public ConfirmationEmailViewModel(int repairID)
        {

            BindingItem = RepairDTO.ToDTO(Repositories.RepairsRepository.GetById((int)repairID));
            To = BindingItem.Client.ClientContact.Email;
            From = BindingItem.Serviceman.Email;
            Subject = $"Zapraszamy po odbiór komputera: {BindingItem.Computer.Name} numer: {BindingItem.Computer.SerialNumber}";
            Message  = Properties.Resources.ConfirmationEmail
                     .Replace("{{numer_zlecenia}}", BindingItem.OrderNumber)
                    .Replace("{{nazwa}}", BindingItem.Computer.Name)
                    .Replace("{{nr_seryjny}}", BindingItem.Computer.SerialNumber)
                    .Replace("{{koszt}}", BindingItem.FinalCosts.ToString())
                   .Replace("{{data}}", DateTime.Now.ToShortDateString())
                   .Replace("{{pracownik}}", BindingItem.Serviceman.FullName);

            OKButtonCommand = new RelayCommand<Window>(OK);
            CancelButtonCommand = new RelayCommand<Window>(Cancel);
        }

        public void OK(Window window)
        {
            string errors = DoValidation();
            if (string.IsNullOrEmpty(errors))
            {
               
                    using (MailMessage email = new MailMessage())
                    {
                        email.From = new MailAddress(From);
                        email.To.Add(To);
                        email.Subject = Subject;
                        email.Body = Message;
                        //email.IsBodyHtml = true;

                        using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                        {
                            smtp.Credentials = new NetworkCredential(From, Password);
                            smtp.EnableSsl = true;
                            try
                            {
                                smtp.Send(email);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Błąd:\n\nWiadomość nie została wysłana!");
                                return;

                        }
                    }
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

     

        private string DoValidation()
        {
           
            string validationErrors = String.Empty;

            if (!(new EmailAddressAttribute().IsValid(From)))
            {
                validationErrors += "Niepoprawny adres nadawcy\n";
            }
            if (!(new EmailAddressAttribute().IsValid(To)))
            {
                validationErrors += "Niepoprawny adres odbiorcy\n";
            }
            if (string.IsNullOrEmpty(Password))
            {
                validationErrors += "Hasło nie może być puste\n";
            }
            if (string.IsNullOrEmpty(Subject))
            {
                validationErrors += "Temat nie może być pusty\n";
            }
            if (string.IsNullOrEmpty(Message))
            {
                validationErrors += "Treść nie może być pusta\n";
            }

            if (string.IsNullOrEmpty(validationErrors))
            {
                return String.Empty;
            }
            else
            {
               return validationErrors;
            }
        }
    }
}