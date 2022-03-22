using Aguila.Core.DTOs;
using FluentValidation;

namespace Aguila.Infrastructure.Validators
{
    public class paisesValidator : AbstractValidator<paisesDto>
    {
        public paisesValidator()
        {
            RuleFor(pais => pais.Nombre)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar vacío")
                .Length(1, 25).WithMessage("tamaño de 1 a 25 caracteres");

            RuleFor(pais => pais.CodMoneda)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar vacío")
                .Length(1, 3).WithMessage("No se admiten más de 3 caracteres");

            RuleFor(pais => pais.CodAlfa2)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar vacío")
                .Length(1, 2).WithMessage("No se admiten más de 2 caracteres");

            RuleFor(pais => pais.CodAlfa3)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar vacío")
                .Length(1, 3).WithMessage("No se admiten más de 3 caracteres");

            RuleFor(pais => pais.CodNumerico)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(pais => pais.Idioma)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe estar vacío")
                .Length(1, 4).WithMessage("No se admiten más de 4 caracteres");
        }
    }
}
