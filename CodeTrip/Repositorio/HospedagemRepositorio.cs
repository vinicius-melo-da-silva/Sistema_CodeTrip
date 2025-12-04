using MySql.Data.MySqlClient;
using System.Data;
using CodeTrip.Models;
using System.Collections.Generic;

namespace CodeTrip.Repositorio
{
    public class HospedagemRepositorio
    {
        private readonly string _conexaoMySQL;

        public HospedagemRepositorio(IConfiguration configuration)
        {
            _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");
        }

        public void Cadastrar(Hospedagem hospedagem)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand(
                    "INSERT INTO Hospedagem (Nome_Hospedagem, Id_Tipo_Hospedagem, Id_Pensao, " +
                    "Logradouro_Endereco_Hospedagem, Numero_Endereco_Hospedagem, Bairro_Endereco_Hospedagem, " +
                    "Complemento_Endereco_Hospedagem, Cidade_Nome, UF_Estado, ativo) " +
                    "VALUES (@nome, @tipo, @pensao, @logradouro, @numero, @bairro, @complemento, @cidade_nome, @uf_estado, 1)",
                    conexao);

                cmd.Parameters.AddWithValue("@nome", hospedagem.Nome_Hospedagem);
                cmd.Parameters.AddWithValue("@tipo", hospedagem.Id_Tipo_Hospedagem);
                cmd.Parameters.AddWithValue("@pensao", hospedagem.Id_Pensao);
                cmd.Parameters.AddWithValue("@logradouro", hospedagem.Logradouro_Endereco_Hospedagem);
                cmd.Parameters.AddWithValue("@numero", hospedagem.Numero_Endereco_Hospedagem);
                cmd.Parameters.AddWithValue("@bairro", hospedagem.Bairro_Endereco_Hospedagem);
                cmd.Parameters.AddWithValue("@complemento", hospedagem.Complemento_Endereco_Hospedagem);
                cmd.Parameters.AddWithValue("@cidade_nome", hospedagem.Cidade_Nome);
                cmd.Parameters.AddWithValue("@uf_estado", hospedagem.UF_Estado);
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public bool Atualizar(Hospedagem hospedagem)
        {
            try
            {
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    conexao.Open();
                    MySqlCommand cmd = new MySqlCommand(
                        "UPDATE Hospedagem SET " +
                        "Nome_Hospedagem = @nome, " +
                        "Id_Tipo_Hospedagem = @tipo, " +
                        "Id_Pensao = @pensao, " +
                        "Logradouro_Endereco_Hospedagem = @logradouro, " +
                        "Numero_Endereco_Hospedagem = @numero, " +
                        "Bairro_Endereco_Hospedagem = @bairro, " +
                        "Complemento_Endereco_Hospedagem = @complemento, " +
                        "Cidade_Nome = @cidade_nome, " +
                        "UF_Estado = @uf_estado " +
                        "WHERE Id_Hospedagem = @id",
                        conexao);

                    cmd.Parameters.AddWithValue("@id", hospedagem.Id_Hospedagem);
                    cmd.Parameters.AddWithValue("@nome", hospedagem.Nome_Hospedagem);
                    cmd.Parameters.AddWithValue("@tipo", hospedagem.Id_Tipo_Hospedagem);
                    cmd.Parameters.AddWithValue("@pensao", hospedagem.Id_Pensao);
                    cmd.Parameters.AddWithValue("@logradouro", hospedagem.Logradouro_Endereco_Hospedagem);
                    cmd.Parameters.AddWithValue("@numero", hospedagem.Numero_Endereco_Hospedagem);
                    cmd.Parameters.AddWithValue("@bairro", hospedagem.Bairro_Endereco_Hospedagem);
                    cmd.Parameters.AddWithValue("@complemento", hospedagem.Complemento_Endereco_Hospedagem);
                    cmd.Parameters.AddWithValue("@cidade_nome", hospedagem.Cidade_Nome);
                    cmd.Parameters.AddWithValue("@uf_estado", hospedagem.UF_Estado);

                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    return linhasAfetadas > 0;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro ao atualizar hospedagem: {ex.Message}");
                return false;
            }
        }

