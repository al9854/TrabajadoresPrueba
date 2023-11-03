using Microsoft.AspNetCore.Mvc;
using MantenimientoTrabajadores.Dominio.Entidades;
using MantenimientoTrabajadores.Dominio.Interfaces;
using MantenimientoTrabajadores.Model.Request;
using MantenimientoTrabajadores.Model.Response;
using AutoMapper;
using MantenimientoTrabajadores.Infraestructura.Repositorio;

namespace MantenimientoTrabajadores.Controllers
{
    [ApiController]
    [Route("api/trabajadores")]
    public class TrabajadoresController : Controller
    {
        private readonly ITrabajadorRepository _trabajadorRepository;
        private readonly IDepartamentoRepository _departamentoRepository;
        private readonly IProvinciaRepository _provinciaRepository;
        private readonly IDistritoRepository _distritoRepository;
        private readonly IMapper _mapper;

        public TrabajadoresController(
            ITrabajadorRepository trabajadorRepository,
            IDepartamentoRepository departamentoRepository,
            IProvinciaRepository provinciaRepository,
            IDistritoRepository distritoRepository,
            IMapper mapper)
        {
            _trabajadorRepository = trabajadorRepository;
            _departamentoRepository = departamentoRepository;
            _provinciaRepository = provinciaRepository;
            _distritoRepository = distritoRepository;
            _mapper = mapper;
        }


        // Endpoint para GuardarTrabajador
        [HttpPost("Guardar", Name = "GuardarTrabajador")]
        public async Task<IActionResult> Guardar(TrabajadorRequest nuevoTrabajador)
        {
            var existe = await _trabajadorRepository.Existe(nuevoTrabajador.NumeroDocumento);
            if (existe)
            {
                return BadRequest("El trabajador ya existe");
            }

            var trabajador = _mapper.Map<Trabajadores>(nuevoTrabajador);

            await _trabajadorRepository.Insertar(trabajador);
            return Ok("Trabajador Registrado");
        }

        [HttpPut("Actualizar/{id}", Name = "ActualizarTrabajador")]
        public async Task<IActionResult> Actualizar(int id, TrabajadorRequest trabajador)
        {
            var trabajadorEnBD = await _trabajadorRepository.ObtenerPorId(id);

            if (trabajadorEnBD == null)
            {
                return NotFound("No se encontró el trabajador para actualizar.");
            }

            // Verificar si el Departamento, Provincia y Distrito existen

            var provinciaExists = await _provinciaRepository.ObtenerPorId(id);
            if (provinciaExists == null)
            {
                return NotFound("La provincia no está registrada.");
            }
            var departamentoExists = await _departamentoRepository.ObtenerPorId(id);
            if (departamentoExists == null)
            {
                return NotFound("La provincia no está registrada.");
            }
            var distritoExists = await _distritoRepository.ObtenerPorId(id);
            if (distritoExists == null)
            {
                return NotFound("La provincia no está registrada.");
            }

            _mapper.Map(trabajador, trabajadorEnBD);

            try
            {
                await _trabajadorRepository.Actualizar(trabajadorEnBD);
                return Ok("Los datos se actualizaron con éxito");
            }
            catch (Exception)
            {
                return NotFound("Error inesperado");
            }
        }


        // Endpoint para ListarTrabajador
        [HttpGet("Listar", Name = "ListarTrabajador")]
        public async Task<IActionResult> Listar()
        {
            var trabajadores = await _trabajadorRepository.ListarTrabajadores();
            var trabajadoresResponse = _mapper.Map<IEnumerable<TrabajadorResponse>>(trabajadores);
            return Ok(trabajadoresResponse);
        }

        // Endpoint para EliminarTrabajador
        [HttpDelete("Eliminar/{id}", Name = "EliminarTrabajador")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var trabajador = await _trabajadorRepository.ObtenerPorId(id);
            if (trabajador == null)
            {
                return NotFound("No se encontró el trabajador a eliminar.");
            }

            await _trabajadorRepository.Eliminar(id);
            return Ok("Trabajador eliminado con éxito.");
        }

        // Endpoint para BuscarPorIdTrabajador
        [HttpGet("Buscar/{id}", Name = "BuscarTrabajadorPorId")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var trabajador = await _trabajadorRepository.ObtenerPorId(id);
            if (trabajador == null)
            {

                return NotFound($"El trabajador con el ID {id} no se encontró.");
            }
            var trabajadorResponse = _mapper.Map<TrabajadorResponse>(trabajador);
            return Ok(trabajadorResponse);
        }

        // Utilizar Store Procedure para el listado principal de Trabajadores
        [HttpGet("ListarConSP", Name = "ListarTrabajadorConSP")]
        public async Task<IActionResult> ListarConSP()
        {
            var trabajadores = await _trabajadorRepository.ListarConSP();
            var trabajadoresResponse = _mapper.Map<IEnumerable<TrabajadorResponse>>(trabajadores);
            return Ok(trabajadoresResponse);
        }
    }
}
