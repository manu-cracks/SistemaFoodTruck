namespace Aplicacion.DTOs
{
    /// <summary>
    /// DTO para transferir información de clientes
    /// </summary>
    public class ClienteDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Telefono { get; set; }
    }
}
