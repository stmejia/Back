using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class rutasValidator : AbstractValidator<rutasDto>
    {
        public rutasValidator()
        {
            RuleFor(e => e.idEmpresa)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estár vacío");

            RuleFor(e => e.idUbicacionOrigen)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.idUbicacionDestino)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.codigo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(6, 10).WithMessage("No se admiten más de 10 caracteres y menos de 6 en el campo");

            RuleFor(e => e.nombre)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 50).WithMessage("No se admiten más de 50 caracteres");

            RuleFor(e => e.distanciaKms)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.gradoPeligrosidad)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 25).WithMessage("No se admiten más de 25 caracteres");

            RuleFor(e => e.estadoCarretera)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 30).WithMessage("No se admiten más de 30 caracteres");
        }
    }
}
