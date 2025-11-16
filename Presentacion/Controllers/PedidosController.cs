using Aplicacion.Contratos.Persistencia;
using Aplicacion.Features.Pedidos.Commands.ActualizarEstadoPedido;
using Aplicacion.Features.Pedidos.Commands.CrearPedido;
using Aplicacion.Features.Pedidos.Queries.ObtenerPedidosPendientes;
using Dominio.Entidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentacion.Controllers
{
    /// <summary>
    /// Controlador para la gestión de pedidos (interfaz de cliente y administración)
    /// </summary>
    public class PedidosController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IProductoRepository _productoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IPedidoRepository _pedidoRepository;

        public PedidosController(
            IMediator mediator,
            IProductoRepository productoRepository,
            IClienteRepository clienteRepository,
            IPedidoRepository pedidoRepository)
        {
            _mediator = mediator;
            _productoRepository = productoRepository;
            _clienteRepository = clienteRepository;
            _pedidoRepository = pedidoRepository;
        }

        // GET: Pedidos
        /// <summary>
        /// Muestra todos los pedidos (interfaz de administración)
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var pedidos = await _pedidoRepository.ObtenerTodosAsync();
            return View(pedidos);
        }

        // GET: Pedidos/Pendientes
        /// <summary>
        /// Muestra los pedidos pendientes (interfaz de administración)
        /// </summary>
        public async Task<IActionResult> Pendientes()
        {
            var query = new ObtenerPedidosPendientesQuery();
            var pedidos = await _mediator.Send(query);
            return View(pedidos);
        }

        // GET: Pedidos/Details/5
        /// <summary>
        /// Muestra los detalles de un pedido
        /// </summary>
        public async Task<IActionResult> Details(int id)
        {
            var pedido = await _pedidoRepository.ObtenerPorIdConDetallesAsync(id);
            
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedidos/Create
        /// <summary>
        /// Muestra el formulario para crear un nuevo pedido (interfaz de cliente)
        /// </summary>
        public async Task<IActionResult> Create()
        {
            await CargarDatosFormulario();
            return View(new CrearPedidoViewModel());
        }

        // POST: Pedidos/Create
        /// <summary>
        /// Procesa la creación de un nuevo pedido
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CrearPedidoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                await CargarDatosFormulario();
                return View(viewModel);
            }

            try
            {
                var command = new CrearPedidoCommand
                {
                    ClienteId = viewModel.ClienteId,
                    Detalles = viewModel.Detalles
                        .Where(d => d.Cantidad > 0)
                        .Select(d => new CrearDetallePedidoDto
                        {
                            ProductoId = d.ProductoId,
                            Cantidad = d.Cantidad
                        }).ToList()
                };

                if (!command.Detalles.Any())
                {
                    ModelState.AddModelError("", "Debe agregar al menos un producto al pedido");
                    await CargarDatosFormulario();
                    return View(viewModel);
                }

                var pedido = await _mediator.Send(command);
                TempData["Mensaje"] = $"Pedido #{pedido.Id} creado exitosamente. Total: S/ {pedido.Total:F2}";
                return RedirectToAction(nameof(Details), new { id = pedido.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al crear el pedido: {ex.Message}");
                await CargarDatosFormulario();
                return View(viewModel);
            }
        }

        // GET: Pedidos/ActualizarEstado/5
        /// <summary>
        /// Muestra el formulario para actualizar el estado de un pedido
        /// </summary>
        public async Task<IActionResult> ActualizarEstado(int id)
        {
            var pedido = await _pedidoRepository.ObtenerPorIdConDetallesAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            ViewBag.Estados = new SelectList(Enum.GetValues(typeof(PedidoEstado)));
            return View(pedido);
        }

        // POST: Pedidos/ActualizarEstado
        /// <summary>
        /// Procesa la actualización del estado de un pedido
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarEstado(int id, PedidoEstado nuevoEstado)
        {
            try
            {
                var command = new ActualizarEstadoPedidoCommand
                {
                    PedidoId = id,
                    NuevoEstado = nuevoEstado
                };

                await _mediator.Send(command);
                TempData["Mensaje"] = "Estado del pedido actualizado exitosamente";
                return RedirectToAction(nameof(Pendientes));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error al actualizar el estado: {ex.Message}";
                return RedirectToAction(nameof(Details), new { id });
            }
        }

        /// <summary>
        /// Método auxiliar para cargar datos del formulario
        /// </summary>
        private async Task CargarDatosFormulario()
        {
            var clientes = await _clienteRepository.ObtenerTodosAsync();
            var productos = await _productoRepository.ObtenerTodosAsync();

            ViewBag.Clientes = new SelectList(clientes, "Id", "Nombre");
            ViewBag.Productos = productos.ToList();
        }
    }

    /// <summary>
    /// ViewModel para la creación de pedidos
    /// </summary>
    public class CrearPedidoViewModel
    {
        public int ClienteId { get; set; }
        public List<DetalleViewModel> Detalles { get; set; } = new();
    }

    public class DetalleViewModel
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
    }
}
