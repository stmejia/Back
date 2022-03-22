using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class condicionCisternaValidator : AbstractValidator<condicionCisternaDto>
    {
        public condicionCisternaValidator()
        {
            RuleFor(e => e.idCondicionActivo)
                .NotNull().WithMessage("Requerido");

            RuleFor(e => e.luzLateral)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 15).WithMessage("No se admiten más de 15 caracteres");

            RuleFor(e => e.luzTrasera)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 15).WithMessage("No se admiten más de 15 caracteres");

            RuleFor(e => e.guardaFango)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 15).WithMessage("No se admiten más de 15 caracteres");

            RuleFor(e => e.cintaReflectiva)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 15).WithMessage("No se admiten más de 15 caracteres");

            RuleFor(e => e.manitas)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 15).WithMessage("No se admiten más de 15 caracteres");

            RuleFor(e => e.fricciones)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 15).WithMessage("No se admiten más de 15 caracteres");

            RuleFor(e => e.friccionesLlantas)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 15).WithMessage("No se admiten más de 15 caracteres");

            RuleFor(e => e.escalera)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 15).WithMessage("No se admiten más de 15 caracteres");

            RuleFor(e => e.señalizacion)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 15).WithMessage("No se admiten más de 15 caracteres");

            RuleFor(e => e.taponValvulas)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 15).WithMessage("No se admiten más de 15 caracteres");

            RuleFor(e => e.manHole)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 15).WithMessage("No se admiten más de 15 caracteres");

            RuleFor(e => e.platinas)
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
