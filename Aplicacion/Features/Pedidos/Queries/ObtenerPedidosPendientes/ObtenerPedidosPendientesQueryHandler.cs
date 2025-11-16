using Aplicacion.Contratos.Persistencia;
using Aplicacion.DTOs;
using MediatR;

namespace Aplicacion.Features.Pedidos.Queries.ObtenerPedidosPendientes
{
    /// <summary>
    /// Handler para obtener pedidos pendientes
    /// </summary>
    public class ObtenerPedidosPendientesQueryHandler : IRequestHandler<ObtenerPedidosPendientesQuery, List<PedidoDto>>
    {
        private readonly IPedidoRepository _pedidoRepository;

        public ObtenerPedidosPendientesQueryHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<List<PedidoDto>> Handle(ObtenerPedidosPendientesQuery request, CancellationToken cancellationToken)
        {
            var pedidos = await _pedidoRepository.ObtenerPendientesAsync();

            return pedidos.Select(p => new PedidoDto
            {
                Id = p.Id,
                ClienteId = p.ClienteId,
                NombreCliente = p.Cliente?.Nombre,
                FechaHoraPedido = p.FechaHoraPedido,
                Estado = p.Estado,
                Total = p.Total,
                Detalles = p.Detalles.Select(d => new DetallePedidoDto
                {
                    Id = d.Id,
                    PedidoId = d.PedidoId,
                    ProductoId = d.ProductoId,
                    NombreProducto = d.Producto?.Nombre,
                    PrecioUnitario = d.Producto?.Precio ?? 0,
                    Cantidad = d.Cantidad,
                    Subtotal = d.Subtotal
                }).ToList()
            }).ToList();
        }
    }
}
