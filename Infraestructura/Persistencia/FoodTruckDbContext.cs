using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Persistencia
{
    /// <summary>
    /// Contexto de base de datos para el sistema Food Truck
    /// </summary>
    public class FoodTruckDbContext : DbContext
    {
        public FoodTruckDbContext(DbContextOptions<FoodTruckDbContext> options) : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetallePedido> DetallesPedido { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de Producto
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Descripcion).HasMaxLength(500);
                entity.Property(p => p.Precio).HasColumnType("decimal(18,2)");
                entity.Property(p => p.Categoria).HasMaxLength(50);
            });

            // Configuración de Cliente
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Email).HasMaxLength(100);
                entity.Property(c => c.Telefono).HasMaxLength(20);
            });

            // Configuración de Pedido
            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.FechaHoraPedido).IsRequired();
                entity.Property(p => p.Estado).IsRequired();
                entity.Property(p => p.Total).HasColumnType("decimal(18,2)");

                // Relación con Cliente
                entity.HasOne(p => p.Cliente)
                    .WithMany(c => c.Pedidos)
                    .HasForeignKey(p => p.ClienteId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de DetallePedido
            modelBuilder.Entity<DetallePedido>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Cantidad).IsRequired();
                entity.Property(d => d.Subtotal).HasColumnType("decimal(18,2)");

                // Relación con Pedido
                entity.HasOne(d => d.Pedido)
                    .WithMany(p => p.Detalles)
                    .HasForeignKey(d => d.PedidoId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Relación con Producto
                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.DetallesPedido)
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Datos semilla (seed data)
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Productos iniciales con precios en Soles Peruanos (S/)
            modelBuilder.Entity<Producto>().HasData(
                new Producto { Id = 1, Nombre = "Hamburguesa Clásica", Descripcion = "Hamburguesa con carne, lechuga, tomate y queso", Precio = 25.00m, Categoria = "Hamburguesas" },
                new Producto { Id = 2, Nombre = "Hot Dog Premium", Descripcion = "Hot dog con salchicha de res y ingredientes premium", Precio = 18.00m, Categoria = "Hot Dogs" },
                new Producto { Id = 3, Nombre = "Papas Fritas", Descripcion = "Papas fritas crujientes", Precio = 10.00m, Categoria = "Acompañamientos" },
                new Producto { Id = 4, Nombre = "Refresco", Descripcion = "Bebida gaseosa 500ml", Precio = 5.00m, Categoria = "Bebidas" },
                new Producto { Id = 5, Nombre = "Taco Mexicano", Descripcion = "Taco con carne, cilantro y cebolla", Precio = 12.00m, Categoria = "Tacos" }
            );

            // Cliente de ejemplo
            modelBuilder.Entity<Cliente>().HasData(
                new Cliente { Id = 1, Nombre = "Cliente Demo", Email = "demo@foodtruck.com", Telefono = "555-1234" }
            );
        }
    }
}
