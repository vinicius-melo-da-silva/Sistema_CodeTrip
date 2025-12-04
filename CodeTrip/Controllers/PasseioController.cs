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
        public IActionResult Index()
        {
            var passeios = _repositorio.TodosPasseios();
            return View(passeios);
        }

        public IActionResult CadastrarPasseio()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CadastrarPasseio(Passeio passeio)
        {
            if (ModelState.IsValid)
            {
                _repositorio.Cadastrar(passeio);
                return RedirectToAction(nameof(Index));
            }

            return View(passeio);
        }

        public IActionResult EditarPasseio(int id)
        {
            var passeio = _repositorio.ObterPasseio(id);
            if (passeio == null)
                return NotFound();

            return View(passeio);
        }

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

            return View(passeio);
        }

        public IActionResult Excluir(int id)
        {
            _repositorio.Excluir(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
