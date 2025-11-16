namespace Dominio.Entidades
{
    /// <summary>
    /// Estados posibles de un pedido en el sistema
    /// </summary>
    public enum PedidoEstado
    {
        Pendiente,
        EnPreparacion,
        ListoParaRecoger,
        Entregado,
        Cancelado
    }
}
