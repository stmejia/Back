using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class tarifarioValidator : AbstractValidator<tarifarioDto>
    {
        public tarifarioValidator()
        {
            RuleFor(e => e.codigo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 20).WithMessage("No se admiten más de 20 caracteres");

            RuleFor(e => e.tipoCarga)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 20).WithMessage("No se admiten más de 20 caracteres");

            RuleFor(e => e.tipoMovimiento)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 15).WithMessage("No se admiten más de 15 caracteres");

            RuleFor(e => e.segmento)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 30).WithMessage("No se admiten más de 30 caracteres");

            //RuleFor(e => e.idUbicacionOrigen)
            //    .NotNull().WithMessage("Requerido")
            //    .NotEmpty().WithMessage("No debe de estar vacío");

            //RuleFor(e => e.idUbicacionDestino)
            //    .NotNull().WithMessage("Requerido")
            //    .NotEmpty().WithMessage("No debe de estar vacío");

            //RuleFor(e => e.idRuta)
            //    .NotNull().WithMessage("Requerido")
            //    .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.idServicio)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.idEmpresa)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.combustibleGls)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.precio)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.kmRecorridosCargado)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.kmRecorridosVacio)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.tipoViaje)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.fechaVigencia)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");
        }
    }
}
