using FluentValidation;

namespace Aplicacion.Features.Pedidos.Commands.ActualizarEstadoPedido
{
    /// <summary>
    /// Validador para el comando ActualizarEstadoPedido
    /// </summary>
    public class ActualizarEstadoPedidoCommandValidator : AbstractValidator<ActualizarEstadoPedidoCommand>
    {
        public ActualizarEstadoPedidoCommandValidator()
        {
            RuleFor(p => p.PedidoId)
                .GreaterThan(0).WithMessage("El ID del pedido es inválido");

            RuleFor(p => p.NuevoEstado)
                .IsInEnum().WithMessage("El estado del pedido no es válido");
        }
    }
}
