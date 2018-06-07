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
    class EmployeeValidator : AbstractValidator<EmployeeDTO>
    {
        public EmployeeValidator()
        {
            RuleFor(employee => employee.Name).NotEmpty().WithMessage("Pole Imie nie może być puste").Must(OnlyLetters).WithMessage("Nieprawidłowe imię");
            RuleFor(employee => employee.Surname).NotEmpty().WithMessage("Pole Nazwisko nie może być puste").Must(OnlyLetters).WithMessage("Nieprawidłowe nazwisko");
            RuleFor(employee => employee.Position).NotEmpty().WithMessage("Pole Stanowisko nie może być puste");
            RuleFor(employee => employee.Email).NotEmpty().WithMessage("Pole Email nie może być puste").EmailAddress().WithMessage("Nieprawidłowy adres email!");
            RuleFor(employee => employee.Adress.City).NotEmpty().WithMessage("Pole Miasto nie może być puste").Must(OnlyLetters).WithMessage("Nieprawidłowe miasto");
            RuleFor(employee => employee.Adress.District).Must(OnlyLetters).WithMessage("Nieprawidłowy powiat");
            RuleFor(employee => employee.Adress.Voivodeship).Must(OnlyLetters).WithMessage("Nieprawidłowe województwo");
            RuleFor(employee => employee.Adress.PostCode).NotEmpty().WithMessage("Pole Kod pocztowy nie może być puste").Must(GoodPostCode).WithMessage("Nieprawidłowy kod pocztowy!");
            RuleFor(employee => employee.Adress.Street).NotEmpty().WithMessage("Pole Ulica nie może być puste").Must(GoodStreet).WithMessage("Nieprawidłowa ulica");
            RuleFor(employee => employee.Adress.BuildingNumber).NotEmpty().WithMessage("Pole Budynek nie może być puste").Must(GoodNumber).WithMessage("Nieprawidłowa numer budynku");
            RuleFor(employee => employee.Adress.AppartamentNumber).Must(GoodNumber).WithMessage("Nieprawidłowa numer mieszkania");
            RuleFor(employee => employee.EmployeeContact.Phone).NotEmpty().WithMessage("Pole Telefon nie może być puste").Must(GoodPhone).WithMessage("Podany numer telefonu jest niepoprawny!");
            RuleFor(employee => employee.EmployeeContact.Fax).Must(GoodPhone).WithMessage("Podany fax jest niepoprawny!");
            RuleFor(employee => employee.EmployeeContact.Www).Must(GoodWWW).WithMessage("Podany www jest niepoprawny!");


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
            Regex regex = new Regex(@"^([a-zA-ZąęóćłńśżźĄĘÓĆŁŃŚŻŹ\-\s\']+)$");
            return regex.IsMatch(text);
        }

        public bool GoodStreet(string text)
        {
            if (string.IsNullOrEmpty(text)) return true;
            Regex regex = new Regex(@"^([a-zA-Z0-9ąęóćłńśżźĄĘÓĆŁŃŚŻŹ\-\s]+)$");
            return regex.IsMatch(text);
        }




    }
}
