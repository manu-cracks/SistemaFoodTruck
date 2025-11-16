namespace Dominio.Entidades
{
    /// <summary>
    /// Entidad que representa un pedido realizado por un cliente
    /// </summary>
    public class Pedido
    {
        public int Id { get; set; }
        
        // Relación con Cliente (Clave Foránea)
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        
        public DateTime FechaHoraPedido { get; set; } = DateTime.Now;
        
        public PedidoEstado Estado { get; set; } = PedidoEstado.Pendiente;
        
        public decimal Total { get; set; }

        // Relación de navegación: Un pedido puede tener múltiples detalles
        public ICollection<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();
    }
}
