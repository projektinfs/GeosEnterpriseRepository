using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeosEnterprise.DTO;
using GeosEnterprise.Validators;
using FluentValidation;
using FluentValidation.Results;

namespace GeosEnterprise.Validators
{
    public class ValidatorTools
    {
        public static string DoValidation<TEntity, TValidator>(TEntity entity, TValidator validator) where TEntity : DTOObject<int> where TValidator : AbstractValidator<TEntity>
        {
            ValidationResult validationResult = validator.Validate(entity);

            if (!validationResult.IsValid)
            {
                return string.Join("\r\n", validationResult.Errors.Select(p => p.ErrorMessage));
            }
            else
            {
                return null;
            }
        }
    }
}
