namespace CodeTrip.Models
{
    public class Usuario
    {
        public int? Id_Usuario { get; set; }
        public string? Nome_Usuario { get; set; }
        public string? Email_Usuario { get; set; }
        public string? Senha_Usuario { get; set; }

        public string? Role { get; set; } // "Colaborador", "Comum", "Admin"
        public bool Ativo { get; set; } = true;
    }
}