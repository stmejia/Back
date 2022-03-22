using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class activoOperacionesValidator : AbstractValidator<activoOperacionesDto>
    {
        public activoOperacionesValidator()
        {
            //RuleFor(e => e.codigo)
            //    .NotNull().WithMessage("Requerido")
            //    .NotEmpty().WithMessage("No debe estar vacío")
            //    .Length(1, 20).WithMessage("No se admiten más de 20 caracteres");

            RuleFor(e => e.descripcion)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar vacío")
                .Length(1, 50).WithMessage("No se admiten más de 50 caracteres");

            RuleFor(e => e.categoria)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar vacío")
                .Length(1, 1).WithMessage("No se admiten más de 1 caracter");

            RuleFor(e => e.color)                
                .Length(0, 25).WithMessage("No se admiten más de 25 caracteres");

            RuleFor(e => e.marca)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar vacío")
                .Length(1, 25).WithMessage("No se admiten más de 25 caracteres");

            RuleFor(e => e.vin)
                .Length(0, 25).WithMessage("No se admiten más de 25 caracteres");

            RuleFor(e => e.correlativo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar vacío")
                .InclusiveBetween(1, 9999).WithMessage("El correlativo esta fuera del tango permitido (1 a 9999)");

            RuleFor(e => e.serie)                
                .Length(0, 25).WithMessage("No se admiten más de 25 caracteres");

            RuleFor(e => e.modeloAnio)                
                 .InclusiveBetween(1940, 2100).WithMessage("El modelo esta fuera del rango permitido (1940 a 2100)");


            //RuleFor(e => e.idActivoGenerales)
            //    .NotNull().WithMessage("Requerido")
            //    .NotEmpty().WithMessage("No debe estar vacío");

            RuleFor(e => e.idTransporte)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar vacío");
        }
    }
}
