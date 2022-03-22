using Aguila.Core.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Infrastructure.Validators
{
    class ImagenValidator : AbstractValidator<Imagen>

    {
        public ImagenValidator()
        {
            RuleFor(imagen => imagen.FileName)
               .NotNull().WithMessage("Requerido")
               .NotEmpty().WithMessage("No debe estar vacio")
               .Length(5, 200).WithMessage("tamaño 5 a 200 caracteres");

            RuleFor(imagen => imagen.Nombre)
               .NotNull().WithMessage("Requerido")
               .NotEmpty().WithMessage("No debe estar vacio")
               .Length(5, 50).WithMessage("tamaño 5 a 50 caracteres");

            RuleFor(imagen => imagen.Descripcion)
               .NotNull().WithMessage("Requerido")
               .NotEmpty().WithMessage("No debe estar vacio")
               .Length(5, 300).WithMessage("tamaño 5 a 300 caracteres");
        }
    }
}
