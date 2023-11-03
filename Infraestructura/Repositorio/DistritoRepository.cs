using MantenimientoTrabajadores.Dominio.Entidades;
using MantenimientoTrabajadores.Dominio.Interfaces;
using MantenimientoTrabajadores.Model;
using Microsoft.EntityFrameworkCore;

namespace MantenimientoDistritoes.Infraestructura.Repositorio
{
    public class DistritoRepository : IDistritoRepository
    {
        private readonly ApplicationDbContext _context;

        public DistritoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Distrito>> ListarDistritos()
        {
            return await _context.Distrito.ToListAsync();
        }

        public async Task<Distrito> ObtenerPorId(int id)
        {
            return await _context.Distrito.FindAsync(id);
        }

        public async Task Insertar(Distrito Distrito)
        {
            _context.Distrito.Add(Distrito);
            await _context.SaveChangesAsync();
        }

        public async Task Actualizar(Distrito Distrito)
        {
            _context.Entry(Distrito).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        public async Task Eliminar(int id)
        {
            var Distrito = await _context.Distrito.FindAsync(id);
            _context.Distrito.Remove(Distrito);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Existe(string nombreDistrito)
        {
            return await _context.Distrito.AnyAsync(p => p.NombreDistrito == nombreDistrito);
        }
    }
}
