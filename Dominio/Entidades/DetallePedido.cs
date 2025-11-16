namespace Dominio.Entidades
{
    /// <summary>
    /// Entidad que representa el detalle (línea) de un pedido
    /// </summary>
    public class DetallePedido
    {
        public int Id { get; set; }
        
        // Relación con Pedido (Clave Foránea)
        public int PedidoId { get; set; }
        public Pedido? Pedido { get; set; }
        
        // Relación con Producto (Clave Foránea)
        public int ProductoId { get; set; }
        public Producto? Producto { get; set; }
        
        public int Cantidad { get; set; }
        
        public decimal Subtotal { get; set; }
    }
}
