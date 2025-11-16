using Aplicacion.Contratos.Persistencia;
using Aplicacion.DTOs;
using MediatR;

namespace Aplicacion.Features.Pedidos.Commands.ActualizarEstadoPedido
{
    /// <summary>
    /// Handler para actualizar el estado de un pedido
    /// </summary>
    public class ActualizarEstadoPedidoCommandHandler : IRequestHandler<ActualizarEstadoPedidoCommand, PedidoDto>
    {
        private readonly IPedidoRepository _pedidoRepository;

        public ActualizarEstadoPedidoCommandHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<PedidoDto> Handle(ActualizarEstadoPedidoCommand request, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepository.ObtenerPorIdConDetallesAsync(request.PedidoId);

            if (pedido == null)
            {
                throw new KeyNotFoundException($"Pedido con ID {request.PedidoId} no encontrado");
            }

            pedido.Estado = request.NuevoEstado;
            await _pedidoRepository.ActualizarAsync(pedido);

            return new PedidoDto
            {
                Id = pedido.Id,
                ClienteId = pedido.ClienteId,
                NombreCliente = pedido.Cliente?.Nombre,
                FechaHoraPedido = pedido.FechaHoraPedido,
                Estado = pedido.Estado,
                Total = pedido.Total,
                Detalles = pedido.Detalles.Select(d => new DetallePedidoDto
                {
                    Id = d.Id,
                    PedidoId = d.PedidoId,
                    ProductoId = d.ProductoId,
                    NombreProducto = d.Producto?.Nombre,
                    PrecioUnitario = d.Producto?.Precio ?? 0,
                    Cantidad = d.Cantidad,
                    Subtotal = d.Subtotal
                }).ToList()
            };
        }
    }
}
