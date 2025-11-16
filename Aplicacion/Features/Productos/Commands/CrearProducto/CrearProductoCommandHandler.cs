using Aplicacion.Contratos.Persistencia;
using Aplicacion.DTOs;
using Dominio.Entidades;
using MediatR;

namespace Aplicacion.Features.Productos.Commands.CrearProducto
{
    /// <summary>
    /// Handler para crear un producto
    /// </summary>
    public class CrearProductoCommandHandler : IRequestHandler<CrearProductoCommand, ProductoDto>
    {
        private readonly IProductoRepository _productoRepository;

        public CrearProductoCommandHandler(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<ProductoDto> Handle(CrearProductoCommand request, CancellationToken cancellationToken)
        {
            var producto = new Producto
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                Precio = request.Precio,
                Categoria = request.Categoria
            };

            var productoCreado = await _productoRepository.CrearAsync(producto);

            return new ProductoDto
            {
                Id = productoCreado.Id,
                Nombre = productoCreado.Nombre,
                Descripcion = productoCreado.Descripcion,
                Precio = productoCreado.Precio,
                Categoria = productoCreado.Categoria
            };
        }
    }
}
