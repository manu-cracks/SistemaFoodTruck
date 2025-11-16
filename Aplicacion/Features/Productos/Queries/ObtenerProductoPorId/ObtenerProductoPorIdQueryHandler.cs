using Aplicacion.Contratos.Persistencia;
using Aplicacion.DTOs;
using MediatR;

namespace Aplicacion.Features.Productos.Queries.ObtenerProductoPorId
{
    /// <summary>
    /// Handler para obtener un producto por ID
    /// </summary>
    public class ObtenerProductoPorIdQueryHandler : IRequestHandler<ObtenerProductoPorIdQuery, ProductoDto?>
    {
        private readonly IProductoRepository _productoRepository;

        public ObtenerProductoPorIdQueryHandler(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<ProductoDto?> Handle(ObtenerProductoPorIdQuery request, CancellationToken cancellationToken)
        {
            var producto = await _productoRepository.ObtenerPorIdAsync(request.Id);

            if (producto == null)
                return null;

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
