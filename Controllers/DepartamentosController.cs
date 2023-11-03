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
    [Route("api/Departamentos")]
    public class DepartamentoController : Controller
    {
        private readonly IDepartamentoRepository _departamentoRepository;
        private readonly IMapper _mapper;

        public DepartamentoController(
            IDepartamentoRepository departamentoRepository,
            IMapper mapper)
        {
            _departamentoRepository = departamentoRepository;
            _mapper = mapper;
        }

        //Endpoint Listar Departamentos
        [HttpGet("Listar", Name = "ListarDepartamentos")]
        public async Task<IActionResult> Listar()
        {
            var Departamentos = await _departamentoRepository.ListarDepartamentos();
            var DepartamentosResponse = _mapper.Map<IEnumerable<DepartamentoResponse>>(Departamentos);
            return Ok(DepartamentosResponse);
        }

        //Endpoint Buscar Departamentos por ID
        [HttpGet("Buscar/{id}", Name = "BuscarDepartamentoPorId")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var Departamento = await _departamentoRepository.ObtenerPorId(id);
            if (Departamento == null)
            {
                return NotFound($"El Departamento con el ID {id} no se encontró.");
            }
            var DepartamentoResponse = _mapper.Map<DepartamentoResponse>(Departamento);
            return Ok(DepartamentoResponse);
        }

        //Endpoint Guardar Departamentos
        [HttpPost("Guardar", Name = "GuardarDepartamento")]
        public async Task<IActionResult> Guardar(DepartamentoRequest nuevaDepartamento)
        {
            var Departamento = _mapper.Map<Departamento>(nuevaDepartamento);

            await _departamentoRepository.Insertar(Departamento);

            return Ok("Departamento registrado");
        }

        //Endpoint Actualizar Departamentos
        [HttpPut("Actualizar/{id}", Name = "ActualizarDepartamento")]
        public async Task<IActionResult> Actualizar(int id, DepartamentoRequest Departamento)
        {
            var DepartamentoEnBD = await _departamentoRepository.ObtenerPorId(id);
            if (DepartamentoEnBD == null)
            {
                return NotFound("No se encontró el departamento para actualizar.");
            }

            _mapper.Map(Departamento, DepartamentoEnBD);

            try
            {
                await _departamentoRepository.Actualizar(DepartamentoEnBD);
                return Ok("Los datos se actualizaron con éxito");
            }
            catch (Exception)
            {
                return NotFound("Error inesperado.");
            }
        }

        [HttpDelete("Eliminar/{id}", Name = "EliminarDepartamento")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var Departamento = await _departamentoRepository.ObtenerPorId(id);
            if (Departamento == null)
            {
                return NotFound("No se encontró el departamento a eliminar.");
            }

            try
            {
                await _departamentoRepository.Eliminar(id);
                return Ok("Departamento eliminado con éxito.");
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
            {
                return BadRequest("No se puede eliminar el Departamento, hay Provincias asociadas a este Departamento.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error al eliminar el Departamento.");
            }
        }

    }
}
