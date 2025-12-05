using Microsoft.AspNetCore.Mvc;
using CodeTrip.Models;
using CodeTrip.Repositorio;
using Microsoft.AspNetCore.Mvc.Rendering;
using CodeTrip.Filters;

namespace CodeTrip.Controllers
{
    [SessionAuthorize(RoleAnyOf = "Admin,Colaborador")]
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
            CarregarViewBags();
            return View();
        }

        [HttpPost]

        public IActionResult CadastrarHospedagem(Hospedagem hospedagem)
        {
            if (ModelState.IsValid)
            {
                _hospedagemRepositorio.Cadastrar(hospedagem);
                return RedirectToAction(nameof(Index));
            }

            CarregarViewBags();
            return View(hospedagem);
        }

        public IActionResult EditarHospedagem(int id)
        {
            var hospedagem = _hospedagemRepositorio.ObterHospedagem(id);

            if (hospedagem == null)
            {
                return NotFound();
            }

            CarregarViewBags();
            return View(hospedagem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarHospedagem(int id, Hospedagem hospedagem)
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
                    CarregarViewBags();
                    return View(hospedagem);
                }
            }

            CarregarViewBags();
            return View(hospedagem);
        }

        public IActionResult ExcluirHospedagem(int id)
        {
            _hospedagemRepositorio.Excluir(id);
            return RedirectToAction(nameof(Index));
        }

        private void CarregarViewBags()
        {
            var estados = _hospedagemRepositorio.Estados() ?? new List<Estado>();
            ViewBag.Estados = new SelectList(estados, "UF_Estado", "Nome_Estado");

            var cidades = _hospedagemRepositorio.Cidades() ?? new List<Cidade>();
            ViewBag.Cidades = new SelectList(cidades, "Cidade_Nome", "Cidade_Nome");

            var tiposHospedagem = _hospedagemRepositorio.TiposHospedagem() ?? new List<Tipo_Hospedagem>();
            ViewBag.TiposHospedagem = new SelectList(tiposHospedagem, "Id_Tipo_Hospedagem", "Desc_Hospedagem");

            var tiposPensao = _hospedagemRepositorio.TiposPensao() ?? new List<Tipo_Pensao>();
            ViewBag.TiposPensao = new SelectList(tiposPensao, "Id_Pensao", "Desc_Pensao");
        }
    }
}