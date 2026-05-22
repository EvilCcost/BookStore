using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookBackend.Modelos
{
    [Index(nameof(Email), IsUnique = true)]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Nombre { get; set; } = null!;

        [Required, MaxLength(150)]
        public string Email { get; set; } = null!;

        [Required, MaxLength(256)]
        public string PasswordHash { get; set; } = null!;

        public bool Activo { get; set; } = true;

        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        [InverseProperty(nameof(UsuarioRol.Usuario))]
        public ICollection<UsuarioRol> UsuarioRoles { get; set; } = new List<UsuarioRol>();

        [InverseProperty(nameof(Sesion.Usuario))]
        public ICollection<Sesion> Sesiones { get; set; } = new List<Sesion>();

        public Carnet? Carnet { get; set; }

        [InverseProperty(nameof(Prestamo.Usuario))]
        public ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();

        [InverseProperty(nameof(Reserva.Usuario))]
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

        [InverseProperty(nameof(Multa.Usuario))]
        public ICollection<Multa> Multas { get; set; } = new List<Multa>();
    }
}
