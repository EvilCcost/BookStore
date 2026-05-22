using BookBackend.Modelos;
using BookBackend.Modelos.DTOs.UsuarioDto;
using BookBackend.Repositorio;
using BookBackend.Repositorio.UsuarioRepositorio;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookBackend.Servicios.UsuarioServicio
{
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly IUsuarioRepositorio _usuarioRepo;
        private readonly IRepositorio<UsuarioRol> _usuarioRolRepo;
        private readonly IRepositorio<Rol> _rolRepo;

        public UsuarioServicio(
            IUsuarioRepositorio usuarioRepo,
            IRepositorio<UsuarioRol> usuarioRolRepo,
            IRepositorio<Rol> rolRepo)
        {
            _usuarioRepo = usuarioRepo;
            _usuarioRolRepo = usuarioRolRepo;
            _rolRepo = rolRepo;
        }

        public async Task<List<UsuarioRespuestaDto>> GetAllAsync()
        {
            var usuarios = await _usuarioRepo.GetAllAsync(incluirPropiedades: "UsuarioRoles.Rol");
            return usuarios.Select(MapToDto).ToList();
        }

        public async Task<UsuarioRespuestaDto?> GetByIdAsync(int id)
        {
            var usuario = await _usuarioRepo.GetUsuarioWithRolesAsync(id);
            return usuario is null ? null : MapToDto(usuario);
        }

        public async Task<UsuarioRespuestaDto?> GetByEmailAsync(string email)
        {
            var usuario = await _usuarioRepo.GetByEmailAsync(email);
            return usuario is null ? null : MapToDto(usuario);
        }

        public async Task<UsuarioRespuestaDto> CreateAsync(CrearUsuarioDto crearDto)
        {
            var emailExiste = await _usuarioRepo.ExistsAsync(u => u.Email == crearDto.Email);
            if (emailExiste)
                throw new InvalidOperationException("El email ya está registrado.");

            var usuario = new Usuario
            {
                Nombre = crearDto.Nombre,
                Email = crearDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(crearDto.Password),
                Activo = true,
                FechaRegistro = DateTime.UtcNow
            };

            await _usuarioRepo.AddAsync(usuario);

            if (crearDto.RolIds is not null && crearDto.RolIds.Length > 0)
            {
                foreach (var rolId in crearDto.RolIds)
                {
                    var rolExiste = await _rolRepo.ExistsAsync(r => r.Id == rolId);
                    if (rolExiste)
                    {
                        await _usuarioRolRepo.AddAsync(new UsuarioRol
                        {
                            UsuarioId = usuario.Id,
                            RolId = rolId
                        });
                    }
                }
            }

            var usuarioCompleto = await _usuarioRepo.GetUsuarioWithRolesAsync(usuario.Id);
            return MapToDto(usuarioCompleto!);
        }

        public async Task<UsuarioRespuestaDto?> UpdateAsync(int id, ActualizarUsuarioDto actualizarDto)
        {
            var usuario = await _usuarioRepo.GetByIdAsync(id);
            if (usuario is null) return null;

            var emailExiste = await _usuarioRepo.ExistsAsync(u => u.Email == actualizarDto.Email && u.Id != id);
            if (emailExiste)
                throw new InvalidOperationException("El email ya está en uso por otro usuario.");

            usuario.Nombre = actualizarDto.Nombre;
            usuario.Email = actualizarDto.Email;
            usuario.Activo = actualizarDto.Activo;

            await _usuarioRepo.UpdateAsync(usuario);

            if (actualizarDto.RolIds is not null)
            {
                var rolesActuales = await _usuarioRolRepo.GetAllAsync(ur => ur.UsuarioId == id);
                foreach (var rol in rolesActuales)
                    await _usuarioRolRepo.DeleteAsync(rol);

                foreach (var rolId in actualizarDto.RolIds)
                {
                    var rolExiste = await _rolRepo.ExistsAsync(r => r.Id == rolId);
                    if (rolExiste)
                    {
                        await _usuarioRolRepo.AddAsync(new UsuarioRol
                        {
                            UsuarioId = id,
                            RolId = rolId
                        });
                    }
                }
            }

            var usuarioCompleto = await _usuarioRepo.GetUsuarioWithRolesAsync(id);
            return MapToDto(usuarioCompleto!);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var usuario = await _usuarioRepo.GetByIdAsync(id);
            if (usuario is null) return false;

            var rolesAsociados = await _usuarioRolRepo.GetAllAsync(ur => ur.UsuarioId == id);
            foreach (var rol in rolesAsociados)
                await _usuarioRolRepo.DeleteAsync(rol);

            await _usuarioRepo.DeleteAsync(usuario);
            return true;
        }

        public async Task<bool> ToggleActivoAsync(int id)
        {
            var usuario = await _usuarioRepo.GetByIdAsync(id);
            if (usuario is null) return false;

            usuario.Activo = !usuario.Activo;
            await _usuarioRepo.UpdateAsync(usuario);
            return true;
        }

        public async Task<LoginUsuarioRespuestaDto> LoginAsync(LoginUsuarioDto logindto)
        {

            var usuario = await _usuarioRepo.GetByEmailAsync(logindto.Email);
            if (usuario is null || !BCrypt.Net.BCrypt.Verify(logindto.Password, usuario.PasswordHash))
                throw new UnauthorizedAccessException("Credenciales Invalidas.");
            if (!usuario.Activo)
                throw new UnauthorizedAccessException("Usuario Inactivo.");

            var token = GenerarJwtToken(usuario);
            return new LoginUsuarioRespuestaDto
            {
                Token = token,
                Usuario = MapToDto(usuario),
                Expiracion = DateTime.UtcNow.AddHours(1)
            };

        }

        private static UsuarioRespuestaDto MapToDto(Usuario usuario)
        {
            return new UsuarioRespuestaDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                Activo = usuario.Activo,
                FechaRegistro = usuario.FechaRegistro,
                Roles = usuario.UsuarioRoles?.Select(ur => ur.Rol.Nombre).ToList() ?? new List<string>()
            };
        }

        private string GenerarJwtToken(Usuario usuario)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET")!)
                );

            var credencial = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new (ClaimTypes.Email, usuario.Email),
                new (ClaimTypes.Name, usuario.Nombre)
            };

            foreach (var rol in usuario.UsuarioRoles?.Select(ur => ur.Rol.Nombre) ?? [])
                claims.Add(new Claim(ClaimTypes.Role, rol));

            var token = new JwtSecurityToken(
                issuer: "BookBackend",
                audience: "BookBackend",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credencial
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
