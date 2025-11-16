using Aplicacion.Contratos.Persistencia;
using Dominio.Entidades;
using Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Repositorios
{
    /// <summary>
    /// Implementación del repositorio de Pedidos usando Entity Framework Core
    /// </summary>
    public class PedidoRepository : IPedidoRepository
    {
        private readonly FoodTruckDbContext _context;

        public PedidoRepository(FoodTruckDbContext context)
        {
            _context = context;
        }

        public async Task<Pedido?> ObtenerPorIdAsync(int id)
        {
            return await _context.Pedidos.FindAsync(id);
        }

        public async Task<Pedido?> ObtenerPorIdConDetallesAsync(int id)
        {
            return await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Detalles)
                    .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Pedido>> ObtenerTodosAsync()
        {
            return await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Detalles)
                    .ThenInclude(d => d.Producto)
                .OrderByDescending(p => p.FechaHoraPedido)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pedido>> ObtenerPorClienteAsync(int clienteId)
        {
            return await _context.Pedidos
                .Include(p => p.Detalles)
                    .ThenInclude(d => d.Producto)
                .Where(p => p.ClienteId == clienteId)
                .OrderByDescending(p => p.FechaHoraPedido)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pedido>> ObtenerPorEstadoAsync(PedidoEstado estado)
        {
            return await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Detalles)
                    .ThenInclude(d => d.Producto)
                .Where(p => p.Estado == estado)
                .OrderByDescending(p => p.FechaHoraPedido)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pedido>> ObtenerPendientesAsync()
        {
            return await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Detalles)
                    .ThenInclude(d => d.Producto)
                .Where(p => p.Estado == PedidoEstado.Pendiente || 
                           p.Estado == PedidoEstado.EnPreparacion ||
                           p.Estado == PedidoEstado.ListoParaRecoger)
                .OrderByDescending(p => p.FechaHoraPedido)
                .ToListAsync();
        }

        public async Task<Pedido> CrearAsync(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }

        public async Task ActualizarAsync(Pedido pedido)
        {
            _context.Entry(pedido).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
                await _context.SaveChangesAsync();
            }
        }
    }
}
