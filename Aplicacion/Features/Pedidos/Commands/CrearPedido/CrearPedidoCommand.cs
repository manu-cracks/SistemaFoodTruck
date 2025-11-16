using Aplicacion.DTOs;
using MediatR;

namespace Aplicacion.Features.Pedidos.Commands.CrearPedido
{
    /// <summary>
    /// Comando para crear un nuevo pedido con sus detalles
    /// </summary>
    public class CrearPedidoCommand : IRequest<PedidoDto>
    {
        public int ClienteId { get; set; }
        public List<CrearDetallePedidoDto> Detalles { get; set; } = new();
    }

    /// <summary>
    /// DTO para crear detalles de pedido
    /// </summary>
    public class CrearDetallePedidoDto
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
    }
}
