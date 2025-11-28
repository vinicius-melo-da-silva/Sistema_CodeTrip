using Microsoft.AspNetCore.Mvc;
using CodeTrip.Models;
using CodeTrip.Repositorio;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            CarregarViewBags(null);
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

            CarregarViewBags(pedido);
            return View(pedido);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarPedido(
            int id,
            [Bind("Id_Pedido,Id_Usuario,CPF_Cli,Data_Inicio,Data_Fim,Id_Transp,Id_End_Transporte,Id_Hospedagem,Id_Pagamento,Id_Origem,Id_Destino,Id_Passeio")]
            Pedido pedido)
        {
            if (id != pedido.Id_Pedido) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    if (_pedidoRepositorio.Atualizar(pedido))
                        return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao editar.");
                }
            }

            CarregarViewBags(pedido);
            return View(pedido);
        }

        public IActionResult ExcluirPedido(int id)
        {
            _pedidoRepositorio.Excluir(id);
            return RedirectToAction(nameof(Index));
        }

        // ================= MÉTODO AUXILIAR PARA VIEWBAGS =================
        private void CarregarViewBags(Pedido? pedido)
        {
            // Usuários
            ViewBag.Usuarios = (_pedidoRepositorio.Usuarios() ?? new List<Usuario>())
                .Select(u => new SelectListItem
                {
                    Value = u.Id_Usuario.ToString(),
                    Text = $"{u.Id_Usuario} - {u.Nome_Usuario}",
                    Selected = pedido?.Id_Usuario == u.Id_Usuario
                }).ToList();

            // Clientes
            ViewBag.Clientes = (_pedidoRepositorio.Clientes() ?? new List<Cliente>())
                .Select(c => new SelectListItem
                {
                    Value = c.CPF_Cli,
                    Text = $"{c.Nome_Cli} - {c.CPF_Cli}",
                    Selected = pedido?.CPF_Cli == c.CPF_Cli
                }).ToList();

            // Transportes
            ViewBag.Transportes = (_pedidoRepositorio.Transportes() ?? new List<Transporte>())
                .Select(t => new SelectListItem
                {
                    Value = t.Id_Transp.ToString(),
                    Text = $"{t.Id_Transp} - {t.Tipo_Transp} / {t.UF_Estado}",
                    Selected = pedido?.Id_Transp == t.Id_Transp
                }).ToList();

            // Endereços Transporte
            ViewBag.End_Transportes = (_pedidoRepositorio.End_Transportes() ?? new List<End_Transporte>())
                .Select(e => new SelectListItem
                {
                    Value = e.Id_End_Transporte.ToString(),
                    Text = $"{e.Id_End_Transporte} - {e.Logradouro_End_Transporte}, {e.Numero_End_Transporte} - {e.Cidade_Nome}/{e.UF_Estado}",
                    Selected = pedido?.Id_End_Transporte == e.Id_End_Transporte
                }).ToList();

            // Hospedagens
            ViewBag.Hospedagens = (_pedidoRepositorio.Hospedagens() ?? new List<Hospedagem>())
                .Select(h => new SelectListItem
                {
                    Value = h.Id_Hospedagem.ToString(),
                    Text = $"{h.Id_Hospedagem} - {h.Nome_Hospedagem}",
                    Selected = pedido?.Id_Hospedagem == h.Id_Hospedagem
                }).ToList();

            // Pagamentos
            ViewBag.Pagamentos = (_pedidoRepositorio.Pagamentos() ?? new List<Pagamento>())
                .Select(p => new SelectListItem
                {
                    Value = p.Id_Pagamento.ToString(),
                    Text = $"{p.Id_Pagamento} - {p.Desc_Pagamento}",
                    Selected = pedido?.Id_Pagamento == p.Id_Pagamento
                }).ToList();

            // Origens
            ViewBag.Origens = (_pedidoRepositorio.Origens() ?? new List<OrigemViagem>())
                .Select(o => new SelectListItem
                {
                    Value = o.Id_Origem.ToString(),
                    Text = $"{o.Id_Origem} - {o.Cidade_Nome}",
                    Selected = pedido?.Id_Origem == o.Id_Origem
                }).ToList();

            // Destinos
            ViewBag.Destinos = (_pedidoRepositorio.Destinos() ?? new List<DestinoViagem>())
                .Select(d => new SelectListItem
                {
                    Value = d.Id_Destino.ToString(),
                    Text = $"{d.Id_Destino} - {d.Cidade_Nome}",
                    Selected = pedido?.Id_Destino == d.Id_Destino
                }).ToList();

            // Passeios
            ViewBag.Passeios = (_pedidoRepositorio.Passeios() ?? new List<Passeio>())
                .Select(p => new SelectListItem
                {
                    Value = p.Id_Passeio.ToString(),
                    Text = $"{p.Id_Passeio} - {p.Nome_Passeio}",
                    Selected = pedido?.Id_Passeio == p.Id_Passeio
                }).ToList();
        }
    }
}
