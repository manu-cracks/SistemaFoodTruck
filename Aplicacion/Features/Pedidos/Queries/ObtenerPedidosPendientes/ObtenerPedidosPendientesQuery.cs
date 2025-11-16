using Aplicacion.DTOs;
using MediatR;

namespace Aplicacion.Features.Pedidos.Queries.ObtenerPedidosPendientes
{
    /// <summary>
    /// Query para obtener todos los pedidos pendientes
    /// </summary>
    public class ObtenerPedidosPendientesQuery : IRequest<List<PedidoDto>>
    {
    }
}
