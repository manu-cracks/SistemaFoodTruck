using Aplicacion.DTOs;
using MediatR;

namespace Aplicacion.Features.Productos.Commands.CrearProducto
{
    /// <summary>
    /// Comando para crear un nuevo producto en el menú
    /// </summary>
    public class CrearProductoCommand : IRequest<ProductoDto>
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string? Categoria { get; set; }
    }
}
