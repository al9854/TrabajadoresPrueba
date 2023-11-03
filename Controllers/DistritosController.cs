using Microsoft.AspNetCore.Mvc;
using MantenimientoTrabajadores.Dominio.Entidades;
using MantenimientoTrabajadores.Dominio.Interfaces;
using MantenimientoTrabajadores.Model.Request;
using MantenimientoTrabajadores.Model.Response;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace MantenimientoTrabajadores.Controllers
{
    [ApiController]
    [Route("api/Distritos")]
    public class DistritoController : Controller
    {
        private readonly IDistritoRepository _DistritoRepository;
        private readonly IMapper _mapper;

        public DistritoController(
            IDistritoRepository DistritoRepository,
            IMapper mapper)
        {
            _DistritoRepository = DistritoRepository;
            _mapper = mapper;
        }

        //Endpoint Listar Distritos
        [HttpGet("Listar", Name = "ListarDistritos")]
        public async Task<IActionResult> Listar()
        {
            var Distritos = await _DistritoRepository.ListarDistritos();
            var DistritosResponse = _mapper.Map<IEnumerable<DistritoResponse>>(Distritos);
            return Ok(DistritosResponse);
        }

        //Endpoint Buscar Distritos por ID
        [HttpGet("Buscar/{id}", Name = "BuscarDistritoPorId")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var Distrito = await _DistritoRepository.ObtenerPorId(id);
            if (Distrito == null)
            {
                return NotFound($"El Distrito con el ID {id} no se encontró.");
            }
            var DistritoResponse = _mapper.Map<DistritoResponse>(Distrito);
            return Ok(DistritoResponse);
        }

        //Endpoint Guardar Distritos
        [HttpPost("Guardar", Name = "GuardarDistrito")]
        public async Task<IActionResult> Guardar(DistritoRequest nuevaDistrito)
        {
            var Distrito = _mapper.Map<Distrito>(nuevaDistrito);

            await _DistritoRepository.Insertar(Distrito);

            return Ok("Distrito registrado");
        }

        //Endpoint Actualizar Distritos
        [HttpPut("Actualizar/{id}", Name = "ActualizarDistrito")]
        public async Task<IActionResult> Actualizar(int id, DistritoRequest Distrito)
        {
            var DistritoEnBD = await _DistritoRepository.ObtenerPorId(id);
            if (DistritoEnBD == null)
            {
                return NotFound("No se encontró el Distrito para actualizar.");
            }

            _mapper.Map(Distrito, DistritoEnBD);

            try
            {
                await _DistritoRepository.Actualizar(DistritoEnBD);
                return Ok("Los datos se actualizaron con éxito");
            }
            catch (Exception)
            {
                return NotFound("Error inesperado.");
            }
        }

        [HttpDelete("Eliminar/{id}", Name = "EliminarDistrito")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var Distrito = await _DistritoRepository.ObtenerPorId(id);
            if (Distrito == null)
            {
                return NotFound("No se encontró el Distrito a eliminar.");
            }

            try
            {
                await _DistritoRepository.Eliminar(id);
                return Ok("Distrito eliminado con éxito.");
            }
            catch (DbUpdateException ex)
            {
                // Mensaje personalizado para restricción de clave foránea
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
                {
                    return BadRequest("No se puede eliminar el Distrito, hay trabajadores asociados a este Distrito.");
                }

                // Capturar y manejar otros tipos de excepciones si es necesario
                return StatusCode(500, "Ocurrió un error al eliminar el Distrito.");
            }
        }

    }
}
