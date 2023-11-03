using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MantenimientoTrabajadores.Dominio.Entidades
{
    public class Trabajadores
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? TipoDocumento { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? Nombres { get; set; }
        public char? Sexo { get; set; }
        public int? IdDepartamento { get; set; }
        public int?  IdProvincia { get; set; }
        public int? IdDistrito { get; set; }


    }
}
