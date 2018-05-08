using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using GeosEnterprise.DTO;

namespace GeosEnterprise.Validators
{
    public class RepairValidator : AbstractValidator<RepairDTO>
    {
        public RepairValidator()
        {
            RuleFor(p => p.Computer.Name).NotNull().WithMessage("Brak przypisanego komputera.");
            RuleFor(p => p.Client.FullName).NotEmpty().WithMessage("Proszę wybrać klienta.");
        }
    }
}
