using System;

namespace CodeTrip.Models
{
    public class Pedido
    {
        public int? Id_Pedido { get; set; }
        public int Id_Usuario { get; set; }
        public string CPF_Cli { get; set; }
        public int Id_Origem { get; set; }
        public int Id_Destino { get; set; }
        public DateOnly Data_Inicio { get; set; }
        public DateOnly Data_Fim { get; set; }
        public int Id_Transp { get; set; }
        public int Id_End_Transporte { get; set; }
        public int Id_Hospedagem { get; set; }
        public int Id_Pagamento { get; set; }
        public int Id_Passeio { get; set; }
        public bool Ativo { get; set; } = true;

        // ================== Campos para exibição amigável ==================
        public string Nome_Usuario { get; set; }
        public string Nome_Cliente { get; set; }
        public string Desc_Origem { get; set; }
        public string Desc_Destino { get; set; }
        public string Desc_Transp { get; set; }
        public string Desc_End_Transporte { get; set; }
        public string Desc_Hospedagem { get; set; }
        public string Desc_Pagamento { get; set; }
        public string Desc_Passeio { get; set; }
    }
}
