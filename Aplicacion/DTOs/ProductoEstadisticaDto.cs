namespace Aplicacion.DTOs
{
    /// <summary>
    /// DTO para mostrar estadísticas de productos más vendidos
    /// </summary>
    public class ProductoEstadisticaDto
    {
        public int ProductoId { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public string? Categoria { get; set; }
        public int TotalVendido { get; set; }
        public decimal TotalIngresos { get; set; }
        public int NumeroPedidos { get; set; }
    }
}
