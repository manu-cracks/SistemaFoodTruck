namespace Dominio.Entidades
{
    /// <summary>
    /// Entidad que representa un cliente del Food Truck
    /// </summary>
    public class Cliente
    {
        public int Id { get; set; }
        
        public string Nombre { get; set; } = string.Empty;
        
        public string? Email { get; set; }
        
        public string? Telefono { get; set; }

        // Relación de navegación: Un cliente puede tener múltiples pedidos
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
