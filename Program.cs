using BookBackend.Data;
using BookBackend.Repositorio;
using BookBackend.Repositorio.AutorRepositorio;
using BookBackend.Repositorio.CarnetRepositorio;
using BookBackend.Repositorio.CategoriaRepositorio;
using BookBackend.Repositorio.CatalogoRepositorio;
using BookBackend.Repositorio.EjemplarRepositorio;
using BookBackend.Repositorio.InspeccionRepositorio;
using BookBackend.Repositorio.LibroRepositorio;
using BookBackend.Repositorio.MultaRepositorio;
using BookBackend.Repositorio.PrestamoRepositorio;
using BookBackend.Repositorio.ReservaRepositorio;
using BookBackend.Repositorio.RolRepositorio;
using BookBackend.Repositorio.SesionRepositorio;
using BookBackend.Repositorio.UsuarioRepositorio;
using BookBackend.Repositorio.VentaRepositorio;
using BookBackend.Servicios.AutorServicio;
using BookBackend.Servicios.CarnetServicio;
using BookBackend.Servicios.CatalogoServicio;
using BookBackend.Servicios.CategoriaServicio;
using BookBackend.Servicios.EjemplarServicio;
using BookBackend.Servicios.InspeccionServicio;
using BookBackend.Servicios.LibroServicio;
using BookBackend.Servicios.MultaServicio;
using BookBackend.Servicios.PrestamoServicio;
using BookBackend.Servicios.ReservaServicio;
using BookBackend.Servicios.RolServicio;
using BookBackend.Servicios.SesionServicio;
using BookBackend.Servicios.UsuarioServicio;
using BookBackend.Servicios.VentaServicio;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
// Clave secreta para JWT
var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET")
    ?? throw new InvalidOperationException("JWT_SECRET no está configurado en el archivo .env");

var key = Encoding.UTF8.GetBytes(jwtSecret);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = "BookBackend",
        ValidateAudience = true,
        ValidAudience = "BookBackend",
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero


    };
});


//Servicio de Conexion a la base de datos
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");

builder.Services.AddDbContext<BookStoreContext>(options =>
    options.UseNpgsql(connectionString));

// Repositorio genérico
builder.Services.AddScoped(typeof(IRepositorio<>), typeof(Repositorio<>));

// Repositorios específicos
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<ILibroRepositorio, LibroRepositorio>();
builder.Services.AddScoped<IAutorRepositorio, AutorRepositorio>();
builder.Services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
builder.Services.AddScoped<IEjemplarRepositorio, EjemplarRepositorio>();
builder.Services.AddScoped<IPrestamoRepositorio, PrestamoRepositorio>();
builder.Services.AddScoped<IReservaRepositorio, ReservaRepositorio>();
builder.Services.AddScoped<IVentaRepositorio, VentaRepositorio>();
builder.Services.AddScoped<IInspeccionRepositorio, InspeccionRepositorio>();
builder.Services.AddScoped<IMultaRepositorio, MultaRepositorio>();
builder.Services.AddScoped<ICarnetRepositorio, CarnetRepositorio>();
builder.Services.AddScoped<ISesionRepositorio, SesionRepositorio>();
builder.Services.AddScoped<IRolRepositorio, RolRepositorio>();
builder.Services.AddScoped<IEstadoEjemplarRepositorio, EstadoEjemplarRepositorio>();
builder.Services.AddScoped<ICondicionEjemplarRepositorio, CondicionEjemplarRepositorio>();
builder.Services.AddScoped<IEstadoPrestamoRepositorio, EstadoPrestamoRepositorio>();
builder.Services.AddScoped<IEstadoReservaRepositorio, EstadoReservaRepositorio>();
builder.Services.AddScoped<IEstadoVentaRepositorio, EstadoVentaRepositorio>();
builder.Services.AddScoped<IMetodoPagoRepositorio, MetodoPagoRepositorio>();
builder.Services.AddScoped<ITipoInspeccionRepositorio, TipoInspeccionRepositorio>();

// Servicios
builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();
builder.Services.AddScoped<ILibroServicio, LibroServicio>();
builder.Services.AddScoped<IAutorServicio, AutorServicio>();
builder.Services.AddScoped<ICategoriaServicio, CategoriaServicio>();
builder.Services.AddScoped<IEjemplarServicio, EjemplarServicio>();
builder.Services.AddScoped<IPrestamoServicio, PrestamoServicio>();
builder.Services.AddScoped<IReservaServicio, ReservaServicio>();
builder.Services.AddScoped<IVentaServicio, VentaServicio>();
builder.Services.AddScoped<IInspeccionServicio, InspeccionServicio>();
builder.Services.AddScoped<IMultaServicio, MultaServicio>();
builder.Services.AddScoped<ICarnetServicio, CarnetServicio>();
builder.Services.AddScoped<ISesionServicio, SesionServicio>();
builder.Services.AddScoped<IRolServicio, RolServicio>();
builder.Services.AddScoped<IEstadoEjemplarService, EstadoEjemplarService>();
builder.Services.AddScoped<ICondicionEjemplarService, CondicionEjemplarService>();
builder.Services.AddScoped<IEstadoPrestamoService, EstadoPrestamoService>();
builder.Services.AddScoped<IEstadoReservaService, EstadoReservaService>();
builder.Services.AddScoped<IEstadoVentaService, EstadoVentaService>();
builder.Services.AddScoped<IMetodoPagoService, MetodoPagoService>();
builder.Services.AddScoped<ITipoInspeccionService, TipoInspeccionService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header. Ejemplo: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
