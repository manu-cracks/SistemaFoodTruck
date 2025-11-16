using Aplicacion.DTOs;
using Aplicacion.Features.Productos.Commands.ActualizarProducto;
using Aplicacion.Features.Productos.Commands.CrearProducto;
using Aplicacion.Features.Productos.Queries.ObtenerProductoPorId;
using Aplicacion.Contratos.Persistencia;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    /// <summary>
    /// Controlador para la gestión del menú de productos (interfaz de administración)
    /// </summary>
    public class ProductosController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IProductoRepository _productoRepository;

        public ProductosController(IMediator mediator, IProductoRepository productoRepository)
        {
            _mediator = mediator;
            _productoRepository = productoRepository;
        }

        // GET: Productos
        /// <summary>
        /// Muestra la lista de productos del menú
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var productos = await _productoRepository.ObtenerTodosAsync();
            var productosDto = productos.Select(p => new ProductoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Precio = p.Precio,
                Categoria = p.Categoria
            }).ToList();

            return View(productosDto);
        }

        // GET: Productos/Details/5
        /// <summary>
        /// Muestra los detalles de un producto
        /// </summary>
        public async Task<IActionResult> Details(int id)
        {
            var query = new ObtenerProductoPorIdQuery(id);
            var producto = await _mediator.Send(query);

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        /// <summary>
        /// Muestra el formulario para crear un nuevo producto
        /// </summary>
        public IActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        /// <summary>
        /// Procesa la creación de un nuevo producto
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CrearProductoCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            try
            {
                var producto = await _mediator.Send(command);
                TempData["Mensaje"] = "Producto creado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al crear el producto: {ex.Message}");
                return View(command);
            }
        }

        // GET: Productos/Edit/5
        /// <summary>
        /// Muestra el formulario para editar un producto
        /// </summary>
        public async Task<IActionResult> Edit(int id)
        {
            var query = new ObtenerProductoPorIdQuery(id);
            var producto = await _mediator.Send(query);

            if (producto == null)
            {
                return NotFound();
            }

            var command = new ActualizarProductoCommand
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Categoria = producto.Categoria
            };

            return View(command);
        }

        // POST: Productos/Edit/5
        /// <summary>
        /// Procesa la actualización de un producto
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ActualizarProductoCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(command);
            }

            try
            {
                await _mediator.Send(command);
                TempData["Mensaje"] = "Producto actualizado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al actualizar el producto: {ex.Message}");
                return View(command);
            }
        }

        // GET: Productos/Delete/5
        /// <summary>
        /// Muestra la confirmación de eliminación
        /// </summary>
        public async Task<IActionResult> Delete(int id)
        {
            var query = new ObtenerProductoPorIdQuery(id);
            var producto = await _mediator.Send(query);

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        /// <summary>
        /// Procesa la eliminación de un producto
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _productoRepository.EliminarAsync(id);
                TempData["Mensaje"] = "Producto eliminado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error al eliminar el producto: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
