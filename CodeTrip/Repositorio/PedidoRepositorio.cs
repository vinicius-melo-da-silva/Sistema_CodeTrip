using CodeTrip.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace CodeTrip.Repositorio
{
    public class PedidoRepositorio
    {
        private readonly string _conexaoMySQL;

        public PedidoRepositorio(IConfiguration configuration)
        {
            _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");
        }

        // ================= LISTAR PEDIDOS =================
        public IEnumerable<Pedido> TodosPedidos()
        {
            List<Pedido> lista = new List<Pedido>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                string query = @"
                    SELECT 
                        p.Id_Pedido,
                        u.Id_Usuario, u.Nome_Usuario,
                        c.CPF_Cli, c.Nome_Cli,
                        o.Id_Origem, CONCAT(o.Cidade_Nome, ' - ', o.UF_Estado) AS OrigemDesc,
                        d.Id_Destino, CONCAT(d.Cidade_Nome, ' - ', d.UF_Estado) AS DestinoDesc,
                        p.Data_Inicio, p.Data_Fim,
                        t.Id_Transp, t.Tipo_Transp,
                        e.Id_End_Transporte, CONCAT(e.Logradouro_End_Transporte, ', ', e.Numero_End_Transporte, ' - ', e.Cidade_Nome, '/', e.UF_Estado) AS EndTranspDesc,
                        h.Id_Hospedagem, h.Nome_Hospedagem,
                        pg.Id_Pagamento, pg.Desc_Pagamento,
                        ps.Id_Passeio, ps.Nome_Passeio,
                        p.Ativo
                    FROM Pedido p
                    JOIN Usuario u ON u.Id_Usuario = p.Id_Usuario
                    JOIN Cliente c ON c.CPF_Cli = p.CPF_Cli
                    JOIN Origem_Viagem o ON o.Id_Origem = p.Id_Origem
                    JOIN Destino_Viagem d ON d.Id_Destino = p.Id_Destino
                    JOIN Transporte t ON t.Id_Transp = p.Id_Transp
                    JOIN End_Transporte e ON e.Id_End_Transporte = p.Id_End_Transporte
                    JOIN Hospedagem h ON h.Id_Hospedagem = p.Id_Hospedagem
                    JOIN Pagamento pg ON pg.Id_Pagamento = p.Id_Pagamento
                    LEFT JOIN Passeio ps ON ps.Id_Passeio = p.Id_Passeio";

                MySqlCommand cmd = new MySqlCommand(query, conexao);
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Pedido
                    {
                        Id_Pedido = dr.GetInt32("Id_Pedido"),

                        Id_Usuario = dr.GetInt32("Id_Usuario"),
                        Nome_Usuario = dr.GetString("Nome_Usuario"),

                        CPF_Cli = dr.GetString("CPF_Cli"),
                        Nome_Cliente = dr.GetString("Nome_Cli"),

                        Id_Origem = dr.GetInt32("Id_Origem"),
                        Desc_Origem = dr.GetString("OrigemDesc"),

                        Id_Destino = dr.GetInt32("Id_Destino"),
                        Desc_Destino = dr.GetString("DestinoDesc"),

                        Data_Inicio = DateOnly.FromDateTime(dr.GetDateTime("Data_Inicio")),
                        Data_Fim = DateOnly.FromDateTime(dr.GetDateTime("Data_Fim")),

                        Id_Transp = dr.GetInt32("Id_Transp"),
                        Desc_Transp = dr.GetString("Tipo_Transp"),

                        Id_End_Transporte = dr.GetInt32("Id_End_Transporte"),
                        Desc_End_Transporte = dr.GetString("EndTranspDesc"),

                        Id_Hospedagem = dr.GetInt32("Id_Hospedagem"),
                        Desc_Hospedagem = dr.GetString("Nome_Hospedagem"),

                        Id_Pagamento = dr.GetInt32("Id_Pagamento"),
                        Desc_Pagamento = dr.GetString("Desc_Pagamento"),

                        Id_Passeio = dr.IsDBNull(dr.GetOrdinal("Id_Passeio")) ? 0 : dr.GetInt32("Id_Passeio"),
                        Desc_Passeio = dr.IsDBNull(dr.GetOrdinal("Nome_Passeio")) ? "Nenhum" : dr.GetString("Nome_Passeio"),

                        Ativo = dr.GetBoolean("Ativo")
                    });
                }
            }

            return lista;
        }

        // ================================= CRUD =================================

        public void Cadastrar(Pedido pedido)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand(@"
                    INSERT INTO Pedido
                    (Id_Usuario, CPF_Cli, Id_Origem, Id_Destino, Data_Inicio, Data_Fim, Id_Transp, Id_End_Transporte, Id_Hospedagem, Id_Pagamento, Id_Passeio, Ativo)
                    VALUES
                    (@Id_Usuario, @CPF_Cli, @Id_Origem, @Id_Destino, @Data_Inicio, @Data_Fim, @Id_Transp, @Id_End_Transporte, @Id_Hospedagem, @Id_Pagamento, @Id_Passeio, @Ativo)",
                    conexao);

                cmd.Parameters.AddWithValue("@Id_Usuario", pedido.Id_Usuario);
                cmd.Parameters.AddWithValue("@CPF_Cli", pedido.CPF_Cli);
                cmd.Parameters.AddWithValue("@Id_Origem", pedido.Id_Origem);
                cmd.Parameters.AddWithValue("@Id_Destino", pedido.Id_Destino);
                cmd.Parameters.AddWithValue("@Data_Inicio", pedido.Data_Inicio);
                cmd.Parameters.AddWithValue("@Data_Fim", pedido.Data_Fim);
                cmd.Parameters.AddWithValue("@Id_Transp", pedido.Id_Transp);
                cmd.Parameters.AddWithValue("@Id_End_Transporte", pedido.Id_End_Transporte);
                cmd.Parameters.AddWithValue("@Id_Hospedagem", pedido.Id_Hospedagem);
                cmd.Parameters.AddWithValue("@Id_Pagamento", pedido.Id_Pagamento);
                cmd.Parameters.AddWithValue("@Id_Passeio", pedido.Id_Passeio);
                cmd.Parameters.AddWithValue("@Ativo", pedido.Ativo);

                cmd.ExecuteNonQuery();
            }
        }

        public bool Atualizar(Pedido pedido)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand(@"
                    UPDATE Pedido SET 
                        Id_Usuario=@Id_Usuario,
                        CPF_Cli=@CPF_Cli,
                        Id_Origem=@Id_Origem,
                        Id_Destino=@Id_Destino,
                        Data_Inicio=@Data_Inicio,
                        Data_Fim=@Data_Fim,
                        Id_Transp=@Id_Transp,
                        Id_End_Transporte=@Id_End_Transporte,
                        Id_Hospedagem=@Id_Hospedagem,
                        Id_Pagamento=@Id_Pagamento,
                        Id_Passeio=@Id_Passeio,
                        Ativo=@Ativo
                    WHERE Id_Pedido=@Id_Pedido",
                    conexao);

                cmd.Parameters.AddWithValue("@Id_Pedido", pedido.Id_Pedido);
                cmd.Parameters.AddWithValue("@Id_Usuario", pedido.Id_Usuario);
                cmd.Parameters.AddWithValue("@CPF_Cli", pedido.CPF_Cli);
                cmd.Parameters.AddWithValue("@Id_Origem", pedido.Id_Origem);
                cmd.Parameters.AddWithValue("@Id_Destino", pedido.Id_Destino);
                cmd.Parameters.AddWithValue("@Data_Inicio", pedido.Data_Inicio);
                cmd.Parameters.AddWithValue("@Data_Fim", pedido.Data_Fim);
                cmd.Parameters.AddWithValue("@Id_Transp", pedido.Id_Transp);
                cmd.Parameters.AddWithValue("@Id_End_Transporte", pedido.Id_End_Transporte);
                cmd.Parameters.AddWithValue("@Id_Hospedagem", pedido.Id_Hospedagem);
                cmd.Parameters.AddWithValue("@Id_Pagamento", pedido.Id_Pagamento);
                cmd.Parameters.AddWithValue("@Id_Passeio", pedido.Id_Passeio);
                cmd.Parameters.AddWithValue("@Ativo", pedido.Ativo);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public Pedido ObterPedido(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Pedido WHERE Id_Pedido=@id", conexao);
                cmd.Parameters.AddWithValue("@id", id);
                var dr = cmd.ExecuteReader();

                Pedido pedido = new Pedido();

                if (dr.Read())
                {
                    pedido.Id_Pedido = dr.GetInt32("Id_Pedido");
                    pedido.Id_Usuario = dr.GetInt32("Id_Usuario");
                    pedido.CPF_Cli = dr.GetString("CPF_Cli");
                    pedido.Id_Origem = dr.GetInt32("Id_Origem");
                    pedido.Id_Destino = dr.GetInt32("Id_Destino");
                    pedido.Data_Inicio = DateOnly.FromDateTime(dr.GetDateTime("Data_Inicio"));
                    pedido.Data_Fim = DateOnly.FromDateTime(dr.GetDateTime("Data_Fim"));
                    pedido.Id_Transp = dr.GetInt32("Id_Transp");
                    pedido.Id_End_Transporte = dr.GetInt32("Id_End_Transporte");
                    pedido.Id_Hospedagem = dr.GetInt32("Id_Hospedagem");
                    pedido.Id_Pagamento = dr.GetInt32("Id_Pagamento");
                }

                return pedido;
            }
        }

        public void Excluir(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM Pedido WHERE Id_Pedido=@id", conexao);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
