using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class condicionFurgonValidator : AbstractValidator<condicionFurgonDto>
    {
        public condicionFurgonValidator()
        {
            RuleFor(e => e.idCondicionActivo)
                .NotNull().WithMessage("Requerido");

            RuleFor(e => e.revPuertaCerrado)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 1).WithMessage("No se admite más de 1 caracter");

            RuleFor(e => e.revPuertaEmpaque)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 1).WithMessage("No se admite más de 1 caracter");

            RuleFor(e => e.revPuertaCinta)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 1).WithMessage("No se admite más de 1 caracter");

            RuleFor(e => e.fricciones)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 1).WithMessage("No se admite más de 1 caracter");

            RuleFor(e => e.senalizacion)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 1).WithMessage("No se admite más de 1 caracter");

            RuleFor(e => e.llanta1)
                .Length(1, 50).WithMessage("No se admiten más de 50 caracteres");

            RuleFor(e => e.llanta2)
                .Length(1, 50).WithMessage("No se admiten más de 50 caracteres");

            RuleFor(e => e.llanta3)
                .Length(1, 50).WithMessage("No se admiten más de 50 caracteres");

            RuleFor(e => e.llanta4)
                .Length(1, 50).WithMessage("No se admiten más de 50 caracteres");

            RuleFor(e => e.llanta5)
                .Length(1, 50).WithMessage("No se admiten más de 50 caracteres");

            RuleFor(e => e.llanta6)
                .Length(1, 50).WithMessage("No se admiten más de 50 caracteres");

            RuleFor(e => e.llanta7)
                .Length(1, 50).WithMessage("No se admiten más de 50 caracteres");

            RuleFor(e => e.llanta8)
                .Length(1, 50).WithMessage("No se admiten más de 50 caracteres");

            RuleFor(e => e.llanta9)
                .Length(1, 50).WithMessage("No se admiten más de 50 caracteres");

            RuleFor(e => e.llanta10)
                .Length(1, 50).WithMessage("No se admiten más de 50 caracteres");

            RuleFor(e => e.llanta11)
                .Length(1, 50).WithMessage("No se admiten más de 50 caracteres");

            RuleFor(e => e.llantaR)
                .Length(1, 50).WithMessage("No se admiten más de 50 caracteres");

            RuleFor(e => e.llantaR2)
                .Length(1, 50).WithMessage("No se admiten más de 50 caracteres");
        }
    }
}
