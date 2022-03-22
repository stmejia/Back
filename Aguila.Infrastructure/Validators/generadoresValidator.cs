using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class generadoresValidator : AbstractValidator<generadoresDto>
    {
        public generadoresValidator()
        {
            
            RuleFor(e => e.idTipoGenerador)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.capacidadGalones)
                .NotNull().WithMessage("Requerido");

            RuleFor(e => e.numeroCilindros)
                .NotNull().WithMessage("Requerido");

            RuleFor(e => e.marcaGenerador)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 1).WithMessage("No se admite más de 1 caracter");

            RuleFor(e => e.tipoInstalacion)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 1).WithMessage("No se admite más de 1 caracter");

            RuleFor(e => e.tipoEnfriamiento)
                .NotNull().WithMessage("Requerido")
                .Length(0, 10).WithMessage("No se admiten más de 10 caracteres");
        }
    }
}
