namespace Aplicacion.Contratos.Persistencia
{
    /// <summary>
    /// Unidad de trabajo para coordinar transacciones entre repositorios
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IProductoRepository Productos { get; }
        IClienteRepository Clientes { get; }
        IPedidoRepository Pedidos { get; }
        
        Task<int> GuardarCambiosAsync(CancellationToken cancellationToken = default);
        Task IniciarTransaccionAsync();
        Task ConfirmarTransaccionAsync();
        Task RevertirTransaccionAsync();
    }
}
