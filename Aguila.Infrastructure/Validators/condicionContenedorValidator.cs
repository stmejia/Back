using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Infrastructure.Validators
{
    public class condicionContenedorValidator : AbstractValidator<condicionContenedorDto>
    {
        public condicionContenedorValidator()
        {
            RuleFor(e => e.idCondicionActivo)
                .NotNull().WithMessage("Requerido");

            RuleFor(e => e.tipoContenedor)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estár vacío")
                .Length(1, 1).WithMessage("No se admite más de 1 caracter");
        }
    }
}