        public IEnumerable<Hospedagem> TodasHospedagens()
        {
            List<Hospedagem> HospedagemLista = new List<Hospedagem>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                string query = @"
                    SELECT 
                        h.*,
                        th.Desc_Hospedagem as Desc_Hospedagem,
                        tp.Desc_Pensao as Desc_Pensao
                    FROM Hospedagem h
                    LEFT JOIN Tipo_Hospedagem th ON h.Id_Tipo_Hospedagem = th.Id_Tipo_Hospedagem
                    LEFT JOIN Tipo_Pensao tp ON h.Id_Pensao = tp.Id_Pensao
                    WHERE h.ativo = 1";

                MySqlCommand cmd = new MySqlCommand(query, conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    HospedagemLista.Add(
                        new Hospedagem
                        {
                            Id_Hospedagem = Convert.ToInt32(dr["Id_Hospedagem"]),
                            Nome_Hospedagem = dr["Nome_Hospedagem"].ToString(),
                            Id_Tipo_Hospedagem = Convert.ToInt32(dr["Id_Tipo_Hospedagem"]),
                            Desc_Hospedagem = dr["Desc_Hospedagem"].ToString(),
                            Id_Pensao = Convert.ToInt32(dr["Id_Pensao"]),
                            Desc_Pensao = dr["Desc_Pensao"].ToString(),
                            Logradouro_Endereco_Hospedagem = dr["Logradouro_Endereco_Hospedagem"].ToString(),
                            Numero_Endereco_Hospedagem = dr["Numero_Endereco_Hospedagem"].ToString(),
                            Bairro_Endereco_Hospedagem = dr["Bairro_Endereco_Hospedagem"].ToString(),
                            Complemento_Endereco_Hospedagem = dr["Complemento_Endereco_Hospedagem"].ToString(),
                            Cidade_Nome = dr["Cidade_Nome"].ToString(),
                            UF_Estado = dr["UF_Estado"].ToString(),
                            ativo = Convert.ToInt32(dr["ativo"])
                        });
                }
                return HospedagemLista;
            }
        }

        public Hospedagem ObterHospedagem(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                string query = @"
                    SELECT 
                        h.*,
                        th.Desc_Hospedagem as Desc_Tipo_Hospedagem,
                        tp.Desc_Pensao as Desc_Tipo_Pensao
                    FROM Hospedagem h
                    LEFT JOIN Tipo_Hospedagem th ON h.Id_Tipo_Hospedagem = th.Id_Tipo_Hospedagem
                    LEFT JOIN Tipo_Pensao tp ON h.Id_Pensao = tp.Id_Pensao
                    WHERE h.Id_Hospedagem = @id AND h.ativo = 1";

                MySqlCommand cmd = new MySqlCommand(query, conexao);
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                Hospedagem hospedagem = null;
                while (dr.Read())
                {
                    hospedagem = new Hospedagem
                    {
                        Id_Hospedagem = Convert.ToInt32(dr["Id_Hospedagem"]),
                        Nome_Hospedagem = dr["Nome_Hospedagem"].ToString(),
                        Id_Tipo_Hospedagem = Convert.ToInt32(dr["Id_Tipo_Hospedagem"]),
                        Desc_Hospedagem = dr["Desc_Hospedagem"].ToString(),
                        Id_Pensao = Convert.ToInt32(dr["Id_Pensao"]),
                        Desc_Pensao = dr["Desc_Pensao"].ToString(),
                        Logradouro_Endereco_Hospedagem = dr["Logradouro_Endereco_Hospedagem"].ToString(),
                        Numero_Endereco_Hospedagem = dr["Numero_Endereco_Hospedagem"].ToString(),
                        Bairro_Endereco_Hospedagem = dr["Bairro_Endereco_Hospedagem"].ToString(),
                        Complemento_Endereco_Hospedagem = dr["Complemento_Endereco_Hospedagem"].ToString(),
                        Cidade_Nome = dr["Cidade_Nome"].ToString(),
                        UF_Estado = dr["UF_Estado"].ToString(),
                        ativo = Convert.ToInt32(dr["ativo"])
                    };
                }
                return hospedagem;
            }
        }

        public void Excluir(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE Hospedagem SET ativo = 0 WHERE Id_Hospedagem = @id", conexao);
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

        public List<Tipo_Hospedagem> TiposHospedagem()
        {
            var lista = new List<Tipo_Hospedagem>();
            using (MySqlConnection conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                string query = "SELECT Id_Tipo_Hospedagem, Desc_Hospedagem FROM Tipo_Hospedagem";
                using (MySqlCommand comando = new MySqlCommand(query, conexao))
                {
                    using (MySqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Tipo_Hospedagem
                            {
                                Id_Tipo_Hospedagem = reader.GetInt32("Id_Tipo_Hospedagem"),
                                Desc_Hospedagem = reader.GetString("Desc_Hospedagem")
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public List<Tipo_Pensao> TiposPensao()
        {
            var lista = new List<Tipo_Pensao>();
            using (MySqlConnection conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                string query = "SELECT Id_Pensao, Desc_Pensao FROM Tipo_Pensao";
                using (MySqlCommand comando = new MySqlCommand(query, conexao))
                {
                    using (MySqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Tipo_Pensao
                            {
                                Id_Pensao = reader.GetInt32("Id_Pensao"),
                                Desc_Pensao = reader.GetString("Desc_Pensao")
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}