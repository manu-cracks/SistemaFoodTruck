using Aplicacion.Contratos.Persistencia;
using Aplicacion.DTOs;
using MediatR;

namespace Aplicacion.Features.Productos.Commands.ActualizarProducto
{
    /// <summary>
    /// Handler para actualizar un producto
    /// </summary>
    public class ActualizarProductoCommandHandler : IRequestHandler<ActualizarProductoCommand, ProductoDto>
    {
        private readonly IProductoRepository _productoRepository;

        public ActualizarProductoCommandHandler(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<ProductoDto> Handle(ActualizarProductoCommand request, CancellationToken cancellationToken)
        {
            var producto = await _productoRepository.ObtenerPorIdAsync(request.Id);
            
            if (producto == null)
            {
                throw new KeyNotFoundException($"Producto con ID {request.Id} no encontrado");
            }

            producto.Nombre = request.Nombre;
            producto.Descripcion = request.Descripcion;
            producto.Precio = request.Precio;
            producto.Categoria = request.Categoria;

            await _productoRepository.ActualizarAsync(producto);

            return new ProductoDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Categoria = producto.Categoria
            };
        }
    }
}
