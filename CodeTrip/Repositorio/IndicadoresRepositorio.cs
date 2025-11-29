using CodeTrip.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CodeTrip.Repositorio
{
    public class IndicadoresRepositorio
    {
        private readonly string _conexaoMySQL;

        public IndicadoresRepositorio(IConfiguration configuration)
        {
            _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");
        }

        // ================= VIAGENS NO MÊS =================
        public List<ViagemMes> ObterViagensPorMes()
        {
            var lista = new List<ViagemMes>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                string query = @"
                    SELECT 
                        DATE_FORMAT(Data_Inicio, '%Y-%m') as Mes,
                        COUNT(*) as Quantidade
                    FROM Pedido 
                    WHERE Ativo = 1 
                    AND Data_Inicio >= DATE_SUB(NOW(), INTERVAL 12 MONTH)
                    GROUP BY DATE_FORMAT(Data_Inicio, '%Y-%m')
                    ORDER BY Mes";

                MySqlCommand cmd = new MySqlCommand(query, conexao);
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new ViagemMes
                    {
                        Mes = dr.GetString("Mes"),
                        Quantidade = dr.GetInt32("Quantidade")
                    });
                }
            }

            return lista;
        }

        // ================= VIAGENS POR DIA (NOVO) =================
        public List<ViagemDia> ObterViagensPorDia(int dias = 30)
        {
            var lista = new List<ViagemDia>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                string query = @"
                    SELECT 
                        DATE(Data_Inicio) as Data,
                        COUNT(*) as Quantidade
                    FROM Pedido 
                    WHERE Ativo = 1 
                    AND Data_Inicio >= DATE_SUB(NOW(), INTERVAL @dias DAY)
                    GROUP BY DATE(Data_Inicio)
                    ORDER BY Data DESC
                    LIMIT @dias";

                MySqlCommand cmd = new MySqlCommand(query, conexao);
                cmd.Parameters.AddWithValue("@dias", dias);
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new ViagemDia
                    {
                        Data = dr.GetDateTime("Data").ToString("yyyy-MM-dd"),
                        Quantidade = dr.GetInt32("Quantidade")
                    });
                }
            }

            return lista;
        }

        // ================= DESTINOS MAIS VENDIDOS =================
        public List<DestinoVendas> ObterDestinosMaisVendidos(int limite = 10)
        {
            var lista = new List<DestinoVendas>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                string query = @"
                    SELECT 
                        CONCAT(d.Cidade_Nome, ' - ', d.UF_Estado) as Destino,
                        COUNT(*) as Quantidade
                    FROM Pedido p
                    JOIN Destino_Viagem d ON p.Id_Destino = d.Id_Destino
                    WHERE p.Ativo = 1
                    GROUP BY p.Id_Destino, d.Cidade_Nome, d.UF_Estado
                    ORDER BY Quantidade DESC
                    LIMIT @limite";

                MySqlCommand cmd = new MySqlCommand(query, conexao);
                cmd.Parameters.AddWithValue("@limite", limite);
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new DestinoVendas
                    {
                        Destino = dr.GetString("Destino"),
                        Quantidade = dr.GetInt32("Quantidade")
                    });
                }
            }

            return lista;
        }

        // ================= PASSEIOS MAIS VENDIDOS =================
        public List<PasseioVendas> ObterPasseiosMaisVendidos(int limite = 10)
        {
            var lista = new List<PasseioVendas>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                string query = @"
                    SELECT 
                        ps.Nome_Passeio as Passeio,
                        COUNT(*) as Quantidade
                    FROM Pedido p
                    JOIN Passeio ps ON p.Id_Passeio = ps.Id_Passeio
                    WHERE p.Ativo = 1 AND ps.Ativo = 1
                    GROUP BY p.Id_Passeio, ps.Nome_Passeio
                    ORDER BY Quantidade DESC
                    LIMIT @limite";

                MySqlCommand cmd = new MySqlCommand(query, conexao);
                cmd.Parameters.AddWithValue("@limite", limite);
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new PasseioVendas
                    {
                        Passeio = dr.GetString("Passeio"),
                        Quantidade = dr.GetInt32("Quantidade")
                    });
                }
            }

            return lista;
        }

        // ================= RECEITA TOTAL =================
        public decimal ObterReceitaTotal()
        {
            decimal receita = 0;

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                string query = @"
                    SELECT 
                        COALESCE(SUM(d.Valor_Destino + 
                                th.Valor_Hospedagem + 
                                tp.Valor_Pensao + 
                                COALESCE(ps.Valor_Passeio, 0)), 0) as Receita
                    FROM Pedido p
                    JOIN Destino_Viagem d ON p.Id_Destino = d.Id_Destino
                    JOIN Hospedagem h ON p.Id_Hospedagem = h.Id_Hospedagem
                    JOIN Tipo_Hospedagem th ON h.Id_Tipo_Hospedagem = th.Id_Tipo_Hospedagem
                    JOIN Tipo_Pensao tp ON h.Id_Pensao = tp.Id_Pensao
                    LEFT JOIN Passeio ps ON p.Id_Passeio = ps.Id_Passeio
                    WHERE p.Ativo = 1";

                MySqlCommand cmd = new MySqlCommand(query, conexao);
                var result = cmd.ExecuteScalar();

                if (result != DBNull.Value && result != null)
                {
                    receita = Convert.ToDecimal(result);
                }
            }

            return receita;
        }

        // ================= CLIENTES ATIVOS =================
        public int ObterClientesAtivos()
        {
            int count = 0;

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                string query = "SELECT COUNT(*) FROM Cliente WHERE Ativo = 1";
                MySqlCommand cmd = new MySqlCommand(query, conexao);
                count = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return count;
        }

        // ================= CLIENTES INATIVOS (NOVO) =================
        public int ObterClientesInativos()
        {
            int count = 0;

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                string query = "SELECT COUNT(*) FROM Cliente WHERE Ativo = 0";
                MySqlCommand cmd = new MySqlCommand(query, conexao);
                count = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return count;
        }

        // ================= OBTER TODOS OS INDICADORES =================
        public Indicadores ObterTodosIndicadores()
        {
            return new Indicadores
            {
                ViagensPorMes = ObterViagensPorMes(),
                ViagensPorDia = ObterViagensPorDia(30),
                DestinosMaisVendidos = ObterDestinosMaisVendidos(5),
                PasseiosMaisVendidos = ObterPasseiosMaisVendidos(5),
                ReceitaTotal = ObterReceitaTotal(),
                ClientesAtivos = ObterClientesAtivos(),
                ClientesInativos = ObterClientesInativos()
            };
        }

        // ================= EXPORTAR DADOS COMPLETOS (NOVO) =================
        public RelatorioExportacao ObterDadosParaExportacao()
        {
            return new RelatorioExportacao
            {
                ViagensMensais = ObterViagensPorMes(),
                ViagensDiarias = ObterViagensPorDia(30),
                TopDestinos = ObterDestinosMaisVendidos(10),
                TopPasseios = ObterPasseiosMaisVendidos(10),
                ReceitaTotal = ObterReceitaTotal(),
                TotalClientesAtivos = ObterClientesAtivos(),
                TotalClientesInativos = ObterClientesInativos(),
                DataGeracao = DateTime.Now
            };
        }
    }
}