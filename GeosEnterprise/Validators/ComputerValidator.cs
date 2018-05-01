using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using GeosEnterprise.DTO;

namespace GeosEnterprise.Validators
{
    public class ComputerValidator : AbstractValidator<ComputerDTO>
    {
        public ComputerValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Proszę podać nazwę komputera.");
            RuleFor(p => p.SerialNumber).NotEmpty().WithMessage("Proszę podać numer seryjny komputera");
        }
    }
}
