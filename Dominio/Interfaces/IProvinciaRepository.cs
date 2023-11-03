using MantenimientoTrabajadores.Dominio.Entidades;

namespace MantenimientoTrabajadores.Dominio.Interfaces
{

    public interface IProvinciaRepository
    {
        Task<IEnumerable<Provincia>> ListarProvincias();
        Task<Provincia> ObtenerPorId(int id);
        Task Insertar(Provincia provincia);
        Task Actualizar(Provincia provincia);
        Task Eliminar(int id);
        Task<bool> Existe(string nombreProvincia);
    }
}
