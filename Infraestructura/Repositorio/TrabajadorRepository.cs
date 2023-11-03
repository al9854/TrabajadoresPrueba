using MantenimientoTrabajadores.Dominio.Entidades;
using MantenimientoTrabajadores.Dominio.Interfaces;
using MantenimientoTrabajadores.Model;
using Microsoft.EntityFrameworkCore;

namespace MantenimientoTrabajadores.Infraestructura.Repositorio
{
    public class TrabajadorRepository : ITrabajadorRepository
    {
        private readonly ApplicationDbContext _context;

        public TrabajadorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Trabajadores>> ListarTrabajadores()
        {
            return await _context.Trabajadores.ToListAsync();
        }

        public async Task<Trabajadores> ObtenerPorId(int id)
        {
            return await _context.Trabajadores.FindAsync(id);
        }

        public async Task Insertar(Trabajadores trabajador)
        {
            _context.Trabajadores.Add(trabajador);
            await _context.SaveChangesAsync();
        }

        public async Task Actualizar(Trabajadores trabajador)
        {
            _context.Entry(trabajador).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Eliminar(int id)
        {
            var trabajador = await _context.Trabajadores.FindAsync(id);
            _context.Trabajadores.Remove(trabajador);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Existe(String NumeroDocumento)
        {
            return await _context.Trabajadores.AnyAsync(e => e.NumeroDocumento == NumeroDocumento);
        }

        public async Task<IEnumerable<Trabajadores>> ListarConSP()
        {
            // Lógica para llamar al Stored Procedure ListarTrabajadores
            return await _context.Trabajadores.FromSqlRaw("ListarTrabajadores").ToListAsync();
        }
    }

}
