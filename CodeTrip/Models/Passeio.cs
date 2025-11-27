namespace CodeTrip.Models
{
    public class Passeio
    {
        public int? Id_Passeio { get; set; }
        public string? Nome_Passeio { get; set; }
        public string? Desc_Passeio { get; set; }
        public decimal Valor_Passeio { get; set; }
        public string? Duracao_Passeio { get; set; }
        public string? Cidade_Nome { get; set; }
        public string? UF_Estado { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
