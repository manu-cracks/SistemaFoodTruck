using FluentValidation;

namespace Aplicacion.Features.Pedidos.Commands.CrearPedido
{
    /// <summary>
    /// Validador para el comando CrearPedido
    /// </summary>
    public class CrearPedidoCommandValidator : AbstractValidator<CrearPedidoCommand>
    {
        public CrearPedidoCommandValidator()
        {
            RuleFor(p => p.ClienteId)
                .GreaterThan(0).WithMessage("El ID del cliente es inválido");

            RuleFor(p => p.Detalles)
                .NotEmpty().WithMessage("El pedido debe tener al menos un producto")
                .Must(detalles => detalles.Count > 0).WithMessage("El pedido debe tener al menos un producto");

            RuleForEach(p => p.Detalles).ChildRules(detalle =>
            {
                detalle.RuleFor(d => d.ProductoId)
                    .GreaterThan(0).WithMessage("El ID del producto es inválido");

                detalle.RuleFor(d => d.Cantidad)
                    .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0");
            });
        }
    }
}
