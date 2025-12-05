using CodeTrip.Filters;
using CodeTrip.Models;
using CodeTrip.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace CodeTrip.Controllers
{
    [SessionAuthorize(RoleAnyOf = "Comum,Admin,Colaborador")]
    public class PedidoController : Controller
    {
        private readonly PedidoRepositorio _pedidoRepositorio;

        public PedidoController(PedidoRepositorio pedidoRepositorio)
        {
            _pedidoRepositorio = pedidoRepositorio;
        }

        public IActionResult Index()
        {
            var usuarioLogado = HttpContext.Session.Get<Usuario>("usuarioLogado");

            if (usuarioLogado == null)
            {
                return RedirectToAction("MenuSistema", "Home");
            }

            if (usuarioLogado.Role == "Comum")
            {
                var todosClientes = _pedidoRepositorio.Clientes();
                var cliente = todosClientes?.FirstOrDefault(c => c.Email_Cli == usuarioLogado.Email_Usuario);

                if (cliente == null)
                {
                    return View(new List<Pedido>());
                }

                var todosPedidos = _pedidoRepositorio.TodosPedidos();
                var pedidosFiltrados = todosPedidos?.Where(p => p.CPF_Cli == cliente.CPF_Cli).ToList();
                return View(pedidosFiltrados ?? new List<Pedido>());
            }
            else
            {
                return View(_pedidoRepositorio.TodosPedidos());
            }
        }

        public IActionResult CadastrarPedido()
        {
            CarregarViewBags();
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarPedido(Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _pedidoRepositorio.Cadastrar(pedido);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Erro ao cadastrar pedido: {ex.Message}");
                }
            }
            CarregarViewBags();
            return View(pedido);
        }

        public IActionResult EditarPedido(int id)
        {
            var pedido = _pedidoRepositorio.ObterPedido(id);

            if (pedido == null)
            {
                return NotFound();
            }

            CarregarViewBags();
            return View(pedido);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarPedido(int id, [Bind("Id_Pedido,Id_Usuario,CPF_Cli,Id_Origem,Id_Destino,Data_Inicio,Data_Fim,Id_Transp,Id_End_Transporte,Id_Hospedagem,Id_Pagamento,Id_Passeio,Ativo")] Pedido pedido)
        {
            if (id != pedido.Id_Pedido)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (_pedidoRepositorio.Atualizar(pedido))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao Editar.");
                    CarregarViewBags();
                    return View(pedido);
                }
            }
            CarregarViewBags();
            return View(pedido);
        }

        public IActionResult ExcluirPedido(int id)
        {
            _pedidoRepositorio.Excluir(id);
            return RedirectToAction(nameof(Index));
        }

        private void CarregarViewBags()
        {
            ViewBag.Usuarios = _pedidoRepositorio.Usuarios()?
                .Select(u => new SelectListItem
                {
                    Value = u.Id_Usuario.ToString(),
                    Text = $"{u.Id_Usuario} - {u.Nome_Usuario}"
                })
            .ToList() ?? new List<SelectListItem>();

            ViewBag.Clientes = _pedidoRepositorio.Clientes()?
                .Select(c => new SelectListItem
                {
                    Value = c.CPF_Cli,
                    Text = $"{c.Nome_Cli} - {c.CPF_Cli}"
                })
            .ToList() ?? new List<SelectListItem>();

            ViewBag.Transportes = _pedidoRepositorio.Transportes()?
                .Select(t => new SelectListItem
                {
                    Value = t.Id_Transp.ToString(),
                    Text = $"{t.Id_Transp} - {t.Tipo_Transp} / {t.UF_Estado}"
                })
            .ToList() ?? new List<SelectListItem>();

            ViewBag.End_Transportes = _pedidoRepositorio.End_Transportes()?
                .Select(e => new SelectListItem
                {
                    Value = e.Id_End_Transporte.ToString(),
                    Text = $"{e.Id_End_Transporte} - {e.Logradouro_End_Transporte}, {e.Numero_End_Transporte} - {e.Cidade_Nome}/{e.UF_Estado} "
                })
            .ToList() ?? new List<SelectListItem>();

            ViewBag.Hospedagens = _pedidoRepositorio.Hospedagens()?
                .Select(h => new SelectListItem
                {
                    Value = h.Id_Hospedagem.ToString(),
                    Text = $"{h.Id_Hospedagem} - {h.Nome_Hospedagem}"
                })
            .ToList() ?? new List<SelectListItem>();

            ViewBag.Pagamentos = _pedidoRepositorio.Pagamentos()?
                .Select(p => new SelectListItem
                {
                    Value = p.Id_Pagamento.ToString(),
                    Text = $"{p.Id_Pagamento} - {p.Desc_Pagamento}"
                })
            .ToList() ?? new List<SelectListItem>();

            ViewBag.Origens = _pedidoRepositorio.OrigensViagem()?
                .Select(o => new SelectListItem
                {
                    Value = o.Id_Origem.ToString(),
                    Text = $"{o.Id_Origem} - {o.Cidade_Nome}/{o.UF_Estado}"
                })
            .ToList() ?? new List<SelectListItem>();

            ViewBag.Destinos = _pedidoRepositorio.DestinosViagem()?
                .Select(d => new SelectListItem
                {
                    Value = d.Id_Destino.ToString(),
                    Text = $"{d.Id_Destino} - {d.Desc_Destino} ({d.Cidade_Nome}/{d.UF_Estado})"
                })
            .ToList() ?? new List<SelectListItem>();

            ViewBag.Passeios = _pedidoRepositorio.Passeios()?
                .Select(p => new SelectListItem
                {
                    Value = p.Id_Passeio.ToString(),
                    Text = $"{p.Id_Passeio} - {p.Nome_Passeio} ({p.Cidade_Nome}/{p.UF_Estado})"
                })
            .ToList() ?? new List<SelectListItem>();
        }
    }
}