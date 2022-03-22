using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;


namespace Aguila.Infrastructure.Validators
{
    public class condicionTecnicaGenSetValidator : AbstractValidator<condicionTecnicaGenSetDto>
    {
        public condicionTecnicaGenSetValidator()
        {
            RuleFor(e => e.idCondicionActivo)
                .NotNull().WithMessage("Requerido");

            RuleFor(e => e.bateriaCodigo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 50).WithMessage("No se admiten más de 50 caracteres");
        }
    }
}
