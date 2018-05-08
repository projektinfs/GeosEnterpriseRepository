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
            RuleFor(p => p.Computer).NotNull().WithMessage("Brak przypisanego komputera.");
            RuleFor(p => p.OrderNumber).NotEmpty().WithMessage("Brak numeru zamówienia.");
            RuleFor(p => p.Client.FullName).NotEmpty().WithMessage("Proszę wybrać klienta.");
        }
    }
}
