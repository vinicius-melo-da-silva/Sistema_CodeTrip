using MySql.Data.MySqlClient;
using System.Data;
using CodeTrip.Models;
using static Mysqlx.Expect.Open.Types.Condition.Types;
using MySqlX.XDevAPI;
using Mysqlx.Crud;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;

namespace CodeTrip.Repositorio
{
    public class PedidoRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");

        public void Cadastrar(Pedido pedido)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand(
                    "INSERT INTO Pedido (Id_Usuario, CPF_Cli, Id_Origem, Id_Destino, Data_Inicio, Data_Fim, Id_Transp, Id_End_Transporte, Id_Hospedagem, Id_Pagamento, Id_Passeio, Ativo) " +
                    "VALUES (@Id_Usuario, @CPF_Cli, @Id_Origem, @Id_Destino, @Data_Inicio, @Data_Fim, @Id_Transp, @Id_End_Transporte, @Id_Hospedagem, @Id_Pagamento, @Id_Passeio, @Ativo)",
                    conexao
                );

                cmd.Parameters.Add("@Id_Usuario", MySqlDbType.Int32).Value = pedido.Id_Usuario;
                cmd.Parameters.Add("@CPF_Cli", MySqlDbType.VarChar).Value = pedido.CPF_Cli;
                cmd.Parameters.Add("@Id_Origem", MySqlDbType.Int32).Value = pedido.Id_Origem;
                cmd.Parameters.Add("@Id_Destino", MySqlDbType.Int32).Value = pedido.Id_Destino;
                cmd.Parameters.Add("@Data_Inicio", MySqlDbType.Date).Value = pedido.Data_Inicio;
                cmd.Parameters.Add("@Data_Fim", MySqlDbType.Date).Value = pedido.Data_Fim;
                cmd.Parameters.Add("@Id_Transp", MySqlDbType.Int32).Value = pedido.Id_Transp;
                cmd.Parameters.Add("@Id_End_Transporte", MySqlDbType.Int32).Value = pedido.Id_End_Transporte;
                cmd.Parameters.Add("@Id_Hospedagem", MySqlDbType.Int32).Value = pedido.Id_Hospedagem;
                cmd.Parameters.Add("@Id_Pagamento", MySqlDbType.Int32).Value = pedido.Id_Pagamento;
                cmd.Parameters.Add("@Id_Passeio", MySqlDbType.Int32).Value = pedido.Id_Passeio;
                cmd.Parameters.Add("@Ativo", MySqlDbType.Bit).Value = pedido.Ativo;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public bool Atualizar(Pedido pedido)
        {
            try
            {
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    conexao.Open();
                    MySqlCommand cmd = new MySqlCommand(
                        "UPDATE Pedido SET Id_Usuario=@Id_Usuario, CPF_Cli=@CPF_Cli, Id_Origem=@Id_Origem, Id_Destino=@Id_Destino, " +
                        "Data_Inicio=@Data_Inicio, Data_Fim=@Data_Fim, Id_Transp=@Id_Transp, Id_End_Transporte=@Id_End_Transporte, " +
                        "Id_Hospedagem=@Id_Hospedagem, Id_Pagamento=@Id_Pagamento, Id_Passeio=@Id_Passeio, Ativo=@Ativo " +
                        "WHERE Id_Pedido=@Id_Pedido",
                        conexao
                    );

                    cmd.Parameters.Add("@Id_Pedido", MySqlDbType.Int32).Value = pedido.Id_Pedido;
                    cmd.Parameters.Add("@Id_Usuario", MySqlDbType.Int32).Value = pedido.Id_Usuario;
                    cmd.Parameters.Add("@CPF_Cli", MySqlDbType.VarChar).Value = pedido.CPF_Cli;
                    cmd.Parameters.Add("@Id_Origem", MySqlDbType.Int32).Value = pedido.Id_Origem;
                    cmd.Parameters.Add("@Id_Destino", MySqlDbType.Int32).Value = pedido.Id_Destino;
                    cmd.Parameters.Add("@Data_Inicio", MySqlDbType.Date).Value = pedido.Data_Inicio;
                    cmd.Parameters.Add("@Data_Fim", MySqlDbType.Date).Value = pedido.Data_Fim;
                    cmd.Parameters.Add("@Id_Transp", MySqlDbType.Int32).Value = pedido.Id_Transp;
                    cmd.Parameters.Add("@Id_End_Transporte", MySqlDbType.Int32).Value = pedido.Id_End_Transporte;
                    cmd.Parameters.Add("@Id_Hospedagem", MySqlDbType.Int32).Value = pedido.Id_Hospedagem;
                    cmd.Parameters.Add("@Id_Pagamento", MySqlDbType.Int32).Value = pedido.Id_Pagamento;
                    cmd.Parameters.Add("@Id_Passeio", MySqlDbType.Int32).Value = pedido.Id_Passeio;
                    cmd.Parameters.Add("@Ativo", MySqlDbType.Bit).Value = pedido.Ativo;

                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    return linhasAfetadas > 0;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro ao atualizar pedido: {ex.Message}");
                return false;
            }
        }

