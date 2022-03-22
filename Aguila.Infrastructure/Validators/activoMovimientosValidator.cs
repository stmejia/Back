using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;


namespace Aguila.Infrastructure.Validators
{
    public class activoMovimientosValidator : AbstractValidator<activoMovimientosDto>
    {
        public activoMovimientosValidator()
        {
            RuleFor(e => e.idActivo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            //RuleFor(e => e.idServicio)
            //    .NotNull().WithMessage("Requerido")
            //    .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.idEstacionTrabajo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            //RuleFor(e => e.idPiloto)
            //    .NotNull().WithMessage("Requerido")
            //    .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.idUsuario)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.lugar)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 150).WithMessage("No se admiten más de 150 caracteres");
        }
    }
}
