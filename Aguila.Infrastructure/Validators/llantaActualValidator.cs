using Aguila.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;


namespace Aguila.Infrastructure.Validators
{
    public class llantaActualValidator : AbstractValidator<llantaActualDto>
    {
        public llantaActualValidator()
        {
            RuleFor(e => e.idLlanta)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.idLlantaTipo)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.idActivoOperaciones)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.idEstado)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.ubicacionId)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.documentoEstado)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 20).WithMessage("No se admiten más de 20 caracteres");

            RuleFor(e => e.documentoUbicacion)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 20).WithMessage("No se admiten más de 20 caracteres");

            RuleFor(e => e.observacion)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 350).WithMessage("No se admiten más de 350 caracteres");

            RuleFor(e => e.posicion)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.profundidadIzquierda)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 10).WithMessage("No se admiten más de 10 caracteres");

            RuleFor(e => e.profundidadCentro)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 10).WithMessage("No se admiten más de 10 caracteres");

            RuleFor(e => e.profundidadDerecho)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 10).WithMessage("No se admiten más de 10 caracteres");

            RuleFor(e => e.reencauche)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 10).WithMessage("No se admiten más de 10 caracteres");

            RuleFor(e => e.precio)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.proposito)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío")
                .Length(1, 20).WithMessage("No se admiten más de 20 caracteres");

            RuleFor(e => e.fechaEstado)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");

            RuleFor(e => e.fechaUbicacion)
                .NotNull().WithMessage("Requerido")
                .NotEmpty().WithMessage("No debe de estar vacío");
        }
    }
}
