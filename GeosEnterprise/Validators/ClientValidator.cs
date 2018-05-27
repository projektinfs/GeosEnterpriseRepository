using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using FluentValidation;
using GeosEnterprise.DTO;

namespace GeosEnterprise.Validators
{
    class ClientValidator : AbstractValidator<ClientDTO>
    {
        public ClientValidator()
        {
            RuleFor(client => client.Name).NotEmpty().WithMessage("Pole Imie nie może być puste").Must(OnlyLetters).WithMessage("Nieprawidłowe imię");
            RuleFor(client => client.Surname).NotEmpty().WithMessage("Pole Nazwisko nie może być puste").Must(OnlyLetters).WithMessage("Nieprawidłowe nazwisko");
            RuleFor(client => client.ClientContact.Email).NotEmpty().WithMessage("Pole Email nie może być puste").EmailAddress().WithMessage("Nieprawidłowy adres email!");
            RuleFor(client => client.ClientAdress.City).NotEmpty().WithMessage("Pole Miasto nie może być puste").Must(OnlyLetters).WithMessage("Nieprawidłowe miasto");
            RuleFor(client => client.ClientAdress.Voivodeship).NotEmpty().WithMessage("Pole Województwo nie może być puste").Must(OnlyLetters).WithMessage("Nieprawidłowe wojewodztwo");
            RuleFor(client => client.ClientAdress.District).NotEmpty().WithMessage("Pole Powiat nie może być puste").Must(OnlyLetters).WithMessage("Nieprawidłowy powiat");
            RuleFor(client => client.ClientAdress.PostCode).NotEmpty().WithMessage("Pole Kod pocztowy nie może być puste").Must(GoodPostCode).WithMessage("Nieprawidłowy kod pocztowy!");
            RuleFor(client => client.ClientAdress.Street).NotEmpty().WithMessage("Pole Ulica nie może być puste").Must(OnlyLetters).WithMessage("Nieprawidłowa ulica");
            RuleFor(client => client.ClientAdress.BuildingNumber).NotEmpty().WithMessage("Pole Budynek nie może być puste").Must(GoodNumber).WithMessage("Nieprawidłowa numer budynku");
            RuleFor(client => client.ClientAdress.AppartamentNumber).Must(GoodNumber).WithMessage("Nieprawidłowa numer mieszkania");
            RuleFor(client => client.ClientContact.Phone).NotEmpty().WithMessage("Pole Telefon nie może być puste").Must(GoodPhone).WithMessage("Podany numer telefonu jest niepoprawny!");
            RuleFor(client => client.ClientContact.Fax).Must(GoodPhone).WithMessage("Podany fax jest niepoprawny!");
            RuleFor(client => client.ClientContact.Www).Must(GoodWWW).WithMessage("Podany www jest niepoprawny!");
        }
  
        public bool GoodPostCode(string postcode)
        {
            if (string.IsNullOrEmpty(postcode)) return true;
            Regex regex = new Regex(@"^[0-9]{2}\-[0-9]{3}$");
            return regex.IsMatch(postcode);
        }

        public bool GoodPhone(string number)
        {
            if (string.IsNullOrEmpty(number)) return true;
            Regex regex = new Regex(@"^(?:(\+[0-9]{2})?[0-9]{9})$");
            return regex.IsMatch(number);
        }

        public bool GoodNumber(string number)
        {
            if (string.IsNullOrEmpty(number)) return true;
            Regex regex = new Regex(@"^([0-9]+([A-Za-z]{1})?)$");
            return regex.IsMatch(number);
        }

        public bool GoodWWW(string text)
        {
            if (string.IsNullOrEmpty(text)) return true;
            Regex regex = new Regex(@"^([-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*))$");
            return regex.IsMatch(text);
        }

        public bool OnlyLetters(string text)
        {
            if (string.IsNullOrEmpty(text)) return true;
            Regex regex = new Regex(@"^([a-zA-ZąęóćłńśżźĄĘÓĆŁŃŚŻŹ\-\s]+)$");
            return regex.IsMatch(text);
        }
    }
}
