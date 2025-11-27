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
    public class TransporteRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");
        public void Cadastrar(Transporte transporte)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into Transporte (Tipo_Transp, UF_Estado) values(@tipo_transp, @uf_estado)", conexao);

                cmd.Parameters.Add("@tipo_transp", MySqlDbType.VarChar).Value = transporte.Tipo_Transp;
                cmd.Parameters.Add("@uf_estado", MySqlDbType.VarChar).Value = transporte.UF_Estado;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public bool Atualizar(Transporte transporte)
        {
            try
            {
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    conexao.Open();
                    MySqlCommand cmd = new MySqlCommand("Update Transporte set Tipo_Transp=@tipo_transp, UF_Estado=@uf_estado" + " where Id_Transp=@id ", conexao);
                    cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = transporte.Id_Transp;
                    cmd.Parameters.Add("@tipo_transp", MySqlDbType.VarChar).Value = transporte.Tipo_Transp;
                    cmd.Parameters.Add("@uf_estado", MySqlDbType.VarChar).Value = transporte.UF_Estado;
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

        public IEnumerable<Transporte> TodosTransportes()
        {
            List<Transporte> TransporteLista = new List<Transporte>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * from Transporte", conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    TransporteLista.Add(
                                new Transporte
                                {
                                    Id_Transp = Convert.ToInt32(dr["Id_Transp"]),
                                    Tipo_Transp = (string)dr["Tipo_Transp"],
                                    UF_Estado = (string)dr["UF_Estado"]
                                });
                }
                return TransporteLista;
            }
        }

        public Transporte ObterTransporte(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Transporte WHERE Id_Transp=@id", conexao);
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;
                Transporte transporte = new Transporte();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    transporte.Id_Transp = Convert.ToInt32(dr["Id_Transp"]);
                    transporte.Tipo_Transp = ((string)dr["Tipo_Transp"]);
                    transporte.UF_Estado = ((string)dr["UF_Estado"]);
                }
                return transporte;
            }
        }

        public void Excluir(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM Transporte WHERE Id_Transp=@id", conexao);
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
