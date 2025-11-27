namespace CodeTrip.Models
{
    public class Transporte
    {
        public int? Id_Transp { get; set; }
        public string? Tipo_Transp { get; set; }
        public string? UF_Estado { get; set; }
        public bool Ativo { get; set; } = true;
    }
}