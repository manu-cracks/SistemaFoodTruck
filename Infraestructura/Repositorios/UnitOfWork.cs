using Aplicacion.Contratos.Persistencia;
using Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infraestructura.Repositorios
{
    /// <summary>
    /// Implementación de la Unidad de Trabajo para coordinar transacciones
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FoodTruckDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(FoodTruckDbContext context,
            IProductoRepository productos,
            IClienteRepository clientes,
            IPedidoRepository pedidos)
        {
            _context = context;
            Productos = productos;
            Clientes = clientes;
            Pedidos = pedidos;
        }

        public IProductoRepository Productos { get; }
        public IClienteRepository Clientes { get; }
        public IPedidoRepository Pedidos { get; }

        public async Task<int> GuardarCambiosAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task IniciarTransaccionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task ConfirmarTransaccionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RevertirTransaccionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
