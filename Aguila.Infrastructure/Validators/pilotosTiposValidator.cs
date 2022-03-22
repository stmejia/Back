using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    public class pilotosTiposValidator : AbstractValidator<pilotosTiposDto>
    {
        public pilotosTiposValidator()
        {
            RuleFor(e => e.codigo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");
                //.Must(e => int.TryParse(e.ToString(), out int codigo))
                //.WithMessage("No se permiten letras");

            When(e => e.codigo.ToString().Equals(""), () =>
            {
                RuleFor(e => e.codigo)
                .NotEmpty()
                .WithMessage("Debe ser entero");
            });

            //.Must(e => int.TryParse(e, out _))
            //.WithMessage("'{e}' is not a Long Data Type");

            RuleFor(e => e.descripcion)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");
        }
    }
}
