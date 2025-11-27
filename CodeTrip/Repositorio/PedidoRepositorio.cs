using CodeTrip.Models;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using MySqlX.XDevAPI;
using System.Collections.Generic;
using System.Data;
using static Mysqlx.Expect.Open.Types.Condition.Types;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                MySqlCommand cmd = new MySqlCommand("INSERT INTO Pedido(Id_Usuario, CPF_Cli, Id_Origem, Id_Destino, Data_Inicio, Data_Fim, Id_Transp, Id_End_Transporte, Id_Hospedagem, Id_Pagamento, Id_Passeio, Ativo) VALUES(@Id_Usuario, @CPF_Cli, @Id_Origem, @Id_Destino, @Data_Inicio, @Data_Fim, @Id_Transp, @Id_End_Transporte, @Id_Hospedagem, @Id_Pagamento, @Id_Passeio, @Ativo)", conexao);

                cmd.Parameters.Add("@Id_Usuario", MySqlDbType.Int32).Value = pedido.Id_Usuario;
                cmd.Parameters.Add("@Id_Origem", MySqlDbType.Int32).Value = pedido.Id_Origem;
                cmd.Parameters.Add("@Id_Destino", MySqlDbType.Int32).Value = pedido.Id_Destino;
                cmd.Parameters.Add("@Id_Transp", MySqlDbType.Int32).Value = pedido.Id_Transp;
                cmd.Parameters.Add("@Id_End_Transporte", MySqlDbType.Int32).Value = pedido.Id_End_Transporte;
                cmd.Parameters.Add("@Id_Hospedagem", MySqlDbType.Int32).Value = pedido.Id_Hospedagem;
                cmd.Parameters.Add("@Id_Pagamento", MySqlDbType.Int32).Value = pedido.Id_Pagamento;
                cmd.Parameters.Add("@Id_Passeio", MySqlDbType.Int32).Value = pedido.Id_Passeio;
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
                    MySqlCommand cmd = new MySqlCommand("UPDATE Pedido SET Id_Usuario = @Id_Usuario, CPF_Cli = @CPF_Cli, Id_Origem = @Id_Origem, Id_Destino = @Id_Destino, Data_Inicio = @Data_Inicio,Data_Fim = @Data_Fim,Id_Transp = @Id_Transp,Id_End_Transporte = @Id_End_Transporte,Id_Hospedagem = @Id_Hospedagem,Id_Pagamento = @Id_Pagamento, Id_Passeio = @Id_Passeio, Ativo = @Ativo WHERE Id_Pedido = @Id_Pedido", conexao);
                    cmd.Parameters.Add("@Id_Pedido", MySqlDbType.Int32).Value = pedido.Id_Pedido;
                    cmd.Parameters.Add("@Id_Usuario", MySqlDbType.Int32).Value = pedido.Id_Usuario;
                    cmd.Parameters.Add("@CPF_Cli", MySqlDbType.VarChar).Value = pedido.CPF_Cli;
                    cmd.Parameters.Add("@Id_Origem", MySqlDbType.Int32).Value = pedido.Id_Origem;
                    cmd.Parameters.Add("@Id_Destino", MySqlDbType.Int32).Value = pedido.Id_Destino;
                    cmd.Parameters.Add("@Data_Inicio", MySqlDbType.Date).Value = pedido.Data_Inicio.ToDateTime(TimeOnly.MinValue);
                    cmd.Parameters.Add("@Data_Fim", MySqlDbType.Date).Value = pedido.Data_Fim.ToDateTime(TimeOnly.MinValue);
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
                Console.WriteLine($"Erro ao atualizar cliente: {ex.Message}");
                return false;
            }
        }

        public IEnumerable<Pedido> TodosPedidos()
        {
            List<Pedido> PedidoLista = new List<Pedido>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * from Pedido", conexao);
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
                                    Id_Pagamento = Convert.ToInt32(dr["Id_Pagamento"])
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
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
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

    }
}
