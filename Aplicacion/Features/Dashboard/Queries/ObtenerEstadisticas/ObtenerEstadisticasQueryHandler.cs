using Aplicacion.Contratos.Persistencia;
using Aplicacion.DTOs;
using Dominio.Entidades;
using MediatR;

namespace Aplicacion.Features.Dashboard.Queries.ObtenerEstadisticas
{
    /// <summary>
    /// Handler para obtener estadísticas del dashboard basadas en pedidos entregados
    /// </summary>
    public class ObtenerEstadisticasQueryHandler : IRequestHandler<ObtenerEstadisticasQuery, DashboardDto>
    {
        private readonly IPedidoRepository _pedidoRepository;

        public ObtenerEstadisticasQueryHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<DashboardDto> Handle(ObtenerEstadisticasQuery request, CancellationToken cancellationToken)
        {
            // Obtener todos los pedidos entregados
            var pedidosEntregados = await _pedidoRepository.ObtenerPorEstadoAsync(PedidoEstado.Entregado);
            var listaPedidos = pedidosEntregados.ToList();

            var dashboard = new DashboardDto();

            if (!listaPedidos.Any())
            {
                return dashboard; // Retornar dashboard vacío si no hay pedidos entregados
            }

            // Estadísticas Generales
            dashboard.TotalPedidosEntregados = listaPedidos.Count;
            dashboard.TotalVentas = listaPedidos.Sum(p => p.Total);
            dashboard.PromedioVentaPorPedido = listaPedidos.Average(p => p.Total);
            dashboard.TotalProductosVendidos = listaPedidos
                .SelectMany(p => p.Detalles)
                .Sum(d => d.Cantidad);

            // Productos más vendidos
            var productosVendidos = listaPedidos
                .SelectMany(p => p.Detalles)
                .GroupBy(d => new
                {
                    d.ProductoId,
                    d.Producto!.Nombre,
                    d.Producto.Categoria
                })
                .Select(g => new ProductoEstadisticaDto
                {
                    ProductoId = g.Key.ProductoId,
                    NombreProducto = g.Key.Nombre,
                    Categoria = g.Key.Categoria,
                    TotalVendido = g.Sum(d => d.Cantidad),
                    TotalIngresos = g.Sum(d => d.Subtotal),
                    NumeroPedidos = g.Select(d => d.PedidoId).Distinct().Count()
                })
                .OrderByDescending(p => p.TotalVendido)
                .Take(request.TopProductos ?? 10)
                .ToList();

            dashboard.ProductosMasVendidos = productosVendidos;

            // Ventas por categoría
            dashboard.VentasPorCategoria = listaPedidos
                .SelectMany(p => p.Detalles)
                .GroupBy(d => d.Producto!.Categoria ?? "Sin Categoría")
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(d => d.Subtotal)
                );

            // Estadísticas por período
            var hoy = DateTime.Today;
            var inicioSemana = hoy.AddDays(-(int)hoy.DayOfWeek);
            var inicioMes = new DateTime(hoy.Year, hoy.Month, 1);

            var pedidosHoy = listaPedidos.Where(p => p.FechaHoraPedido.Date == hoy).ToList();
            var pedidosSemana = listaPedidos.Where(p => p.FechaHoraPedido.Date >= inicioSemana).ToList();
            var pedidosMes = listaPedidos.Where(p => p.FechaHoraPedido.Date >= inicioMes).ToList();

            dashboard.PedidosHoy = pedidosHoy.Count;
            dashboard.PedidosEstaSemana = pedidosSemana.Count;
            dashboard.PedidosEsteMes = pedidosMes.Count;

            dashboard.VentasHoy = pedidosHoy.Sum(p => p.Total);
            dashboard.VentasEstaSemana = pedidosSemana.Sum(p => p.Total);
            dashboard.VentasEsteMes = pedidosMes.Sum(p => p.Total);

            return dashboard;
        }
    }
}
