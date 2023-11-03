using Microsoft.AspNetCore.Mvc;
using MantenimientoTrabajadores.Dominio.Entidades;
using MantenimientoTrabajadores.Dominio.Interfaces;
using MantenimientoTrabajadores.Model.Request;
using MantenimientoTrabajadores.Model.Response;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MantenimientoTrabajadores.Controllers
{
    [ApiController]
    [Route("api/Provincias")]
    public class ProvinciaController : Controller
    {
        private readonly IProvinciaRepository _ProvinciaRepository;
        private readonly IMapper _mapper;

        public ProvinciaController(
            IProvinciaRepository ProvinciaRepository,
            IMapper mapper)
        {
            _ProvinciaRepository = ProvinciaRepository;
            _mapper = mapper;
        }

        //Endpoint Listar Provincias
        [HttpGet("Listar", Name = "ListarProvincias")]
        public async Task<IActionResult> Listar()
        {
            var Provincias = await _ProvinciaRepository.ListarProvincias();
            var ProvinciasResponse = _mapper.Map<IEnumerable<ProvinciaResponse>>(Provincias);
            return Ok(ProvinciasResponse);
        }

        //Endpoint Buscar Provincias por ID
        [HttpGet("Buscar/{id}", Name = "BuscarProvinciaPorId")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var Provincia = await _ProvinciaRepository.ObtenerPorId(id);
            if (Provincia == null)
            {
                return NotFound($"La provincia con el ID {id} no se encontró.");
            }
            var ProvinciaResponse = _mapper.Map<ProvinciaResponse>(Provincia);
            return Ok(ProvinciaResponse);
        }

        //Endpoint Guardar Provincias
        [HttpPost("Guardar", Name = "GuardarProvincia")]
        public async Task<IActionResult> Guardar(ProvinciaRequest nuevaProvincia)
        {
            var Provincia = _mapper.Map<Provincia>(nuevaProvincia);

            await _ProvinciaRepository.Insertar(Provincia);

            return Ok("Provincia registrada");
        }

        //Endpoint Actualizar Provincias
        [HttpPut("Actualizar/{id}", Name = "ActualizarProvincia")]
        public async Task<IActionResult> Actualizar(int id, ProvinciaRequest Provincia)
        {
            var ProvinciaEnBD = await _ProvinciaRepository.ObtenerPorId(id);
            if (ProvinciaEnBD == null)
           {
                return NotFound("No se encontró la provincia para actualizar.");
            }

            _mapper.Map(Provincia, ProvinciaEnBD);

            try
            {
                await _ProvinciaRepository.Actualizar(ProvinciaEnBD);
                return Ok("Los datos se actualizaron con éxito");
            }
            catch (Exception)
            {
                return NotFound("Error inesperado.");
            }
        }

        [HttpDelete("Eliminar/{id}", Name = "EliminarProvincia")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var Provincia = await _ProvinciaRepository.ObtenerPorId(id);
            if (Provincia == null)
            {
                return NotFound("No se encontró la Provincia a eliminar.");
            }

            try
            {
                await _ProvinciaRepository.Eliminar(id);
                return Ok("Provincia eliminada con éxito.");
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
            {
                return BadRequest("No se puede eliminar la Provincia, hay Distritos asociados a esta Provincia.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error al eliminar la Provincia.");
            }
        }
    }

    }
