using Microsoft.AspNetCore.Mvc;
using CodeTrip.Models;
using CodeTrip.Repositorio;
using MySqlX.XDevAPI;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeTrip.Controllers
{
    public class HospedagemController : Controller
    {
        private readonly HospedagemRepositorio _hospedagemRepositorio;



        public HospedagemController(HospedagemRepositorio hospedagemRepositorio)
        {
            _hospedagemRepositorio = hospedagemRepositorio;
        }

        public IActionResult Index()
        {
            return View(_hospedagemRepositorio.TodasHospedagens());
        }

        public IActionResult CadastrarHospedagem()
        {
            var estados = _hospedagemRepositorio.Estados() ?? new List<Estado>();
            ViewBag.Estados = new SelectList(estados, "UF_Estado", "UF_Estado");
            var cidades = _hospedagemRepositorio.Cidades() ?? new List<Cidade>();
            ViewBag.Cidades = new SelectList(cidades, "Cidade_Nome", "Cidade_Nome");

            return View();
        }

        [HttpPost]
        public IActionResult CadastrarHospedagem(Hospedagem hospedagem)
        {

            _hospedagemRepositorio.Cadastrar(hospedagem);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditarHospedagem(int id)
        {
            var estados = _hospedagemRepositorio.Estados() ?? new List<Estado>();
            ViewBag.Estados = new SelectList(estados, "UF_Estado", "UF_Estado");
            var cidades = _hospedagemRepositorio.Cidades() ?? new List<Cidade>();
            ViewBag.Cidades = new SelectList(cidades, "Cidade_Nome", "Cidade_Nome");

            var hospedagem = _hospedagemRepositorio.ObterHospedagem(id);

            if (hospedagem == null)
            {
                return NotFound();
            }

            return View(hospedagem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarHospedagem(int id, [Bind("Id_Hospedagem, Nome_Hospedagem, Id_Tipo_Hospedagem, Id_Pensao, Logradouro_Endereco_Hospedagem, Numero_Endereco_Hospedagem, Bairro_Endereco_Hospedagem, Complemento_Endereco_Hospedagem, Cidade_Nome, UF_Estado")] Hospedagem hospedagem)
        {
            if (id != hospedagem.Id_Hospedagem)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (_hospedagemRepositorio.Atualizar(hospedagem))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao Editar.");
                    return View(hospedagem);
                }
            }
            return View(hospedagem);
        }

        public IActionResult ExcluirHospedagem(int id)
        {
            _hospedagemRepositorio.Excluir(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
