namespace CodeTrip.Models
{
    public class Cliente
    {
        public int? Id_Cli { get; set; }
        public string? Nome_Cli { get; set; }
        public string? Email_Cli { get; set; }
        public DateOnly? Data_Nasc_Cli { get; set; }
        public string? CPF_Cli { get; set; }
        public string? Telefone_Cli { get; set; }
        public string? Logradouro_Cli { get; set; }
        public string? Numero_Cli { get; set; }
        public string? Bairro_Cli { get; set; }
        public string? Complemento_Cli { get; set; }
        public string? Cidade_Nome { get; set; }
        public string? UF_Estado { get; set; }
        public bool Ativo { get; set; } = true;
    }
}