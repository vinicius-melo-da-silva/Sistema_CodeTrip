using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using CodeTrip.Autenticacao;

namespace CodeTrip.Controllers
{
    public class AuthController : Controller
    {
        private readonly string _connectionString;

        public AuthController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View("~/Views/Usuario/Login.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string Email_Usuario, string Senha_Usuario, string? returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(Email_Usuario) || string.IsNullOrWhiteSpace(Senha_Usuario))
            {
                TempData["Error"] = "Informe e-mail e senha.";
                return View("~/Views/Usuario/Login.cshtml");
            }

            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            using var cmd = new MySqlCommand("SELECT * FROM Usuario WHERE Email_Usuario = @Email", conn);
            cmd.Parameters.AddWithValue("@Email", Email_Usuario);

            using var rd = cmd.ExecuteReader();

            if (!rd.Read())
            {
                TempData["Error"] = "Usuário não encontrado.";
                return View("~/Views/Usuario/Login.cshtml");
            }

            var id = rd.GetInt32("Id_Usuario");
            var nome = rd.GetString("Nome_Usuario");
            var email = rd.GetString("Email_Usuario");
            var role = rd.GetString("role");
            var ativo = rd.GetBoolean("ativo");
            var senhaBanco = rd["Senha_Usuario"] as string ?? "";

            if (!ativo)
            {
                TempData["Error"] = "Usuário inativo.";
                return View("~/Views/Usuario/Login.cshtml");
            }

            if (Senha_Usuario != senhaBanco)
            {
                TempData["Error"] = "Senha inválida.";
                return View("~/Views/Usuario/Login.cshtml");
            }

            HttpContext.Session.SetInt32(SessionKeys.UserId, id);
            HttpContext.Session.SetString(SessionKeys.UserName, nome);
            HttpContext.Session.SetString(SessionKeys.UserEmail, email);
            HttpContext.Session.SetString(SessionKeys.UserRole, role);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("MenuSistema", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("MenuSistema", "Home");
        }

        [HttpGet]
        public IActionResult AcessoNegado() => View();
    }
}