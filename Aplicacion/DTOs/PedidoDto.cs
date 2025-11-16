using Dominio.Entidades;

namespace Aplicacion.DTOs
{
    /// <summary>
    /// DTO para transferir información de pedidos
    /// </summary>
    public class PedidoDto
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string? NombreCliente { get; set; }
        public DateTime FechaHoraPedido { get; set; }
        public PedidoEstado Estado { get; set; }
        public decimal Total { get; set; }
        public List<DetallePedidoDto> Detalles { get; set; } = new();
    }
}
