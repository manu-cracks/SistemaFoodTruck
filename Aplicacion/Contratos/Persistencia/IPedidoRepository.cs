using Dominio.Entidades;

namespace Aplicacion.Contratos.Persistencia
{
    /// <summary>
    /// Contrato para el repositorio de Pedidos
    /// </summary>
    public interface IPedidoRepository
    {
        Task<Pedido?> ObtenerPorIdAsync(int id);
        Task<Pedido?> ObtenerPorIdConDetallesAsync(int id);
        Task<IEnumerable<Pedido>> ObtenerTodosAsync();
        Task<IEnumerable<Pedido>> ObtenerPorClienteAsync(int clienteId);
        Task<IEnumerable<Pedido>> ObtenerPorEstadoAsync(PedidoEstado estado);
        Task<IEnumerable<Pedido>> ObtenerPendientesAsync();
        Task<Pedido> CrearAsync(Pedido pedido);
        Task ActualizarAsync(Pedido pedido);
        Task EliminarAsync(int id);
    }
}
