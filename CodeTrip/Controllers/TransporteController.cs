using Microsoft.AspNetCore.Mvc;
using CodeTrip.Models;
using CodeTrip.Repositorio;
using MySqlX.XDevAPI;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeTrip.Controllers
{
    public class TransporteController : Controller
    {
        private readonly TransporteRepositorio _transporteRepositorio;



        public TransporteController(TransporteRepositorio transporteRepositorio)
        {
            _transporteRepositorio = transporteRepositorio;
        }

        public IActionResult Index()
        {
            return View(_transporteRepositorio.TodosTransportes());
        }

        public IActionResult CadastrarTransporte()
        {
            var estados = _transporteRepositorio.Estados() ?? new List<Estado>();
            ViewBag.Estados = new SelectList(estados, "UF_Estado", "UF_Estado");
            var cidades = _transporteRepositorio.Cidades() ?? new List<Cidade>();
            ViewBag.Cidades = new SelectList(cidades, "Cidade_Nome", "Cidade_Nome");

            return View();
        }

        [HttpPost]
        public IActionResult CadastrarTransporte(Transporte transporte)
        {

            _transporteRepositorio.Cadastrar(transporte);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditarTransporte(int id)
        {
            var estados = _transporteRepositorio.Estados() ?? new List<Estado>();
            ViewBag.Estados = new SelectList(estados, "UF_Estado", "UF_Estado");
            var cidades = _transporteRepositorio.Cidades() ?? new List<Cidade>();
            ViewBag.Cidades = new SelectList(cidades, "Cidade_Nome", "Cidade_Nome");

            var transporte = _transporteRepositorio.ObterTransporte(id);

            if (transporte == null)
            {
                return NotFound();
            }

            return View(transporte);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarTransporte(int id, [Bind("Id_Transp, Tipo_Transp, UF_Estado")] Transporte transporte)
        {
            if (id != transporte.Id_Transp)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (_transporteRepositorio.Atualizar(transporte))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao Editar.");
                    return View(transporte);
                }
            }
            return View(transporte);
        }

        public IActionResult ExcluirTransporte(int id)
        {
            _transporteRepositorio.Excluir(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
