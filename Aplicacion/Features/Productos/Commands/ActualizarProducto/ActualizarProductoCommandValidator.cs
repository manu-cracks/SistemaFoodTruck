using FluentValidation;

namespace Aplicacion.Features.Productos.Commands.ActualizarProducto
{
    /// <summary>
    /// Validador para el comando ActualizarProducto
    /// </summary>
    public class ActualizarProductoCommandValidator : AbstractValidator<ActualizarProductoCommand>
    {
        public ActualizarProductoCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0).WithMessage("El ID del producto es inválido");

            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("El nombre del producto es obligatorio")
                .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres");

            RuleFor(p => p.Precio)
                .GreaterThan(0).WithMessage("El precio debe ser mayor a 0");

            RuleFor(p => p.Descripcion)
                .MaximumLength(500).WithMessage("La descripción no puede exceder 500 caracteres")
                .When(p => !string.IsNullOrEmpty(p.Descripcion));

            RuleFor(p => p.Categoria)
                .MaximumLength(50).WithMessage("La categoría no puede exceder 50 caracteres")
                .When(p => !string.IsNullOrEmpty(p.Categoria));
        }
    }
}
