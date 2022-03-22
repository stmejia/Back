using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class municipiosValidator : AbstractValidator<municipiosDto>
    {
        public municipiosValidator()
        {
            RuleFor(e => e.idDepartamento)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.codMunicipio)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar vacío")
                .Length(1, 5).WithMessage("No se adminten más de 5 caracteres");

            RuleFor(e => e.nombreMunicipio)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar vacío")
                .Length(1, 25).WithMessage("No se adminten más de 25 caracteres");

            //RuleFor(e => e.fechaCreacion)
            //    .NotNull().WithMessage("Requerido")
            //    .NotEmpty().WithMessage("El campo fecha no debe de estar vacío");
        }
    }
}
