using Aplicacion.DTOs;
using MediatR;

namespace Aplicacion.Features.Dashboard.Queries.ObtenerEstadisticas
{
    /// <summary>
    /// Query para obtener estadísticas del dashboard
    /// </summary>
    public class ObtenerEstadisticasQuery : IRequest<DashboardDto>
    {
        public int? TopProductos { get; set; } = 10; // Top 10 por defecto
    }
}
