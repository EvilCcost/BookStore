using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookBackend.Modelos
{
    [PrimaryKey(nameof(UsuarioId), nameof(RolId))]   // Clave compuesta con DataAnnotations
    public class UsuarioRol
    {
        public int UsuarioId { get; set; }
        public int RolId { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        [InverseProperty(nameof(Usuario.UsuarioRoles))]
        public Usuario Usuario { get; set; } = null!;

        [ForeignKey(nameof(RolId))]
        [InverseProperty(nameof(Rol.UsuarioRoles))]
        public Rol Rol { get; set; } = null!;
    }

}
