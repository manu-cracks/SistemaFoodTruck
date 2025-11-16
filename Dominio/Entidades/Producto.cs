namespace Dominio.Entidades
{
    /// <summary>
    /// Entidad que representa un producto del Food Truck
    /// </summary>
    public class Producto
    {
        public int Id { get; set; }
        
        public string Nombre { get; set; } = string.Empty;
        
        public string? Descripcion { get; set; }
        
        public decimal Precio { get; set; }
        
        public string? Categoria { get; set; }

        // Relación de navegación: Un producto puede estar en múltiples detalles de pedido
        public ICollection<DetallePedido> DetallesPedido { get; set; } = new List<DetallePedido>();
    }
}
