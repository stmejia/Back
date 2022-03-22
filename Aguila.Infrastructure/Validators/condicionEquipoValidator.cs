using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class condicionEquipoValidator : AbstractValidator<condicionEquipoDto>
    {
        public condicionEquipoValidator()
        {
            RuleFor(e => e.idCondicionActivo)
                .NotNull().WithMessage("Requerido");

            RuleFor(e => e.guardaFangosG)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 15).WithMessage("No se admiten más de 15 caracteres");

            RuleFor(e => e.guardaFangosI)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 15).WithMessage("No se admiten más de 15 caracteres");

            RuleFor(e => e.cintaReflectivaLat)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 15).WithMessage("No se admiten más de 15 caracteres");

            RuleFor(e => e.cintaReflectivaFront)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 15).WithMessage("No se admiten más de 15 caracteres");

            RuleFor(e => e.cintaReflectivaTra)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 15).WithMessage("No se admiten más de 15 caracteres");

            RuleFor(e => e.manitas1)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 15).WithMessage("No se admiten más de 15 caracteres");

            RuleFor(e => e.manitas2)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 15).WithMessage("No se admiten más de 15 caracteres");

            RuleFor(e => e.bumper)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 15).WithMessage("No se admiten más de 15 caracteres");

            RuleFor(e => e.fricciones)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 15).WithMessage("No se admiten más de 15 caracteres");

            RuleFor(e => e.friccionesLlantas)
                .Length(1, 45).WithMessage("No se admiten más de 45 caracteres");

            RuleFor(e => e.patas)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 15).WithMessage("No se admiten más de 15 caracteres");

            RuleFor(e => e.ganchos)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 15).WithMessage("No se admiten más de 15 caracteres");

            RuleFor(e => e.balancines)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 15).WithMessage("No se admiten más de 15 caracteres");

            RuleFor(e => e.hojasResortes)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 15).WithMessage("No se admiten más de 15 caracteres");

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

            RuleFor(e => e.llanta12)
                .Length(1, 50).WithMessage("No se admiten más de 50 caracteres");

            RuleFor(e => e.llantaR)
                .Length(1, 50).WithMessage("No se admiten más de 50 caracteres");

            RuleFor(e => e.llantaR2)
                .Length(1, 50).WithMessage("No se admiten más de 50 caracteres");
        }
    }
}
