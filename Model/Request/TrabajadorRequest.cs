using System.ComponentModel.DataAnnotations;

namespace MantenimientoTrabajadores.Model.Request
{
    public class TrabajadorRequest
    {
        public string? TipoDocumento { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? Nombres { get; set; }

        [MaxLength(1)]
        [RegularExpression("^[MFmf]$")] // Asegura que el valor sea 'M' o 'F' (mayúscula o minúscula)
        public string? Sexo { get; set; }

        public int? IdDepartamento { get; set; }
        public int? IdProvincia { get; set; }
        public int? IdDistrito { get; set; }
    }
}
