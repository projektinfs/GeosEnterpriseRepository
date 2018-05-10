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
            RuleFor(employee => employee.Name).NotEmpty().WithMessage("Pole Imie nie może być puste");
            RuleFor(employee => employee.Surname).NotEmpty().WithMessage("Pole Nazwisko nie może być puste");
            RuleFor(employee => employee.Position).NotEmpty().WithMessage("Pole Stanowisko nie może być puste");
            RuleFor(employee => employee.Adress.City).NotEmpty().WithMessage("Pole Miasto nie może być puste");
            RuleFor(employee => employee.Email).NotEmpty().WithMessage("Pole Email nie może być puste").EmailAddress().WithMessage("Nieprawidłowy adres email!");
            RuleFor(employee => employee.EmployeeContact.Phone).Must(GoodPhone).WithMessage("Podany numer telefonu jest niepoprawny!");
            RuleFor(employee => employee.Adress.PostCode).Must(GoodPostCode).WithMessage("Nieprawidłowy kod pocztowy!");
            RuleFor(employee => employee.Password).NotEmpty().Must(GoodPassword).WithMessage("Hasło musi zawierć prznajmniej 6 znaków!");

        }


        public bool GoodPassword(string password)
        {
            return (password.Length >=6) ;
        }

        public bool GoodPostCode(string postcode)
        {
            if (string.IsNullOrEmpty(postcode)) return false;
            Regex regex = new Regex(@"^[0-9]{2}\-[0-9]{3}$");
            return regex.IsMatch(postcode);

        }

        public bool GoodPhone(string number)
        {
            if (string.IsNullOrEmpty(number)) return false;
            Regex regex = new Regex(@"^(?:(\+[0-9]{2})?[0-9]{9})$");
            return regex.IsMatch(number);

        }
    }
}
