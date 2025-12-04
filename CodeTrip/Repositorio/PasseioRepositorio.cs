using MySql.Data.MySqlClient;
using System.Data;
using CodeTrip.Models;

namespace CodeTrip.Repositorio
{
    public class PasseioRepositorio
    {
        private readonly string _conexaoMySQL;

        public PasseioRepositorio(IConfiguration configuration)
        {
            _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");
        }

        public void Cadastrar(Passeio passeio)
        {
            using var conexao = new MySqlConnection(_conexaoMySQL);
            conexao.Open();

            string sql = @"INSERT INTO Passeio 
                           (Nome_Passeio, Desc_Passeio, Valor_Passeio, Duracao_Passeio, Cidade_Nome, UF_Estado)
                           VALUES (@Nome_Passeio, @Desc_Passeio, @Valor_Passeio, @Duracao_Passeio, @Cidade_Nome, @UF_Estado)";

            using var cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@Nome_Passeio", passeio.Nome_Passeio);
            cmd.Parameters.AddWithValue("@Desc_Passeio", passeio.Desc_Passeio);
            cmd.Parameters.AddWithValue("@Valor_Passeio", passeio.Valor_Passeio);
            cmd.Parameters.AddWithValue("@Duracao_Passeio", passeio.Duracao_Passeio);
            cmd.Parameters.AddWithValue("@Cidade_Nome", passeio.Cidade_Nome);
            cmd.Parameters.AddWithValue("@UF_Estado", passeio.UF_Estado);

            cmd.ExecuteNonQuery();
        }

        public bool Atualizar(Passeio passeio)
        {
            try
            {
                using var conexao = new MySqlConnection(_conexaoMySQL);
                conexao.Open();

                string sql = @"UPDATE Passeio SET 
                               Nome_Passeio=@Nome_Passeio,
                               Desc_Passeio=@Desc_Passeio,
                               Valor_Passeio=@Valor_Passeio,
                               Duracao_Passeio=@Duracao_Passeio,
                               Cidade_Nome=@Cidade_Nome,
                               UF_Estado=@UF_Estado
                               WHERE Id_Passeio=@Id_Passeio";

                using var cmd = new MySqlCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@Id_Passeio", passeio.Id_Passeio);
                cmd.Parameters.AddWithValue("@Nome_Passeio", passeio.Nome_Passeio);
                cmd.Parameters.AddWithValue("@Desc_Passeio", passeio.Desc_Passeio);
                cmd.Parameters.AddWithValue("@Valor_Passeio", passeio.Valor_Passeio);
                cmd.Parameters.AddWithValue("@Duracao_Passeio", passeio.Duracao_Passeio);
                cmd.Parameters.AddWithValue("@Cidade_Nome", passeio.Cidade_Nome);
                cmd.Parameters.AddWithValue("@UF_Estado", passeio.UF_Estado);

                int linhasAfetadas = cmd.ExecuteNonQuery();
                return linhasAfetadas > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar passeio: {ex.Message}");
                return false;
            }
        }

        public List<Passeio> TodosPasseios()
        {
            var lista = new List<Passeio>();
            using var conexao = new MySqlConnection(_conexaoMySQL);
            conexao.Open();

            string sql = "SELECT * FROM Passeio";
            using var cmd = new MySqlCommand(sql, conexao);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Passeio
                {
                    Id_Passeio = reader.GetInt32("Id_Passeio"),
                    Nome_Passeio = reader.GetString("Nome_Passeio"),
                    Desc_Passeio = reader.IsDBNull(reader.GetOrdinal("Desc_Passeio")) ? null : reader.GetString("Desc_Passeio"),
                    Valor_Passeio = reader.GetDecimal("Valor_Passeio"),
                    Duracao_Passeio = reader.IsDBNull(reader.GetOrdinal("Duracao_Passeio")) ? null : reader.GetString("Duracao_Passeio"),
                    Cidade_Nome = reader.GetString("Cidade_Nome"),
                    UF_Estado = reader.GetString("UF_Estado"),
                    Ativo = reader.GetBoolean("ativo")
                });
            }

            return lista;
        }

        public Passeio ObterPasseio(int id)
        {
            using var conexao = new MySqlConnection(_conexaoMySQL);
            conexao.Open();

            string sql = "SELECT * FROM Passeio WHERE Id_Passeio=@id";
            using var cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Passeio
                {
                    Id_Passeio = reader.GetInt32("Id_Passeio"),
                    Nome_Passeio = reader.GetString("Nome_Passeio"),
                    Desc_Passeio = reader.IsDBNull(reader.GetOrdinal("Desc_Passeio")) ? null : reader.GetString("Desc_Passeio"),
                    Valor_Passeio = reader.GetDecimal("Valor_Passeio"),
                    Duracao_Passeio = reader.IsDBNull(reader.GetOrdinal("Duracao_Passeio")) ? null : reader.GetString("Duracao_Passeio"),
                    Cidade_Nome = reader.GetString("Cidade_Nome"),
                    UF_Estado = reader.GetString("UF_Estado"),
                    Ativo = reader.GetBoolean("ativo")
                };
            }

            return null;
        }

        public void Excluir(int id)
        {
            using var conexao = new MySqlConnection(_conexaoMySQL);
            conexao.Open();

            string sql = "DELETE FROM Passeio WHERE Id_Passeio=@id";
            using var cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }

        public List<Passeio> PasseiosAtivos()
        {
            var lista = new List<Passeio>();
            using var conexao = new MySqlConnection(_conexaoMySQL);
            conexao.Open();

            string sql = "SELECT * FROM Passeio WHERE ativo=1";
            using var cmd = new MySqlCommand(sql, conexao);
            using var reader = cmd.ExecuteReader();

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

            return lista;
        }
    }
}
