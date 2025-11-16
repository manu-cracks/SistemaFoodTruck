using Aplicacion.Contratos.Persistencia;
using Aplicacion.DTOs;
using Dominio.Entidades;
using MediatR;

namespace Aplicacion.Features.Pedidos.Commands.CrearPedido
{
    /// <summary>
    /// Handler para crear un pedido con transacción
    /// </summary>
    public class CrearPedidoCommandHandler : IRequestHandler<CrearPedidoCommand, PedidoDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CrearPedidoCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PedidoDto> Handle(CrearPedidoCommand request, CancellationToken cancellationToken)
        {
            // Validar que el cliente exista
            var cliente = await _unitOfWork.Clientes.ObtenerPorIdAsync(request.ClienteId);
            if (cliente == null)
            {
                throw new KeyNotFoundException($"Cliente con ID {request.ClienteId} no encontrado");
            }

            // Iniciar transacción
            await _unitOfWork.IniciarTransaccionAsync();

            try
            {
                // Crear el pedido
                var pedido = new Pedido
                {
                    ClienteId = request.ClienteId,
                    FechaHoraPedido = DateTime.Now,
                    Estado = PedidoEstado.Pendiente,
                    Total = 0
                };

                decimal total = 0;

                // Crear los detalles del pedido
                foreach (var detalleRequest in request.Detalles)
                {
                    var producto = await _unitOfWork.Productos.ObtenerPorIdAsync(detalleRequest.ProductoId);
                    
                    if (producto == null)
                    {
                        throw new KeyNotFoundException($"Producto con ID {detalleRequest.ProductoId} no encontrado");
                    }

                    var subtotal = producto.Precio * detalleRequest.Cantidad;
                    total += subtotal;

                    var detalle = new DetallePedido
                    {
                        ProductoId = detalleRequest.ProductoId,
                        Cantidad = detalleRequest.Cantidad,
                        Subtotal = subtotal
                    };

                    pedido.Detalles.Add(detalle);
                }

                pedido.Total = total;

                // Guardar el pedido
                var pedidoCreado = await _unitOfWork.Pedidos.CrearAsync(pedido);
                await _unitOfWork.GuardarCambiosAsync(cancellationToken);
                
                // Confirmar transacción
                await _unitOfWork.ConfirmarTransaccionAsync();

                // Obtener el pedido completo con las relaciones
                var pedidoCompleto = await _unitOfWork.Pedidos.ObtenerPorIdConDetallesAsync(pedidoCreado.Id);

                if (pedidoCompleto == null)
                {
                    throw new InvalidOperationException("Error al obtener el pedido creado");
                }

                // Mapear a DTO
                return new PedidoDto
                {
                    Id = pedidoCompleto.Id,
                    ClienteId = pedidoCompleto.ClienteId,
                    NombreCliente = pedidoCompleto.Cliente?.Nombre,
                    FechaHoraPedido = pedidoCompleto.FechaHoraPedido,
                    Estado = pedidoCompleto.Estado,
                    Total = pedidoCompleto.Total,
                    Detalles = pedidoCompleto.Detalles.Select(d => new DetallePedidoDto
                    {
                        Id = d.Id,
                        PedidoId = d.PedidoId,
                        ProductoId = d.ProductoId,
                        NombreProducto = d.Producto?.Nombre,
                        PrecioUnitario = d.Producto?.Precio ?? 0,
                        Cantidad = d.Cantidad,
                        Subtotal = d.Subtotal
                    }).ToList()
                };
            }
            catch
            {
                // Revertir transacción en caso de error
                await _unitOfWork.RevertirTransaccionAsync();
                throw;
            }
        }
    }
}
