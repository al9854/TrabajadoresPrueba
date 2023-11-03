using MantenimientoTrabajadores.Dominio.Entidades;

namespace MantenimientoTrabajadores.Dominio.Interfaces
{

    public interface IDistritoRepository
    {
        Task<IEnumerable<Distrito>> ListarDistritos();
        Task<Distrito> ObtenerPorId(int id);
        Task Insertar(Distrito Distrito);
        Task Actualizar(Distrito Distrito);
        Task Eliminar(int id);
        Task<bool> Existe(string nombreDistrito);
    }
}
