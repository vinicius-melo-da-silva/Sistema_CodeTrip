using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using CodeTrip.Models;
using CodeTrip.Repositorio;

namespace CodeTrip.Controllers
{
    public class PedidoController : Controller
    {
        private readonly PedidoRepositorio _pedidoRepositorio;

        public PedidoController(PedidoRepositorio pedidoRepositorio)
        {
            _pedidoRepositorio = pedidoRepositorio;
        }

        public IActionResult Index()
        {
            return View(_pedidoRepositorio.TodosPedidos());
        }

        public IActionResult CadastrarPedido()
        {
            var usuarios = _pedidoRepositorio.Usuarios() ?? new List<Usuario>();
            ViewBag.Usuarios = usuarios.Select(u => new SelectListItem { Value = u.Id_Usuario.ToString(), Text = $"{u.Id_Usuario} - {u.Nome_Usuario}" }).ToList();

            var clientes = _pedidoRepositorio.Clientes() ?? new List<Cliente>();
            ViewBag.Clientes = clientes.Select(c => new SelectListItem { Value = c.CPF_Cli, Text = $"{c.Nome_Cli} - {c.CPF_Cli}" }).ToList();

            var transportes = _pedidoRepositorio.Transportes() ?? new List<Transporte>();
            ViewBag.Transportes = transportes.Select(t => new SelectListItem { Value = t.Id_Transp.ToString(), Text = $"{t.Id_Transp} - {t.Tipo_Transp} / {t.UF_Estado}" }).ToList();

            var end_transportes = _pedidoRepositorio.End_Transportes() ?? new List<End_Transporte>();
            ViewBag.End_Transportes = end_transportes.Select(e => new SelectListItem { Value = e.Id_End_Transporte.ToString(), Text = $"{e.Id_End_Transporte} - {e.Logradouro_End_Transporte}, {e.Numero_End_Transporte} - {e.Cidade_Nome}/{e.UF_Estado}" }).ToList();

            var hospedagens = _pedidoRepositorio.Hospedagens() ?? new List<Hospedagem>();
            ViewBag.Hospedagens = hospedagens.Select(h => new SelectListItem { Value = h.Id_Hospedagem.ToString(), Text = $"{h.Id_Hospedagem} - {h.Nome_Hospedagem}" }).ToList();

            var pagamentos = _pedidoRepositorio.Pagamentos() ?? new List<Pagamento>();
            ViewBag.Pagamentos = pagamentos.Select(p => new SelectListItem { Value = p.Id_Pagamento.ToString(), Text = $"{p.Id_Pagamento} - {p.Desc_Pagamento}" }).ToList();

            var origens = _pedidoRepositorio.Origens() ?? new List<OrigemViagem>();
            ViewBag.Origens = origens.Select(o => new SelectListItem { Value = o.Id_Origem.ToString(), Text = $"{o.Id_Origem} - {o.Cidade_Nome}" }).ToList();

            var destinos = _pedidoRepositorio.Destinos() ?? new List<DestinoViagem>();
            ViewBag.Destinos = destinos.Select(d => new SelectListItem { Value = d.Id_Destino.ToString(), Text = $"{d.Id_Destino} - {d.Cidade_Nome}" }).ToList();

            var passeios = _pedidoRepositorio.Passeios() ?? new List<Passeio>();
            ViewBag.Passeios = passeios.Select(p => new SelectListItem { Value = p.Id_Passeio.ToString(), Text = $"{p.Id_Passeio} - {p.Desc_Passeio}" }).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult CadastrarPedido(Pedido pedido)
        {
            _pedidoRepositorio.Cadastrar(pedido);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditarPedido(int id)
        {
            var pedido = _pedidoRepositorio.ObterPedido(id);
            if (pedido == null) return NotFound();

            var usuarios = _pedidoRepositorio.Usuarios() ?? new List<Usuario>();
            ViewBag.Usuarios = usuarios.Select(u => new SelectListItem { Value = u.Id_Usuario.ToString(), Text = $"{u.Id_Usuario} - {u.Nome_Usuario}" }).ToList();

            var clientes = _pedidoRepositorio.Clientes() ?? new List<Cliente>();
            ViewBag.Clientes = clientes.Select(c => new SelectListItem { Value = c.CPF_Cli, Text = $"{c.Nome_Cli} - {c.CPF_Cli}" }).ToList();

            var transportes = _pedidoRepositorio.Transportes() ?? new List<Transporte>();
            ViewBag.Transportes = transportes.Select(t => new SelectListItem { Value = t.Id_Transp.ToString(), Text = $"{t.Id_Transp} - {t.Tipo_Transp} / {t.UF_Estado}" }).ToList();

            var end_transportes = _pedidoRepositorio.End_Transportes() ?? new List<End_Transporte>();
            ViewBag.End_Transportes = end_transportes.Select(e => new SelectListItem { Value = e.Id_End_Transporte.ToString(), Text = $"{e.Id_End_Transporte} - {e.Logradouro_End_Transporte}, {e.Numero_End_Transporte} - {e.Cidade_Nome}/{e.UF_Estado}" }).ToList();

            var hospedagens = _pedidoRepositorio.Hospedagens() ?? new List<Hospedagem>();
            ViewBag.Hospedagens = hospedagens.Select(h => new SelectListItem { Value = h.Id_Hospedagem.ToString(), Text = $"{h.Id_Hospedagem} - {h.Nome_Hospedagem}" }).ToList();

            var pagamentos = _pedidoRepositorio.Pagamentos() ?? new List<Pagamento>();
            ViewBag.Pagamentos = pagamentos.Select(p => new SelectListItem { Value = p.Id_Pagamento.ToString(), Text = $"{p.Id_Pagamento} - {p.Desc_Pagamento}" }).ToList();

            var origens = _pedidoRepositorio.Origens() ?? new List<OrigemViagem>();
            ViewBag.Origens = origens.Select(o => new SelectListItem { Value = o.Id_Origem.ToString(), Text = $"{o.Id_Origem} - {o.Cidade_Nome}" }).ToList();

            var destinos = _pedidoRepositorio.Destinos() ?? new List<DestinoViagem>();
            ViewBag.Destinos = destinos.Select(d => new SelectListItem { Value = d.Id_Destino.ToString(), Text = $"{d.Id_Destino} - {d.Cidade_Nome}" }).ToList();

            var passeios = _pedidoRepositorio.Passeios() ?? new List<Passeio>();
            ViewBag.Passeios = passeios.Select(p => new SelectListItem { Value = p.Id_Passeio.ToString(), Text = $"{p.Id_Passeio} - {p.Desc_Passeio}" }).ToList();

            return View(pedido);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarPedido(int id, [Bind("Id_Pedido,Id_Usuario,CPF_Cli,Id_Origem,Id_Destino,Data_Inicio,Data_Fim,Id_Transp,Id_End_Transporte,Id_Hospedagem,Id_Pagamento,Id_Passeio,Ativo")] Pedido pedido)
        {
            if (id != pedido.Id_Pedido) return BadRequest();
            if (ModelState.IsValid)
            {
                if (_pedidoRepositorio.Atualizar(pedido)) return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", "Ocorreu um erro ao editar.");
            }
            return View(pedido);
        }

        public IActionResult ExcluirPedido(int id)
        {
            _pedidoRepositorio.Excluir(id);
            return RedirectToAction(nameof(Index));
        }
    }
}