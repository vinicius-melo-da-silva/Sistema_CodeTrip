namespace CodeTrip.Models
{
    public class Indicadores
    {
        public List<ViagemMes> ViagensPorMes { get; set; }
        public List<ViagemDia> ViagensPorDia { get; set; }
        public List<DestinoVendas> DestinosMaisVendidos { get; set; }
        public List<PasseioVendas> PasseiosMaisVendidos { get; set; }
        public decimal ReceitaTotal { get; set; }
        public int ClientesAtivos { get; set; }
        public int ClientesInativos { get; set; }
    }

    public class ViagemMes
    {
        public string Mes { get; set; }
        public int Quantidade { get; set; }
    }

    public class ViagemDia
    {
        public string Data { get; set; }
        public int Quantidade { get; set; }
    }

    public class DestinoVendas
    {
        public string Destino { get; set; }
        public int Quantidade { get; set; }
    }

    public class PasseioVendas
    {
        public string Passeio { get; set; }
        public int Quantidade { get; set; }
    }
    public class RelatorioExportacao
    {
        public List<ViagemMes> ViagensMensais { get; set; }
        public List<ViagemDia> ViagensDiarias { get; set; }
        public List<DestinoVendas> TopDestinos { get; set; }
        public List<PasseioVendas> TopPasseios { get; set; }
        public decimal ReceitaTotal { get; set; }
        public int TotalClientesAtivos { get; set; }
        public int TotalClientesInativos { get; set; }
        public DateTime DataGeracao { get; set; }
    }
}