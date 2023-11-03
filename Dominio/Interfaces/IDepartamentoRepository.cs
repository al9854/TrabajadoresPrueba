using MantenimientoTrabajadores.Dominio.Entidades;

namespace MantenimientoTrabajadores.Dominio.Interfaces
{

    public interface IDepartamentoRepository
    {
        Task<IEnumerable<Departamento>> ListarDepartamentos();
        Task<Departamento> ObtenerPorId(int id);
        Task Insertar(Departamento Departamento);
        Task Actualizar(Departamento Departamento);
        Task Eliminar(int id);
        Task<bool> Existe(string nombreDepartamento);
    }
}
