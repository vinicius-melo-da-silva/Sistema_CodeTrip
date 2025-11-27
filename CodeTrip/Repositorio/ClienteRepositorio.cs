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
    public class ClienteRepositorio(IConfiguration configuration)
    {

        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");

        public void Cadastrar(Cliente cliente)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into Cliente (Nome_Cli, Email_Cli, Data_Nasc_Cli, CPF_Cli, Telefone_Cli, Logradouro_Cli, Numero_Cli, Bairro_Cli, Complemento_Cli, Cidade_Nome, UF_Estado) values(@nome, @email, @data_nasc, @cpf, @telefone, @logradouro, @numero, @bairro, @complemento, @cidade, @uf)", conexao);

                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = cliente.Nome_Cli;
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = cliente.Email_Cli;
                cmd.Parameters.Add("@data_nasc", MySqlDbType.Date).Value = cliente.Data_Nasc_Cli;
                cmd.Parameters.Add("@cpf", MySqlDbType.VarChar).Value = cliente.CPF_Cli;
                cmd.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = cliente.Telefone_Cli;
                cmd.Parameters.Add("@logradouro", MySqlDbType.VarChar).Value = cliente.Logradouro_Cli;
                cmd.Parameters.Add("@numero", MySqlDbType.VarChar).Value = cliente.Numero_Cli;
                cmd.Parameters.Add("@bairro", MySqlDbType.VarChar).Value = cliente.Bairro_Cli;
                cmd.Parameters.Add("@complemento", MySqlDbType.VarChar).Value = cliente.Complemento_Cli;
                cmd.Parameters.Add("@cidade", MySqlDbType.VarChar).Value = cliente.Cidade_Nome;
                cmd.Parameters.Add("@uf", MySqlDbType.VarChar).Value = cliente.UF_Estado;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public bool Atualizar(Cliente cliente)
        {
            try
            {
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    conexao.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE Cliente SET Nome_Cli = @nome, Email_Cli = @email, Data_Nasc_Cli = @data_nasc, CPF_Cli = @cpf, Telefone_Cli = @telefone, Logradouro_Cli = @logradouro, Numero_Cli = @numero, Bairro_Cli = @bairro, Complemento_Cli = @complemento, Cidade_Nome = @cidade, UF_Estado = @uf WHERE Id_Cli = @id", conexao);
                    cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = cliente.Id_Cli;
                    cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = cliente.Nome_Cli;
                    cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = cliente.Email_Cli;
                    cmd.Parameters.Add("@data_nasc", MySqlDbType.DateTime).Value = cliente.Data_Nasc_Cli;
                    cmd.Parameters.Add("@cpf", MySqlDbType.VarChar).Value = cliente.CPF_Cli;
                    cmd.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = cliente.Telefone_Cli;
                    cmd.Parameters.Add("@logradouro", MySqlDbType.VarChar).Value = cliente.Logradouro_Cli;
                    cmd.Parameters.Add("@numero", MySqlDbType.VarChar).Value = cliente.Numero_Cli;
                    cmd.Parameters.Add("@bairro", MySqlDbType.VarChar).Value = cliente.Bairro_Cli;
                    cmd.Parameters.Add("@complemento", MySqlDbType.VarChar).Value = cliente.Complemento_Cli;
                    cmd.Parameters.Add("@cidade", MySqlDbType.VarChar).Value = cliente.Cidade_Nome;
                    cmd.Parameters.Add("@uf", MySqlDbType.VarChar).Value = cliente.UF_Estado;
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


        public IEnumerable<Cliente> TodosClientes()
        {
            List<Cliente> ClienteLista = new List<Cliente>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * from Cliente", conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    ClienteLista.Add(
                                new Cliente
                                {
                                    Id_Cli = Convert.ToInt32(dr["Id_Cli"]),
                                    Nome_Cli = (string)dr["Nome_Cli"],
                                    Email_Cli = (string)dr["Email_Cli"],
                                    Data_Nasc_Cli = DateOnly.FromDateTime(Convert.ToDateTime(dr["Data_Nasc_Cli"])),
                                    CPF_Cli = (string)dr["CPF_Cli"],
                                    Telefone_Cli = (string)dr["Telefone_Cli"],
                                    Logradouro_Cli = (string)dr["Logradouro_Cli"],
                                    Numero_Cli = (string)dr["Numero_Cli"],
                                    Bairro_Cli = (string)dr["Bairro_Cli"],
                                    Complemento_Cli = (string)dr["Complemento_Cli"],
                                    Cidade_Nome = (string)dr["Cidade_Nome"],
                                    UF_Estado = (string)dr["UF_Estado"]
                                });
                }
                return ClienteLista;
            }
        }

        public Cliente ObterCliente(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Cliente WHERE Id_Cli=@id", conexao);
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;
                Cliente cliente = new Cliente();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    cliente.Id_Cli = Convert.ToInt32(dr["Id_Cli"]);
                    cliente.Nome_Cli = (string)(dr["Nome_Cli"]);
                    cliente.Email_Cli = (string)(dr["Email_Cli"]);
                    cliente.Data_Nasc_Cli = DateOnly.FromDateTime(Convert.ToDateTime(dr["Data_Nasc_Cli"]));
                    cliente.CPF_Cli = (string)(dr["CPF_Cli"]);
                    cliente.Telefone_Cli = (string)(dr["Telefone_Cli"]);
                    cliente.Logradouro_Cli = (string)(dr["Logradouro_Cli"]);
                    cliente.Numero_Cli = (string)(dr["Numero_Cli"]);
                    cliente.Bairro_Cli = (string)(dr["Bairro_Cli"]);
                    cliente.Complemento_Cli = (string)(dr["Complemento_Cli"]);
                    cliente.Cidade_Nome = (string)(dr["Cidade_Nome"]);
                    cliente.UF_Estado = (string)(dr["UF_Estado"]);
                }
                return cliente;
            }
        }

        public void Excluir(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM Cliente WHERE Id_Cli=@id", conexao);
                cmd.Parameters.AddWithValue("@id", id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }

        }

        public List<Estado> Estados()
        {
            var lista = new List<Estado>();
            using (MySqlConnection conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                string query = "SELECT UF_Estado, Nome_Estado from Estado";
                using (MySqlCommand comando = new MySqlCommand(query, conexao))
                {
                    using (MySqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Estado
                            {
                                UF_Estado = reader.GetString("UF_Estado"),
                                Nome_Estado = reader.GetString("Nome_Estado")
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public List<Cidade> Cidades()
        {
            var lista = new List<Cidade>();
            using (MySqlConnection conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                string query = "SELECT UF_Estado, Cidade_Nome from Cidade";
                using (MySqlCommand comando = new MySqlCommand(query, conexao))
                {
                    using (MySqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Cidade
                            {
                                UF_Estado = reader.GetString("UF_Estado"),
                                Cidade_Nome = reader.GetString("Cidade_Nome")
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}

