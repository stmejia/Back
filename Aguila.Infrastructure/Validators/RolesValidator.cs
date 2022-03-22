using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class RolesValidator : AbstractValidator<RolesDto>
    {
        public RolesValidator()
        {
            RuleFor(rol => rol.nombre)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar Vacio")
                .Length(2,50).WithMessage("tamaño de 2 a 50 caracteres");
        }
    }
}
