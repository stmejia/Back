using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class ModulosValidator : AbstractValidator<ModulosDto>
    {
        public ModulosValidator()
        {
            RuleFor(modulo => modulo.Nombre)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar Vacio");

            RuleFor(modulo => modulo.path)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar Vacio")
                .Length(1, 200).WithMessage("No se admiten más de 200 caracteres");

            RuleFor(modulo => modulo.ModuMinVersion)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar Vacio");
        }
    }
}
