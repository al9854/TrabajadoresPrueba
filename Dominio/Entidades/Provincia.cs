using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MantenimientoTrabajadores.Dominio.Entidades
{
    public class Provincia
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? IdDepartamento { get; set; }
        public string? NombreProvincia { get; set; }

    }
}
