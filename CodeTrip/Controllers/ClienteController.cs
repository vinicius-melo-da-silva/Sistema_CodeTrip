using Microsoft.AspNetCore.Mvc;
using CodeTrip.Models;
using CodeTrip.Repositorio;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeTrip.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteRepositorio _clienteRepositorio;

        public ClienteController(ClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        public IActionResult Index()
        {
            return View(_clienteRepositorio.TodosClientes());
        }

        public IActionResult CadastrarCliente()
        {
            CarregarViewBags();
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarCliente(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _clienteRepositorio.Cadastrar(cliente);
                return RedirectToAction(nameof(Index));
            }

            CarregarViewBags();
            return View(cliente);
        }

        public IActionResult EditarCliente(int id)
        {
            var cliente = _clienteRepositorio.ObterCliente(id);

            if (cliente == null)
            {
                return NotFound();
            }

            CarregarViewBags();
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarCliente(int id, Cliente cliente)
        {
            if (id != cliente.Id_Cli)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (_clienteRepositorio.Atualizar(cliente))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao Editar.");
                    CarregarViewBags();
                    return View(cliente);
                }
            }

            CarregarViewBags();
            return View(cliente);
        }

        public IActionResult ExcluirCliente(int id)
        {
            _clienteRepositorio.Excluir(id);
            return RedirectToAction(nameof(Index));
        }

        private void CarregarViewBags()
        {
            var estados = _clienteRepositorio.Estados() ?? new List<Estado>();
            ViewBag.Estados = new SelectList(estados, "UF_Estado", "Nome_Estado");

            var cidades = _clienteRepositorio.Cidades() ?? new List<Cidade>();
            ViewBag.Cidades = new SelectList(cidades, "Cidade_Nome", "Cidade_Nome");
        }
    }
}