namespace Aplicacion.DTOs
{
    /// <summary>
    /// DTO para transferir información de productos
    /// </summary>
    public class ProductoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string? Categoria { get; set; }
    }
}
