using Aplicacion.Contratos.Persistencia;
using Dominio.Entidades;
using Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Repositorios
{
    /// <summary>
    /// Implementación del repositorio de Productos usando Entity Framework Core
    /// </summary>
    public class ProductoRepository : IProductoRepository
    {
        private readonly FoodTruckDbContext _context;

        public ProductoRepository(FoodTruckDbContext context)
        {
            _context = context;
        }

        public async Task<Producto?> ObtenerPorIdAsync(int id)
        {
            return await _context.Productos.FindAsync(id);
        }

        public async Task<IEnumerable<Producto>> ObtenerTodosAsync()
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task<IEnumerable<Producto>> ObtenerPorCategoriaAsync(string categoria)
        {
            return await _context.Productos
                .Where(p => p.Categoria == categoria)
                .ToListAsync();
        }

        public async Task<Producto> CrearAsync(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task ActualizarAsync(Producto producto)
        {
            _context.Entry(producto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
