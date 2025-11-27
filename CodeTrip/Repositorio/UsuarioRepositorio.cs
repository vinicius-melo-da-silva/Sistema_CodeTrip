using System.Data;
using CodeTrip.Models;
using MySql.Data.MySqlClient;

namespace CodeTrip.Repositorio
{

    public class UsuarioRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");



        public Usuario ObterUsuario(string email)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new("SELECT * FROM Usuario WHERE Email_Usuario = @email", conexao);
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;

                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    Usuario usuario = null;
                    if (dr.Read())
                    {

                        usuario = new Usuario
                        {
                            Id_Usuario = Convert.ToInt32(dr["Id_Usuario"]),
                            Nome_Usuario = dr["Nome_Usuario"].ToString(),
                            Email_Usuario = dr["Email_Usuario"].ToString(),
                            Senha_Usuario = dr["Senha_Usuario"].ToString()
                        };
                    }
                    return usuario;
                }
            }
        }

        public void Cadastrar(Usuario usuario)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                var cmd = new MySqlCommand(
                    "INSERT INTO Usuario (Nome_Usuario, Email_Usuario, Senha_Usuario, Role, Ativo) " +
                    "VALUES (@nome, @email, @senha, @role, 1)", conexao);

                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = usuario.Nome_Usuario;
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = usuario.Email_Usuario;
                cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = usuario.Senha_Usuario;
                cmd.Parameters.Add("@role", MySqlDbType.VarChar).Value = usuario.Role ?? "Comum";

                cmd.ExecuteNonQuery();
            }
        }

    }

}

