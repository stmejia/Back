using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class ModulosMnuValidator : AbstractValidator<ModulosMnuDto>
    {
        public ModulosMnuValidator()
        {
            RuleFor(moduloMnu => moduloMnu.ModuloId)
               .NotNull().WithMessage("Requerido")
               .NotEmpty().WithMessage("No debe estar Vacio");

            RuleFor(moduloMnu => moduloMnu.Codigo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar Vacio");

            RuleFor(moduloMnu => moduloMnu.Descrip)
               .NotNull().WithMessage("Requerido")
               .NotEmpty().WithMessage("No debe estar Vacio");

            RuleFor(moduloMnu => moduloMnu.Activo)
               .NotNull().WithMessage("Requerido")
               .NotEmpty().WithMessage("No debe estar Vacio");
        }
    }
}
