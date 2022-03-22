using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
namespace Aguila.Infrastructure.Validators
{
    public class invUbicacionBodegaValidator : AbstractValidator<invUbicacionBodegaDto>
    {
        public invUbicacionBodegaValidator()
        {
            RuleFor(e => e.idBodega)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.estante)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.pasillo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.nivel)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.lugar)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.idProducto)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");
        }
    }
}
