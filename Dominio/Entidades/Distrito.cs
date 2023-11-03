using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MantenimientoTrabajadores.Dominio.Entidades
{
    public class Distrito
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? IdProvincia { get; set; }
        public string? NombreDistrito { get; set; }
    }
}
