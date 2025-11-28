using System;

namespace CodeTrip.Models
{
    public class DestinoViagem
    {
        public int? Id_Destino { get; set; }
        public string Desc_Destino { get; set; }
        public string Cidade_Nome { get; set; }
        public string UF_Estado { get; set; }
        public decimal Valor_Destino { get; set; }
    }
}