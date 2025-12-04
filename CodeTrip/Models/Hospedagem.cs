namespace CodeTrip.Models
{
    public class Hospedagem
    {
        public int Id_Hospedagem { get; set; }
        public string Nome_Hospedagem { get; set; }
        public int Id_Tipo_Hospedagem { get; set; }
        public string Desc_Hospedagem { get; set; } // Nova propriedade
        public int Id_Pensao { get; set; }
        public string Desc_Pensao { get; set; } // Nova propriedade
        public string Logradouro_Endereco_Hospedagem { get; set; }
        public string Numero_Endereco_Hospedagem { get; set; }
        public string Bairro_Endereco_Hospedagem { get; set; }
        public string Complemento_Endereco_Hospedagem { get; set; }
        public string Cidade_Nome { get; set; }
        public string UF_Estado { get; set; }
        public int ativo { get; set; }
    }
}