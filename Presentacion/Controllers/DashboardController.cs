using Aplicacion.Features.Dashboard.Queries.ObtenerEstadisticas;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    /// <summary>
    /// Controlador para el Dashboard de estadísticas de ventas
    /// </summary>
    public class DashboardController : Controller
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Muestra el dashboard con estadísticas de ventas
        /// Basado únicamente en pedidos con estado "Entregado"
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var query = new ObtenerEstadisticasQuery
            {
                TopProductos = 10 // Mostrar top 10 productos
            };

            var estadisticas = await _mediator.Send(query);
            return View(estadisticas);
        }
    }
}
