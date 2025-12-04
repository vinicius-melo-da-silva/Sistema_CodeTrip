using MySql.Data.MySqlClient;
using System.Data;
using CodeTrip.Models;

namespace CodeTrip.Repositorio
{
    public class TransporteRepositorio
    {
        private readonly string _conexaoMySQL;

        public TransporteRepositorio(IConfiguration configuration)
        {
            _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");
        }

        public void Cadastrar(Transporte transporte)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand(
                    "INSERT INTO Transporte (Tipo_Transp, UF_Estado) VALUES(@tipo_transp, @uf_estado)", conexao);

                cmd.Parameters.Add("@tipo_transp", MySqlDbType.VarChar).Value = transporte.Tipo_Transp;
                cmd.Parameters.Add("@uf_estado", MySqlDbType.VarChar).Value = transporte.UF_Estado;
                cmd.ExecuteNonQuery();
            }
        }

        public bool Atualizar(Transporte transporte)
        {
            try
            {
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    conexao.Open();
                    MySqlCommand cmd = new MySqlCommand(
                        "UPDATE Transporte SET Tipo_Transp=@tipo_transp, UF_Estado=@uf_estado WHERE Id_Transp=@id",
                        conexao);

                    cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = transporte.Id_Transp;
                    cmd.Parameters.Add("@tipo_transp", MySqlDbType.VarChar).Value = transporte.Tipo_Transp;
                    cmd.Parameters.Add("@uf_estado", MySqlDbType.VarChar).Value = transporte.UF_Estado;

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Transporte> TodosTransportes()
        {
            var lista = new List<Transporte>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Transporte", conexao);
                DataTable dt = new DataTable();
                new MySqlDataAdapter(cmd).Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lista.Add(new Transporte
                    {
                        Id_Transp = Convert.ToInt32(dr["Id_Transp"]),
                        Tipo_Transp = dr["Tipo_Transp"].ToString(),
                        UF_Estado = dr["UF_Estado"].ToString()
                    });
                }
            }

            return lista;
        }

        public Transporte ObterTransporte(int id)
        {
            Transporte transporte = null;

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand(
                    "SELECT * FROM Transporte WHERE Id_Transp=@id", conexao);

                cmd.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        transporte = new Transporte
                        {
                            Id_Transp = Convert.ToInt32(dr["Id_Transp"]),
                            Tipo_Transp = dr["Tipo_Transp"].ToString(),
                            UF_Estado = dr["UF_Estado"].ToString()
                        };
                    }
                }
            }

            return transporte;
        }

        public void Excluir(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand(
                    "DELETE FROM Transporte WHERE Id_Transp=@id", conexao);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Estado> Estados()
        {
            var lista = new List<Estado>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                string query = "SELECT UF_Estado, Nome_Estado FROM Estado";

                using (MySqlCommand comando = new MySqlCommand(query, conexao))
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

            return lista;
        }
    }
}
