using BookBackend.Modelos;
using Microsoft.EntityFrameworkCore;

namespace BookBackend.Data
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options)
        {
        }


        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Rol> Roles => Set<Rol>();
        public DbSet<UsuarioRol> UsuarioRoles => Set<UsuarioRol>();
        public DbSet<Sesion> Sesiones => Set<Sesion>();
        public DbSet<Carnet> Carnets => Set<Carnet>();
        public DbSet<Libro> Libros => Set<Libro>();
        public DbSet<Autor> Autores => Set<Autor>();
        public DbSet<LibroAutor> LibroAutores => Set<LibroAutor>();
        public DbSet<Categoria> Categorias => Set<Categoria>();
        public DbSet<LibroCategoria> LibroCategorias => Set<LibroCategoria>();
        public DbSet<Ejemplar> Ejemplares => Set<Ejemplar>();
        public DbSet<Prestamo> Prestamos => Set<Prestamo>();
        public DbSet<DetallePrestamo> DetallePrestamos => Set<DetallePrestamo>();
        public DbSet<Inspeccion> Inspecciones => Set<Inspeccion>();
        public DbSet<Reserva> Reservas => Set<Reserva>();
        public DbSet<Multa> Multas => Set<Multa>();
        public DbSet<Venta> Ventas => Set<Venta>();
        public DbSet<DetalleVenta> DetalleVentas => Set<DetalleVenta>();

        // Tablas catálogo
        public DbSet<EstadoEjemplar> EstadoEjemplares => Set<EstadoEjemplar>();
        public DbSet<CondicionEjemplar> CondicionEjemplares => Set<CondicionEjemplar>();
        public DbSet<EstadoPrestamo> EstadoPrestamos => Set<EstadoPrestamo>();
        public DbSet<EstadoReserva> EstadoReservas => Set<EstadoReserva>();
        public DbSet<EstadoVenta> EstadoVentas => Set<EstadoVenta>();
        public DbSet<MetodoPago> MetodoPagos => Set<MetodoPago>();
        public DbSet<TipoInspeccion> TipoInspecciones => Set<TipoInspeccion>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data para catálogos
            modelBuilder.Entity<EstadoEjemplar>().HasData(
                new EstadoEjemplar { Id = 1, Nombre = "Disponible" },
                new EstadoEjemplar { Id = 2, Nombre = "Prestado" },
                new EstadoEjemplar { Id = 3, Nombre = "Dañado" },
                new EstadoEjemplar { Id = 4, Nombre = "Perdido" },
                new EstadoEjemplar { Id = 5, Nombre = "Vendido" }
            );

            modelBuilder.Entity<CondicionEjemplar>().HasData(
                new CondicionEjemplar { Id = 1, Nombre = "Nuevo" },
                new CondicionEjemplar { Id = 2, Nombre = "Bueno" },
                new CondicionEjemplar { Id = 3, Nombre = "Regular" }
            );

            modelBuilder.Entity<EstadoPrestamo>().HasData(
                new EstadoPrestamo { Id = 1, Nombre = "Activo" },
                new EstadoPrestamo { Id = 2, Nombre = "Devuelto" },
                new EstadoPrestamo { Id = 3, Nombre = "Vencido" },
                new EstadoPrestamo { Id = 4, Nombre = "Cancelado" }
            );

            modelBuilder.Entity<EstadoReserva>().HasData(
                new EstadoReserva { Id = 1, Nombre = "Pendiente" },
                new EstadoReserva { Id = 2, Nombre = "Cumplida" },
                new EstadoReserva { Id = 3, Nombre = "Cancelada" }
            );

            modelBuilder.Entity<EstadoVenta>().HasData(
                new EstadoVenta { Id = 1, Nombre = "Completada" },
                new EstadoVenta { Id = 2, Nombre = "Cancelada" }
            );

            modelBuilder.Entity<MetodoPago>().HasData(
                new MetodoPago { Id = 1, Nombre = "Efectivo" },
                new MetodoPago { Id = 2, Nombre = "Tarjeta" },
                new MetodoPago { Id = 3, Nombre = "Transferencia" }
            );

            modelBuilder.Entity<TipoInspeccion>().HasData(
                new TipoInspeccion { Id = 1, Nombre = "Salida" },
                new TipoInspeccion { Id = 2, Nombre = "Entrada" }
            );

            // Eliminación en cascada segura para evitar ciclos múltiples
            modelBuilder.Entity<Prestamo>()
                .HasMany(p => p.DetallePrestamos)
                .WithOne(d => d.Prestamo)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DetallePrestamo>()
                .HasOne(d => d.Ejemplar)
                .WithMany(e => e.DetallePrestamos)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Inspeccion>()
                .HasOne(i => i.Prestamo)
                .WithMany(p => p.Inspecciones)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Inspeccion>()
                .HasOne(i => i.Ejemplar)
                .WithMany(e => e.Inspecciones)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Inspeccion>()
                .HasOne(i => i.InspeccionadoPor)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<DetalleVenta>()
                .HasOne(d => d.Venta)
                .WithMany(v => v.DetalleVentas)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DetalleVenta>()
                .HasOne(d => d.Ejemplar)
                .WithMany(e => e.DetalleVentas)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
