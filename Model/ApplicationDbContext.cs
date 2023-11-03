using MantenimientoTrabajadores.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace MantenimientoTrabajadores.Model
{
    //Es el contexto de la base de datos
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Trabajadores> Trabajadores { get; set; }
        public DbSet<Provincia> Provincia { get; set; }

        public DbSet<Departamento> Departamento { get; set; }

        public DbSet<Distrito> Distrito { get; set; }
    }

}
