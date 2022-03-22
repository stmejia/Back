using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class condicionGenSetValidator : AbstractValidator<condicionGenSetDto>
    {
        public condicionGenSetValidator()
        {
            RuleFor(e => e.idCondicionActivo)
                .NotNull().WithMessage("Requerido");
        }
    }
}
