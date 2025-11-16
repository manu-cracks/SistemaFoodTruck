using Aplicacion.DTOs;
using MediatR;

namespace Aplicacion.Features.Productos.Queries.ObtenerProductoPorId
{
    /// <summary>
    /// Query para obtener un producto por su ID
    /// </summary>
    public class ObtenerProductoPorIdQuery : IRequest<ProductoDto?>
    {
        public int Id { get; set; }

        public ObtenerProductoPorIdQuery(int id)
        {
            Id = id;
        }
    }
}
