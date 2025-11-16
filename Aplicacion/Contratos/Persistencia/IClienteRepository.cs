using Dominio.Entidades;

namespace Aplicacion.Contratos.Persistencia
{
    /// <summary>
    /// Contrato para el repositorio de Clientes
    /// </summary>
    public interface IClienteRepository
    {
        Task<Cliente?> ObtenerPorIdAsync(int id);
        Task<Cliente?> ObtenerPorEmailAsync(string email);
        Task<IEnumerable<Cliente>> ObtenerTodosAsync();
        Task<Cliente> CrearAsync(Cliente cliente);
        Task ActualizarAsync(Cliente cliente);
        Task EliminarAsync(int id);
    }
}
