using Microsoft.AspNetCore.Mvc;
using CodeTrip.Models;
using CodeTrip.Repositorio;
using MySqlX.XDevAPI;
using MySql.Data.MySqlClient;
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
            var estados = _clienteRepositorio.Estados() ?? new List<Estado>();
            ViewBag.Estados = estados
                .Select(e => new SelectListItem
                {
                    Value = e.UF_Estado,
                    Text = $"{e.UF_Estado} - {e.Nome_Estado}"
                })
                .ToList();

            var cidades = _clienteRepositorio.Cidades() ?? new List<Cidade>();
            ViewBag.Cidades = cidades
                    .Select(c => new SelectListItem
                    {
                        Value = c.Cidade_Nome,
                        Text = $"{c.Cidade_Nome} / {c.UF_Estado}" 
                    })
                .ToList();

            return View();
        }

        [HttpPost]
        public IActionResult CadastrarCliente(Cliente cliente)
        {

            _clienteRepositorio.Cadastrar(cliente);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditarCliente(int id)
        {
            var estados = _clienteRepositorio.Estados() ?? new List<Estado>();
            ViewBag.Estados = new SelectList(estados, "UF_Estado", "UF_Estado");
            var cidades = _clienteRepositorio.Cidades() ?? new List<Cidade>();
            ViewBag.Cidades = new SelectList(cidades, "Cidade_Nome", "Cidade_Nome");


            var cliente = _clienteRepositorio.ObterCliente(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarCliente(int id, [Bind("Id_Cli, Nome_Cli, Email_Cli, Data_Nasc_Cli, CPF_Cli, Telefone_Cli, Logradouro_Cli, Numero_Cli, Bairro_Cli, Complemento_Cli, Cidade_Nome, UF_Estado")] Cliente cliente)
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
                    return View(cliente);
                }
            }
            return View(cliente);
        }

        public IActionResult ExcluirCliente(int id)
        {
            _clienteRepositorio.Excluir(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
