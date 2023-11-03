using MantenimientoTrabajadores.Dominio.Entidades;
using MantenimientoTrabajadores.Dominio.Interfaces;
using MantenimientoTrabajadores.Model;
using Microsoft.EntityFrameworkCore;

namespace MantenimientoProvinciaes.Infraestructura.Repositorio
{
    public class ProvinciaRepository : IProvinciaRepository
    {
        private readonly ApplicationDbContext _context;

        public ProvinciaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Provincia>> ListarProvincias()
        {
            return await _context.Provincia.ToListAsync();
        }

        public async Task<Provincia> ObtenerPorId(int id)
        {
            return await _context.Provincia.FindAsync(id);
        }

        public async Task Insertar(Provincia provincia)
        {
            _context.Provincia.Add(provincia);
            await _context.SaveChangesAsync();
        }

        public async Task Actualizar(Provincia provincia)
        {
            _context.Entry(provincia).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        public async Task Eliminar(int id)
        {
            var Provincia = await _context.Provincia.FindAsync(id);
            _context.Provincia.Remove(Provincia);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Existe(string nombreProvincia)
        {
            return await _context.Provincia.AnyAsync(p => p.NombreProvincia == nombreProvincia);
        }
    }
}
