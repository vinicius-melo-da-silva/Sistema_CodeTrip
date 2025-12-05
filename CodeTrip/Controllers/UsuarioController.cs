using CodeTrip.Models;
using CodeTrip.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace CodeTrip.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(UsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Email_Usuario, string Senha_Usuario)
        {
            var usuario = _usuarioRepositorio.ObterUsuario(Email_Usuario);

            if (usuario != null && usuario.Ativo)
            {
                if (usuario.Senha_Usuario == Senha_Usuario)
                {
                    // ✔ Salvar na sessão
                    HttpContext.Session.Set("usuarioLogado", usuario);

                    // ✔ Se ainda quiser usar TempData:
                    TempData["NomeUsuario"] = usuario.Nome_Usuario;
                    TempData["Role"] = usuario.Role;

                    return RedirectToAction("MenuSistema", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Senha inválida.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Usuário não encontrado ou inativo.");
            }

            return View();
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _usuarioRepositorio.Cadastrar(usuario);
                TempData["Mensagem"] = "Usuário cadastrado com sucesso!";
                return RedirectToAction("Login");
            }

            return View(usuario);
        }
    }
}
