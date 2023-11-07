
using MantenimientoTrabajadores.Dominio.Entidades;
using MantenimientoTrabajadores.Dominio.Interfaces;
using MantenimientoTrabajadores.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MantenimientoDepartamentoes.Infraestructura.Repositorio
{
    public class DepartamentoRepository : IDepartamentoRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartamentoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Departamento>> ListarDepartamentos()
        {
            return await _context.Departamento.ToListAsync();
        }

        public async Task<Departamento> ObtenerPorId(int id)
        {
            return await _context.Departamento.FindAsync(id);
        }

        public async Task Insertar(Departamento Departamento)
        {
            _context.Departamento.Add(Departamento);
            await _context.SaveChangesAsync();
        }

        public async Task Actualizar(Departamento Departamento)
        {
            _context.Entry(Departamento).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        public async Task Eliminar(int id)
        {
            var Departamento = await _context.Departamento.FindAsync(id);
            _context.Departamento.Remove(Departamento);
            await _context.SaveChangesAsync();
        }

    }
}
