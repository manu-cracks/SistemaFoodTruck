using Aplicacion.DTOs;
using Dominio.Entidades;
using MediatR;

namespace Aplicacion.Features.Pedidos.Commands.ActualizarEstadoPedido
{
    /// <summary>
    /// Comando para actualizar el estado de un pedido
    /// </summary>
    public class ActualizarEstadoPedidoCommand : IRequest<PedidoDto>
    {
        public int PedidoId { get; set; }
        public PedidoEstado NuevoEstado { get; set; }
    }
}
