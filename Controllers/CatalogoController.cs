using BookBackend.Modelos.DTOs.CatalogoDto;
using BookBackend.Servicios.CatalogoServicio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBackend.Controllers
{
    [Route("api/catalogos")]
    [ApiController]
    [Authorize(Roles = "Administrador,Bibliotecario,Cliente")]
    public class CatalogoController : ControllerBase
    {
        private readonly IEstadoEjemplarService _estadoEjemplarSvc;
        private readonly ICondicionEjemplarService _condicionEjemplarSvc;
        private readonly IEstadoPrestamoService _estadoPrestamoSvc;
        private readonly IEstadoReservaService _estadoReservaSvc;
        private readonly IEstadoVentaService _estadoVentaSvc;
        private readonly IMetodoPagoService _metodoPagoSvc;
        private readonly ITipoInspeccionService _tipoInspeccionSvc;

        public CatalogoController(
            IEstadoEjemplarService estadoEjemplarSvc,
            ICondicionEjemplarService condicionEjemplarSvc,
            IEstadoPrestamoService estadoPrestamoSvc,
            IEstadoReservaService estadoReservaSvc,
            IEstadoVentaService estadoVentaSvc,
            IMetodoPagoService metodoPagoSvc,
            ITipoInspeccionService tipoInspeccionSvc)
        {
            _estadoEjemplarSvc = estadoEjemplarSvc;
            _condicionEjemplarSvc = condicionEjemplarSvc;
            _estadoPrestamoSvc = estadoPrestamoSvc;
            _estadoReservaSvc = estadoReservaSvc;
            _estadoVentaSvc = estadoVentaSvc;
            _metodoPagoSvc = metodoPagoSvc;
            _tipoInspeccionSvc = tipoInspeccionSvc;
        }

        [HttpGet("estados-ejemplar")]
        public async Task<ActionResult<List<EstadoEjemplarRespuestaDto>>> GetEstadosEjemplar()
        {
            return Ok(await _estadoEjemplarSvc.GetAllAsync());
        }

        [HttpGet("estados-ejemplar/{id:int}")]
        public async Task<ActionResult<EstadoEjemplarRespuestaDto>> GetEstadoEjemplarById(int id)
        {
            var result = await _estadoEjemplarSvc.GetByIdAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpGet("condiciones-ejemplar")]
        public async Task<ActionResult<List<CondicionEjemplarRespuestaDto>>> GetCondicionesEjemplar()
        {
            return Ok(await _condicionEjemplarSvc.GetAllAsync());
        }

        [HttpGet("condiciones-ejemplar/{id:int}")]
        public async Task<ActionResult<CondicionEjemplarRespuestaDto>> GetCondicionEjemplarById(int id)
        {
            var result = await _condicionEjemplarSvc.GetByIdAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpGet("estados-prestamo")]
        public async Task<ActionResult<List<EstadoPrestamoRespuestaDto>>> GetEstadosPrestamo()
        {
            return Ok(await _estadoPrestamoSvc.GetAllAsync());
        }

        [HttpGet("estados-prestamo/{id:int}")]
        public async Task<ActionResult<EstadoPrestamoRespuestaDto>> GetEstadoPrestamoById(int id)
        {
            var result = await _estadoPrestamoSvc.GetByIdAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpGet("estados-reserva")]
        public async Task<ActionResult<List<EstadoReservaRespuestaDto>>> GetEstadosReserva()
        {
            return Ok(await _estadoReservaSvc.GetAllAsync());
        }

        [HttpGet("estados-reserva/{id:int}")]
        public async Task<ActionResult<EstadoReservaRespuestaDto>> GetEstadoReservaById(int id)
        {
            var result = await _estadoReservaSvc.GetByIdAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpGet("estados-venta")]
        public async Task<ActionResult<List<EstadoVentaRespuestaDto>>> GetEstadosVenta()
        {
            return Ok(await _estadoVentaSvc.GetAllAsync());
        }

        [HttpGet("estados-venta/{id:int}")]
        public async Task<ActionResult<EstadoVentaRespuestaDto>> GetEstadoVentaById(int id)
        {
            var result = await _estadoVentaSvc.GetByIdAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpGet("metodos-pago")]
        public async Task<ActionResult<List<MetodoPagoRespuestaDto>>> GetMetodosPago()
        {
            return Ok(await _metodoPagoSvc.GetAllAsync());
        }

        [HttpGet("metodos-pago/{id:int}")]
        public async Task<ActionResult<MetodoPagoRespuestaDto>> GetMetodoPagoById(int id)
        {
            var result = await _metodoPagoSvc.GetByIdAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpGet("tipos-inspeccion")]
        public async Task<ActionResult<List<TipoInspeccionRespuestaDto>>> GetTiposInspeccion()
        {
            return Ok(await _tipoInspeccionSvc.GetAllAsync());
        }

        [HttpGet("tipos-inspeccion/{id:int}")]
        public async Task<ActionResult<TipoInspeccionRespuestaDto>> GetTipoInspeccionById(int id)
        {
            var result = await _tipoInspeccionSvc.GetByIdAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }
    }
}
