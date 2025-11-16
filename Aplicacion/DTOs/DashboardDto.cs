namespace Aplicacion.DTOs
{
    /// <summary>
    /// DTO para mostrar estadísticas generales del dashboard
    /// </summary>
    public class DashboardDto
    {
        // Estadísticas Generales
        public int TotalPedidosEntregados { get; set; }
        public decimal TotalVentas { get; set; }
        public decimal PromedioVentaPorPedido { get; set; }
        public int TotalProductosVendidos { get; set; }

        // Productos más vendidos
        public List<ProductoEstadisticaDto> ProductosMasVendidos { get; set; } = new();

        // Ventas por categoría
        public Dictionary<string, decimal> VentasPorCategoria { get; set; } = new();

        // Estadísticas por período
        public int PedidosHoy { get; set; }
        public int PedidosEstaSemana { get; set; }
        public int PedidosEsteMes { get; set; }
        public decimal VentasHoy { get; set; }
        public decimal VentasEstaSemana { get; set; }
        public decimal VentasEsteMes { get; set; }
    }
}
