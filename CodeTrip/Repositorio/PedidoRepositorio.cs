using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using CodeTrip.Models;

namespace CodeTrip.Repositorio
{
    public class PedidoRepositorio
    {
        private readonly string _conexaoMySQL;

        public PedidoRepositorio(IConfiguration configuration)
        {
            _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");
        }

        public void Cadastrar(Pedido pedido)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO Pedido (Id_Usuario, CPF_Cli, Id_Origem, Id_Destino, Data_Inicio, Data_Fim, Id_Transp, Id_End_Transporte, Id_Hospedagem, Id_Pagamento, Id_Passeio, ativo) VALUES(@Id_Usuario, @CPF_Cli, @Id_Origem, @Id_Destino, @Data_Inicio, @Data_Fim, @Id_Transp, @Id_End_Transporte, @Id_Hospedagem, @Id_Pagamento, @Id_Passeio, @ativo)", conexao);

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
                cmd.Parameters.Add("@ativo", MySqlDbType.Bit).Value = pedido.Ativo;
                cmd.ExecuteNonQuery();
            }
        }

        public bool Atualizar(Pedido pedido)
        {
            try
            {
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    conexao.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE Pedido SET Id_Usuario=@Id_Usuario, CPF_Cli=@CPF_Cli, Id_Origem=@Id_Origem, Id_Destino=@Id_Destino, Data_Inicio=@Data_Inicio, Data_Fim=@Data_Fim, Id_Transp=@Id_Transp, Id_End_Transporte=@Id_End_Transporte, Id_Hospedagem=@Id_Hospedagem, Id_Pagamento=@Id_Pagamento, Id_Passeio=@Id_Passeio, ativo=@ativo WHERE Id_Pedido=@Id_Pedido", conexao);

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
                    cmd.Parameters.Add("@ativo", MySqlDbType.Bit).Value = pedido.Ativo;

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch { return false; }
        }

        public IEnumerable<Pedido> TodosPedidos()
        {
            List<Pedido> lista = new List<Pedido>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Pedido", conexao);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Pedido
                    {
                        Id_Pedido = reader.GetInt32("Id_Pedido"),
                        Id_Usuario = reader.GetInt32("Id_Usuario"),
                        CPF_Cli = reader.GetString("CPF_Cli"),
                        Id_Origem = reader.GetInt32("Id_Origem"),
                        Id_Destino = reader.GetInt32("Id_Destino"),
                        Data_Inicio = DateOnly.FromDateTime(reader.GetDateTime("Data_Inicio")),
                        Data_Fim = DateOnly.FromDateTime(reader.GetDateTime("Data_Fim")),
                        Id_Transp = reader.GetInt32("Id_Transp"),
                        Id_End_Transporte = reader.GetInt32("Id_End_Transporte"),
                        Id_Hospedagem = reader.GetInt32("Id_Hospedagem"),
                        Id_Pagamento = reader.GetInt32("Id_Pagamento"),
                        Id_Passeio = reader.GetInt32("Id_Passeio"),
                        Ativo = reader.GetBoolean("ativo")
                    });
                }
            }
            return lista;
        }

        public Pedido ObterPedido(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Pedido WHERE Id_Pedido=@id", conexao);
                cmd.Parameters.AddWithValue("@id", id);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Pedido
                    {
                        Id_Pedido = reader.GetInt32("Id_Pedido"),
                        Id_Usuario = reader.GetInt32("Id_Usuario"),
                        CPF_Cli = reader.GetString("CPF_Cli"),
                        Id_Origem = reader.GetInt32("Id_Origem"),
                        Id_Destino = reader.GetInt32("Id_Destino"),
                        Data_Inicio = DateOnly.FromDateTime(reader.GetDateTime("Data_Inicio")),
                        Data_Fim = DateOnly.FromDateTime(reader.GetDateTime("Data_Fim")),
                        Id_Transp = reader.GetInt32("Id_Transp"),
                        Id_End_Transporte = reader.GetInt32("Id_End_Transporte"),
                        Id_Hospedagem = reader.GetInt32("Id_Hospedagem"),
                        Id_Pagamento = reader.GetInt32("Id_Pagamento"),
                        Id_Passeio = reader.GetInt32("Id_Passeio"),
                        Ativo = reader.GetBoolean("ativo")
                    };
                }
                return null;
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

        public List<Usuario> Usuarios()
        {
            var lista = new List<Usuario>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                var cmd = new MySqlCommand("SELECT Id_Usuario, Nome_Usuario FROM Usuario", conexao);
                using var rd = cmd.ExecuteReader();
                while (rd.Read()) lista.Add(new Usuario { Id_Usuario = rd.GetInt32("Id_Usuario"), Nome_Usuario = rd.GetString("Nome_Usuario") });
            }
            return lista;
        }

        public List<Cliente> Clientes()
        {
            var lista = new List<Cliente>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                var cmd = new MySqlCommand("SELECT Nome_Cli, CPF_Cli FROM Cliente", conexao);
                using var rd = cmd.ExecuteReader();
                while (rd.Read()) lista.Add(new Cliente { Nome_Cli = rd.GetString("Nome_Cli"), CPF_Cli = rd.GetString("CPF_Cli") });
            }
            return lista;
        }

        public List<Transporte> Transportes()
        {
            var lista = new List<Transporte>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                var cmd = new MySqlCommand("SELECT Id_Transp, Tipo_Transp, UF_Estado FROM Transporte", conexao);
                using var rd = cmd.ExecuteReader();
                while (rd.Read()) lista.Add(new Transporte { Id_Transp = rd.GetInt32("Id_Transp"), Tipo_Transp = rd.GetString("Tipo_Transp"), UF_Estado = rd.GetString("UF_Estado") });
            }
            return lista;
        }

        public List<End_Transporte> End_Transportes()
        {
            var lista = new List<End_Transporte>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                var cmd = new MySqlCommand("SELECT Id_End_Transporte, Logradouro_End_Transporte, Numero_End_Transporte, Cidade_Nome, UF_Estado FROM End_Transporte", conexao);
                using var rd = cmd.ExecuteReader();
                while (rd.Read()) lista.Add(new End_Transporte { Id_End_Transporte = rd.GetInt32("Id_End_Transporte"), Logradouro_End_Transporte = rd.GetString("Logradouro_End_Transporte"), Numero_End_Transporte = rd.GetString("Numero_End_Transporte"), Cidade_Nome = rd.GetString("Cidade_Nome"), UF_Estado = rd.GetString("UF_Estado") });
            }
            return lista;
        }

        public List<Hospedagem> Hospedagens()
        {
            var lista = new List<Hospedagem>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                var cmd = new MySqlCommand("SELECT Id_Hospedagem, Nome_Hospedagem FROM Hospedagem", conexao);
                using var rd = cmd.ExecuteReader();
                while (rd.Read()) lista.Add(new Hospedagem { Id_Hospedagem = rd.GetInt32("Id_Hospedagem"), Nome_Hospedagem = rd.GetString("Nome_Hospedagem") });
            }
            return lista;
        }

        public List<Pagamento> Pagamentos()
        {
            var lista = new List<Pagamento>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                var cmd = new MySqlCommand("SELECT Id_Pagamento, Desc_Pagamento FROM Pagamento", conexao);
                using var rd = cmd.ExecuteReader();
                while (rd.Read()) lista.Add(new Pagamento { Id_Pagamento = rd.GetInt32("Id_Pagamento"), Desc_Pagamento = rd.GetString("Desc_Pagamento") });
            }
            return lista;
        }

        public List<OrigemViagem> Origens()
        {
            var lista = new List<OrigemViagem>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                var cmd = new MySqlCommand("SELECT Id_Origem, Cidade_Nome, UF_Estado FROM Origem_Viagem", conexao);
                using var rd = cmd.ExecuteReader();
                while (rd.Read()) lista.Add(new OrigemViagem { Id_Origem = rd.GetInt32("Id_Origem"), Cidade_Nome = rd.GetString("Cidade_Nome"), UF_Estado = rd.GetString("UF_Estado") });
            }
            return lista;
        }

        public List<DestinoViagem> Destinos()
        {
            var lista = new List<DestinoViagem>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                var cmd = new MySqlCommand("SELECT Id_Destino, Desc_Destino, Cidade_Nome, UF_Estado, Valor_Destino FROM Destino_Viagem", conexao);
                using var rd = cmd.ExecuteReader();
                while (rd.Read()) lista.Add(new DestinoViagem { Id_Destino = rd.GetInt32("Id_Destino"), Desc_Destino = rd.GetString("Desc_Destino"), Cidade_Nome = rd.GetString("Cidade_Nome"), UF_Estado = rd.GetString("UF_Estado"), Valor_Destino = rd.GetDecimal("Valor_Destino") });
            }
            return lista;
        }

        public List<Passeio> Passeios()
        {
            var lista = new List<Passeio>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                var cmd = new MySqlCommand("SELECT Id_Passeio, Desc_Passeio FROM Passeio", conexao);
                using var rd = cmd.ExecuteReader();
                while (rd.Read()) lista.Add(new Passeio { Id_Passeio = rd.GetInt32("Id_Passeio"), Desc_Passeio = rd.GetString("Desc_Passeio") });
            }
            return lista;
        }
    }
}
