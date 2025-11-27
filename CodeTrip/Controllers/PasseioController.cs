using Microsoft.AspNetCore.Mvc;
using CodeTrip.Models;
using CodeTrip.Repositorio;

namespace CodeTrip.Controllers
{
    public class PasseioController : Controller
    {
        private readonly PasseioRepositorio _repositorio;

        public PasseioController(PasseioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        // Lista todos os passeios
        public IActionResult Index()
        {
            var passeios = _repositorio.TodosPasseios();
            return View(passeios);
        }

        // FORMULÁRIO DE CADASTRO
        // GET: Passeio/CadastrarPasseio
        public IActionResult CadastrarPasseio()
        {
            return View(); // View: CadastrarPasseio.cshtml
        }

        // POST: Passeio/CadastrarPasseio
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CadastrarPasseio(Passeio passeio)
        {
            if (ModelState.IsValid)
            {
                _repositorio.Cadastrar(passeio);
                return RedirectToAction(nameof(Index));
            }

            return View(passeio); // Retorna à mesma view se houver erro
        }

        // FORMULÁRIO DE EDIÇÃO
        // GET: Passeio/EditarPasseio/{id}
        public IActionResult EditarPasseio(int id)
        {
            var passeio = _repositorio.ObterPasseio(id);
            if (passeio == null)
                return NotFound();

            return View(passeio); // View: EditarPasseio.cshtml
        }

        // POST: Passeio/EditarPasseio/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarPasseio(int id, Passeio passeio)
        {
            if (id != passeio.Id_Passeio)
                return BadRequest();

            if (ModelState.IsValid)
            {
                _repositorio.Atualizar(passeio);
                return RedirectToAction(nameof(Index));
            }

            return View(passeio); // Retorna à mesma view se houver erro
        }

        // EXCLUIR PASSEIO
        public IActionResult Excluir(int id)
        {
            _repositorio.Excluir(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
