using Aplicacion.DTOs;
using MediatR;

namespace Aplicacion.Features.Productos.Commands.ActualizarProducto
{
    /// <summary>
    /// Comando para actualizar un producto existente
    /// </summary>
    public class ActualizarProductoCommand : IRequest<ProductoDto>
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string? Categoria { get; set; }
    }
}
