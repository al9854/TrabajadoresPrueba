using MantenimientoTrabajadores.Dominio.Entidades;
using MantenimientoTrabajadores.Model.Request;

namespace MantenimientoTrabajadores.Dominio.Interfaces
{
    public interface ITrabajadorRepository
    {
        Task<IEnumerable<Trabajadores>> ListarTrabajadores();
        Task<Trabajadores> ObtenerPorId(int id);
        Task Insertar(Trabajadores trabajador);
        Task Actualizar(Trabajadores trabajador);
        Task Eliminar(int id);
        Task<bool> Existe(String documento);
        Task<IEnumerable<Trabajadores>> ListarConSP();

    }
}
