using Aguila.Core.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class condicionCabezalValidator : AbstractValidator<condicionCabezal>
    {
        public condicionCabezalValidator()
        {
            RuleFor(e => e.idCondicionActivo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");
            

            //RuleFor(e => e.idReparacion)
            //    .NotNull().WithMessage("Requerido")
            //    .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.windShield)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 1).WithMessage("No se admite más de 1 caracter");

            RuleFor(e => e.plumillas)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 5).WithMessage("No se admiten más de 5 caracteres");

            RuleFor(e => e.viscera)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 1).WithMessage("No se admite más de 1 caracter");

            RuleFor(e => e.rompeVientos)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 1).WithMessage("No se admite más de 1 caracter");

            RuleFor(e => e.persiana)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 1).WithMessage("No se admite más de 1 caracter");

            RuleFor(e => e.bumper)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 1).WithMessage("No se admite más de 1 caracter");

            RuleFor(e => e.capo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 1).WithMessage("No se admite más de 1 caracter");

            RuleFor(e => e.retrovisor)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 5).WithMessage("No se admiten más de 5 caracteres");

            RuleFor(e => e.ojoBuey)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 5).WithMessage("No se admiten más de 5 caracteres");

            RuleFor(e => e.pataGallo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 5).WithMessage("No se admiten más de 5 caracteres");

            RuleFor(e => e.portaLlanta)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 1).WithMessage("No se admite más de 1 caracter");

            RuleFor(e => e.spoilers)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 5).WithMessage("No se admiten más de 5 caracteres");

            RuleFor(e => e.salpicadera)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 5).WithMessage("No se admiten más de 5 caracteres");

            RuleFor(e => e.guardaFango)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 5).WithMessage("No se admiten más de 5 caracteres");

            RuleFor(e => e.taponCombustible)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 5).WithMessage("No se admiten más de 5 caracteres");

            RuleFor(e => e.baterias)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 5).WithMessage("No se admiten más de 5 caracteres");

            RuleFor(e => e.lucesDelanteras)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 1).WithMessage("No se admite más de 1 caracter");

            RuleFor(e => e.lucesTraseras)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 1).WithMessage("No se admite más de 1 caracter");

            RuleFor(e => e.pintura)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 1).WithMessage("No se admite más de 1 caracter");

            RuleFor(e => e.llanta1)         
                .Length(0, 100).WithMessage("No se admiten más de 100 caracteres");

            RuleFor(e => e.llanta2)
                .Length(0, 100).WithMessage("No se admiten más de 100 caracteres");

            RuleFor(e => e.llanta3)
                .Length(0, 100).WithMessage("No se admiten más de 100 caracteres");

            RuleFor(e => e.llanta4)
                .Length(0, 100).WithMessage("No se admiten más de 100 caracteres");

            RuleFor(e => e.llanta5)
                .Length(0, 100).WithMessage("No se admiten más de 100 caracteres");

            RuleFor(e => e.llanta6)
                .Length(0, 100).WithMessage("No se admiten más de 100 caracteres");

            RuleFor(e => e.llanta7)
                .Length(0, 100).WithMessage("No se admiten más de 100 caracteres");

            RuleFor(e => e.llanta8)
                .Length(0, 100).WithMessage("No se admiten más de 100 caracteres");

            RuleFor(e => e.llanta9)
                .Length(0, 100).WithMessage("No se admiten más de 100 caracteres");

            RuleFor(e => e.llanta10)
                .Length(0, 100).WithMessage("No se admiten más de 100 caracteres");

            RuleFor(e => e.llantaR)
                .Length(0, 100).WithMessage("No se admiten más de 100 caracteres");

            RuleFor(e => e.llantaR2)
                .Length(0, 100).WithMessage("No se admiten más de 100 caracteres");
        }
    }
}
