using Dominio.Entidades;

namespace Aplicacion.Contratos.Persistencia
{
    /// <summary>
    /// Contrato para el repositorio de Productos
    /// </summary>
    public interface IProductoRepository
    {
        Task<Producto?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Producto>> ObtenerTodosAsync();
        Task<IEnumerable<Producto>> ObtenerPorCategoriaAsync(string categoria);
        Task<Producto> CrearAsync(Producto producto);
        Task ActualizarAsync(Producto producto);
        Task EliminarAsync(int id);
    }
}
