using MySql.Data.MySqlClient;
using System.Data;
using CodeTrip.Models;
using System.Collections.Generic;

namespace CodeTrip.Repositorio
{
    public class ClienteRepositorio
    {
        private readonly string _conexaoMySQL;

        public ClienteRepositorio(IConfiguration configuration)
        {
            _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");
        }

        public void Cadastrar(Cliente cliente)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand(
                    "INSERT INTO Cliente (Nome_Cli, Email_Cli, Data_Nasc_Cli, CPF_Cli, Telefone_Cli, " +
                    "Logradouro_Cli, Numero_Cli, Bairro_Cli, Complemento_Cli, Cidade_Nome, UF_Estado, ativo) " +
                    "VALUES (@nome, @email, @data_nasc, @cpf, @telefone, @logradouro, @numero, @bairro, " +
                    "@complemento, @cidade_nome, @uf_estado, 1)",
                    conexao);

                cmd.Parameters.AddWithValue("@nome", cliente.Nome_Cli);
                cmd.Parameters.AddWithValue("@email", cliente.Email_Cli);
                cmd.Parameters.AddWithValue("@data_nasc", cliente.Data_Nasc_Cli);
                cmd.Parameters.AddWithValue("@cpf", cliente.CPF_Cli);
                cmd.Parameters.AddWithValue("@telefone", cliente.Telefone_Cli);
                cmd.Parameters.AddWithValue("@logradouro", cliente.Logradouro_Cli);
                cmd.Parameters.AddWithValue("@numero", cliente.Numero_Cli);
                cmd.Parameters.AddWithValue("@bairro", cliente.Bairro_Cli);
                cmd.Parameters.AddWithValue("@complemento", cliente.Complemento_Cli);
                cmd.Parameters.AddWithValue("@cidade_nome", cliente.Cidade_Nome);
                cmd.Parameters.AddWithValue("@uf_estado", cliente.UF_Estado);
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
                    MySqlCommand cmd = new MySqlCommand(
                        "UPDATE Cliente SET " +
                        "Nome_Cli = @nome, " +
                        "Email_Cli = @email, " +
                        "Data_Nasc_Cli = @data_nasc, " +
                        "CPF_Cli = @cpf, " +
                        "Telefone_Cli = @telefone, " +
                        "Logradouro_Cli = @logradouro, " +
                        "Numero_Cli = @numero, " +
                        "Bairro_Cli = @bairro, " +
                        "Complemento_Cli = @complemento, " +
                        "Cidade_Nome = @cidade_nome, " +
                        "UF_Estado = @uf_estado " +
                        "WHERE Id_Cli = @id",
                        conexao);

                    cmd.Parameters.AddWithValue("@id", cliente.Id_Cli);
                    cmd.Parameters.AddWithValue("@nome", cliente.Nome_Cli);
                    cmd.Parameters.AddWithValue("@email", cliente.Email_Cli);
                    cmd.Parameters.AddWithValue("@data_nasc", cliente.Data_Nasc_Cli);
                    cmd.Parameters.AddWithValue("@cpf", cliente.CPF_Cli);
                    cmd.Parameters.AddWithValue("@telefone", cliente.Telefone_Cli);
                    cmd.Parameters.AddWithValue("@logradouro", cliente.Logradouro_Cli);
                    cmd.Parameters.AddWithValue("@numero", cliente.Numero_Cli);
                    cmd.Parameters.AddWithValue("@bairro", cliente.Bairro_Cli);
                    cmd.Parameters.AddWithValue("@complemento", cliente.Complemento_Cli);
                    cmd.Parameters.AddWithValue("@cidade_nome", cliente.Cidade_Nome);
                    cmd.Parameters.AddWithValue("@uf_estado", cliente.UF_Estado);

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
                string query = @"
                    SELECT 
                        c.*,
                        e.Nome_Estado
                    FROM Cliente c
                    LEFT JOIN Estado e ON c.UF_Estado = e.UF_Estado
                    WHERE c.ativo = 1";

                MySqlCommand cmd = new MySqlCommand(query, conexao);
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
                            Nome_Cli = dr["Nome_Cli"].ToString(),
                            Email_Cli = dr["Email_Cli"].ToString(),
                            Data_Nasc_Cli = Convert.ToDateTime(dr["Data_Nasc_Cli"]),
                            CPF_Cli = dr["CPF_Cli"].ToString(),
                            Telefone_Cli = dr["Telefone_Cli"].ToString(),
                            Logradouro_Cli = dr["Logradouro_Cli"].ToString(),
                            Numero_Cli = dr["Numero_Cli"].ToString(),
                            Bairro_Cli = dr["Bairro_Cli"].ToString(),
                            Complemento_Cli = dr["Complemento_Cli"].ToString(),
                            Cidade_Nome = dr["Cidade_Nome"].ToString(),
                            UF_Estado = dr["UF_Estado"].ToString(),
                            Nome_Estado = dr["Nome_Estado"].ToString(),
                            ativo = Convert.ToInt32(dr["ativo"])
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
                string query = @"
                    SELECT 
                        c.*,
                        e.Nome_Estado
                    FROM Cliente c
                    LEFT JOIN Estado e ON c.UF_Estado = e.UF_Estado
                    WHERE c.Id_Cli = @id AND c.ativo = 1";

                MySqlCommand cmd = new MySqlCommand(query, conexao);
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                Cliente cliente = null;
                while (dr.Read())
                {
                    cliente = new Cliente
                    {
                        Id_Cli = Convert.ToInt32(dr["Id_Cli"]),
                        Nome_Cli = dr["Nome_Cli"].ToString(),
                        Email_Cli = dr["Email_Cli"].ToString(),
                        Data_Nasc_Cli = Convert.ToDateTime(dr["Data_Nasc_Cli"]),
                        CPF_Cli = dr["CPF_Cli"].ToString(),
                        Telefone_Cli = dr["Telefone_Cli"].ToString(),
                        Logradouro_Cli = dr["Logradouro_Cli"].ToString(),
                        Numero_Cli = dr["Numero_Cli"].ToString(),
                        Bairro_Cli = dr["Bairro_Cli"].ToString(),
                        Complemento_Cli = dr["Complemento_Cli"].ToString(),
                        Cidade_Nome = dr["Cidade_Nome"].ToString(),
                        UF_Estado = dr["UF_Estado"].ToString(),
                        Nome_Estado = dr["Nome_Estado"].ToString(),
                        ativo = Convert.ToInt32(dr["ativo"])
                    };
                }
                return cliente;
            }
        }

        public void Excluir(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE Cliente SET ativo = 0 WHERE Id_Cli = @id", conexao);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public List<Estado> Estados()
        {
            var lista = new List<Estado>();
            using (MySqlConnection conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                string query = "SELECT UF_Estado, Nome_Estado FROM Estado";
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
                string query = "SELECT UF_Estado, Cidade_Nome FROM Cidade";
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