        public IEnumerable<Pedido> TodosPedidos()
        {
            List<Pedido> PedidoLista = new List<Pedido>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                // Query modificada com JOINs para trazer os dados relacionados
                string query = @"
            SELECT 
                p.*,
                u.Nome_Usuario,
                c.Nome_Cli,
                ov.Cidade_Nome as Origem_Cidade,
                ov.UF_Estado as Origem_UF,
                dv.Desc_Destino as Destino_Desc,
                dv.Cidade_Nome as Destino_Cidade,
                dv.UF_Estado as Destino_UF,
                t.Tipo_Transp,
                et.Logradouro_End_Transporte,
                h.Nome_Hospedagem,
                pg.Desc_Pagamento,
                ps.Nome_Passeio
            FROM Pedido p
            LEFT JOIN Usuario u ON p.Id_Usuario = u.Id_Usuario
            LEFT JOIN Cliente c ON p.CPF_Cli = c.CPF_Cli
            LEFT JOIN Origem_Viagem ov ON p.Id_Origem = ov.Id_Origem
            LEFT JOIN Destino_Viagem dv ON p.Id_Destino = dv.Id_Destino
            LEFT JOIN Transporte t ON p.Id_Transp = t.Id_Transp
            LEFT JOIN End_Transporte et ON p.Id_End_Transporte = et.Id_End_Transporte
            LEFT JOIN Hospedagem h ON p.Id_Hospedagem = h.Id_Hospedagem
            LEFT JOIN Pagamento pg ON p.Id_Pagamento = pg.Id_Pagamento
            LEFT JOIN Passeio ps ON p.Id_Passeio = ps.Id_Passeio";

                MySqlCommand cmd = new MySqlCommand(query, conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    PedidoLista.Add(
                        new Pedido
                        {
                            Id_Pedido = Convert.ToInt32(dr["Id_Pedido"]),
                            Id_Usuario = Convert.ToInt32(dr["Id_Usuario"]),
                            CPF_Cli = ((string)dr["CPF_Cli"]),
                            Id_Origem = Convert.ToInt32(dr["Id_Origem"]),
                            Id_Destino = Convert.ToInt32(dr["Id_Destino"]),
                            Data_Inicio = DateOnly.FromDateTime(Convert.ToDateTime(dr["Data_Inicio"])),
                            Data_Fim = DateOnly.FromDateTime(Convert.ToDateTime(dr["Data_Fim"])),
                            Id_Transp = Convert.ToInt32(dr["Id_Transp"]),
                            Id_End_Transporte = Convert.ToInt32(dr["Id_End_Transporte"]),
                            Id_Hospedagem = Convert.ToInt32(dr["Id_Hospedagem"]),
                            Id_Pagamento = Convert.ToInt32(dr["Id_Pagamento"]),
                            Id_Passeio = Convert.ToInt32(dr["Id_Passeio"]),
                            Ativo = Convert.ToBoolean(dr["Ativo"]),
                            // Novas propriedades para exibição
                            Nome_Usuario = dr["Nome_Usuario"] != DBNull.Value ? dr["Nome_Usuario"].ToString() : string.Empty,
                            Nome_Cli = dr["Nome_Cli"] != DBNull.Value ? dr["Nome_Cli"].ToString() : string.Empty,
                            Origem_Cidade = dr["Origem_Cidade"] != DBNull.Value ? dr["Origem_Cidade"].ToString() : string.Empty,
                            Destino_Desc = dr["Destino_Desc"] != DBNull.Value ? dr["Destino_Desc"].ToString() : string.Empty,
                            Tipo_Transp = dr["Tipo_Transp"] != DBNull.Value ? dr["Tipo_Transp"].ToString() : string.Empty,
                            Logradouro_End_Transporte = dr["Logradouro_End_Transporte"] != DBNull.Value ? dr["Logradouro_End_Transporte"].ToString() : string.Empty,
                            Nome_Hospedagem = dr["Nome_Hospedagem"] != DBNull.Value ? dr["Nome_Hospedagem"].ToString() : string.Empty,
                            Desc_Pagamento = dr["Desc_Pagamento"] != DBNull.Value ? dr["Desc_Pagamento"].ToString() : string.Empty,
                            Nome_Passeio = dr["Nome_Passeio"] != DBNull.Value ? dr["Nome_Passeio"].ToString() : string.Empty
                        });
                }
                return PedidoLista;
            }
        }

        public Pedido ObterPedido(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Pedido WHERE Id_Pedido=@id", conexao);
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader dr;
                Pedido pedido = new Pedido();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    pedido.Id_Pedido = Convert.ToInt32(dr["Id_Pedido"]);
                    pedido.Id_Usuario = Convert.ToInt32(dr["Id_Usuario"]);
                    pedido.CPF_Cli = ((string)dr["CPF_Cli"]);
                    pedido.Id_Origem = Convert.ToInt32(dr["Id_Origem"]);
                    pedido.Id_Destino = Convert.ToInt32(dr["Id_Destino"]);
                    pedido.Data_Inicio = DateOnly.FromDateTime(Convert.ToDateTime(dr["Data_Inicio"]));
                    pedido.Data_Fim = DateOnly.FromDateTime(Convert.ToDateTime(dr["Data_Fim"]));
                    pedido.Id_Transp = Convert.ToInt32(dr["Id_Transp"]);
                    pedido.Id_End_Transporte = Convert.ToInt32(dr["Id_End_Transporte"]);
                    pedido.Id_Hospedagem = Convert.ToInt32(dr["Id_Hospedagem"]);
                    pedido.Id_Pagamento = Convert.ToInt32(dr["Id_Pagamento"]);
                    pedido.Id_Passeio = Convert.ToInt32(dr["Id_Passeio"]);
                    pedido.Ativo = Convert.ToBoolean(dr["Ativo"]);
                }
                dr.Close();
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
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public List<Usuario> Usuarios()
        {
            var lista = new List<Usuario>();
            using (MySqlConnection conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                string query = "SELECT id_Usuario, nome_Usuario from Usuario";
                using (MySqlCommand comando = new MySqlCommand(query, conexao))
                {
                    using (MySqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Usuario
                            {
                                Id_Usuario = reader.GetInt32("Id_Usuario"),
                                Nome_Usuario = reader.GetString("Nome_Usuario")
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public List<Cliente> Clientes()
        {
            var lista = new List<Cliente>();
            using (MySqlConnection conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                string query = "SELECT Nome_Cli, CPF_Cli from Cliente";
                using (MySqlCommand comando = new MySqlCommand(query, conexao))
                {
                    using (MySqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Cliente
                            {
                                Nome_Cli = reader.GetString("Nome_Cli"),
                                CPF_Cli = reader.GetString("CPF_Cli")
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public List<Transporte> Transportes()
        {
            var lista = new List<Transporte>();
            using (MySqlConnection conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                string query = "SELECT Id_Transp, Tipo_Transp, UF_Estado from Transporte";
                using (MySqlCommand comando = new MySqlCommand(query, conexao))
                {
                    using (MySqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Transporte
                            {
                                Id_Transp = reader.GetInt32("Id_Transp"),
                                Tipo_Transp = reader.GetString("Tipo_Transp"),
                                UF_Estado = reader.GetString("UF_Estado")
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public List<End_Transporte> End_Transportes()
        {
            var lista = new List<End_Transporte>();
            using (MySqlConnection conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                string query = "SELECT Id_End_Transporte, Logradouro_End_Transporte, Numero_End_Transporte, Cidade_Nome, UF_Estado from End_Transporte";
                using (MySqlCommand comando = new MySqlCommand(query, conexao))
                {
                    using (MySqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new End_Transporte
                            {
                                Id_End_Transporte = reader.GetInt32("Id_End_Transporte"),
                                Logradouro_End_Transporte = reader.GetString("Logradouro_End_Transporte"),
                                Numero_End_Transporte = reader.GetString("Numero_End_Transporte"),
                                Cidade_Nome = reader.GetString("Cidade_Nome"),
                                UF_Estado = reader.GetString("UF_Estado")
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public List<Hospedagem> Hospedagens()
        {
            var lista = new List<Hospedagem>();
            using (MySqlConnection conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                string query = "SELECT Id_Hospedagem, Nome_Hospedagem from Hospedagem";
                using (MySqlCommand comando = new MySqlCommand(query, conexao))
                {
                    using (MySqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Hospedagem
                            {
                                Id_Hospedagem = reader.GetInt32("Id_Hospedagem"),
                                Nome_Hospedagem = reader.GetString("Nome_Hospedagem"),
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public List<Pagamento> Pagamentos()
        {
            var lista = new List<Pagamento>();
            using (MySqlConnection conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                string query = "SELECT Id_Pagamento, Desc_Pagamento from Pagamento";
                using (MySqlCommand comando = new MySqlCommand(query, conexao))
                {
                    using (MySqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Pagamento
                            {
                                Id_Pagamento = reader.GetInt32("Id_Pagamento"),
                                Desc_Pagamento = reader.GetString("Desc_Pagamento"),
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public List<OrigemViagem> OrigensViagem()
        {
            var lista = new List<OrigemViagem>();
            using (MySqlConnection conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                string query = "SELECT Id_Origem, Cidade_Nome, UF_Estado from Origem_Viagem";
                using (MySqlCommand comando = new MySqlCommand(query, conexao))
                {
                    using (MySqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new OrigemViagem
                            {
                                Id_Origem = reader.GetInt32("Id_Origem"),
                                Cidade_Nome = reader.GetString("Cidade_Nome"),
                                UF_Estado = reader.GetString("UF_Estado")
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public List<DestinoViagem> DestinosViagem()
        {
            var lista = new List<DestinoViagem>();
            using (MySqlConnection conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                string query = "SELECT Id_Destino, Desc_Destino, Cidade_Nome, UF_Estado from Destino_Viagem";
                using (MySqlCommand comando = new MySqlCommand(query, conexao))
                {
                    using (MySqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new DestinoViagem
                            {
                                Id_Destino = reader.GetInt32("Id_Destino"),
                                Desc_Destino = reader.GetString("Desc_Destino"),
                                Cidade_Nome = reader.GetString("Cidade_Nome"),
                                UF_Estado = reader.GetString("UF_Estado")
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public List<Passeio> Passeios()
        {
            var lista = new List<Passeio>();
            using (MySqlConnection conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                string query = "SELECT Id_Passeio, Nome_Passeio, Cidade_Nome, UF_Estado from Passeio WHERE Ativo = 1";
                using (MySqlCommand comando = new MySqlCommand(query, conexao))
                {
                    using (MySqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Passeio
                            {
                                Id_Passeio = reader.GetInt32("Id_Passeio"),
                                Nome_Passeio = reader.GetString("Nome_Passeio"),
                                Cidade_Nome = reader.GetString("Cidade_Nome"),
                                UF_Estado = reader.GetString("UF_Estado")
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}