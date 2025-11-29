using Microsoft.AspNetCore.Mvc;
using CodeTrip.Models;
using CodeTrip.Repositorio;
using System.Text;

namespace CodeTrip.Controllers
{
    public class IndicadoresController : Controller
    {
        private readonly IndicadoresRepositorio _indicadoresRepositorio;

        public IndicadoresController(IndicadoresRepositorio indicadoresRepositorio)
        {
            _indicadoresRepositorio = indicadoresRepositorio;
        }

        public IActionResult Index()
        {
            var indicadores = _indicadoresRepositorio.ObterTodosIndicadores();
            return View(indicadores);
        }

        [HttpGet]
        public JsonResult ObterDadosGrafico()
        {
            var viagensPorMes = _indicadoresRepositorio.ObterViagensPorMes();
            return Json(viagensPorMes);
        }

        [HttpGet]
        public JsonResult ObterDadosGraficoDiario()
        {
            var viagensPorDia = _indicadoresRepositorio.ObterViagensPorDia(30);
            return Json(viagensPorDia);
        }

        [HttpGet]
        public JsonResult ObterRankingDestinos()
        {
            var destinos = _indicadoresRepositorio.ObterDestinosMaisVendidos(5);
            return Json(destinos);
        }

        [HttpGet]
        public JsonResult ObterRankingPasseios()
        {
            var passeios = _indicadoresRepositorio.ObterPasseiosMaisVendidos(5);
            return Json(passeios);
        }

        [HttpGet]
        public JsonResult ObterResumo()
        {
            var resumo = new
            {
                ReceitaTotal = _indicadoresRepositorio.ObterReceitaTotal(),
                ClientesAtivos = _indicadoresRepositorio.ObterClientesAtivos(),
                ClientesInativos = _indicadoresRepositorio.ObterClientesInativos()
            };
            return Json(resumo);
        }

        // ================= EXPORTAÇÃO PDF (NOVO) =================
        public IActionResult ExportarPDF()
        {
            var dados = _indicadoresRepositorio.ObterDadosParaExportacao();

            // Criar conteúdo HTML simples para o PDF
            var htmlContent = new StringBuilder();
            htmlContent.Append(@"
                <html>
                <head>
                    <style>
                        body { font-family: Arial, sans-serif; }
                        .header { text-align: center; color: #2c3e50; }
                        .section { margin: 20px 0; }
                        .table { width: 100%; border-collapse: collapse; }
                        .table th, .table td { border: 1px solid #ddd; padding: 8px; text-align: left; }
                        .table th { background-color: #f2f2f2; }
                        .card { border: 1px solid #ddd; padding: 15px; margin: 10px 0; border-radius: 5px; }
                    </style>
                </head>
                <body>
                    <div class='header'>
                        <h1>Relatório CodeTrip</h1>
                        <p>Gerado em: " + dados.DataGeracao.ToString("dd/MM/yyyy HH:mm") + @"</p>
                    </div>");

            // Resumo
            htmlContent.Append(@"
                <div class='section'>
                    <h2>Resumo Geral</h2>
                    <div class='card'>
                        <strong>Receita Total:</strong> R$ " + dados.ReceitaTotal.ToString("N2") + @"<br>
                        <strong>Clientes Ativos:</strong> " + dados.TotalClientesAtivos + @"<br>
                        <strong>Clientes Inativos:</strong> " + dados.TotalClientesInativos + @"
                    </div>
                </div>");

            // Viagens por Mês
            htmlContent.Append(@"
                <div class='section'>
                    <h2>Viagens por Mês</h2>
                    <table class='table'>
                        <tr><th>Mês</th><th>Quantidade</th></tr>");
            foreach (var item in dados.ViagensMensais)
            {
                htmlContent.Append($"<tr><td>{item.Mes}</td><td>{item.Quantidade}</td></tr>");
            }
            htmlContent.Append("</table></div>");

            // Top Destinos
            htmlContent.Append(@"
                <div class='section'>
                    <h2>Top Destinos</h2>
                    <table class='table'>
                        <tr><th>Destino</th><th>Vendas</th></tr>");
            foreach (var item in dados.TopDestinos)
            {
                htmlContent.Append($"<tr><td>{item.Destino}</td><td>{item.Quantidade}</td></tr>");
            }
            htmlContent.Append("</table></div>");

            htmlContent.Append("</body></html>");

            // Retornar como arquivo (em produção, usar biblioteca como iTextSharp para PDF real)
            var bytes = Encoding.UTF8.GetBytes(htmlContent.ToString());
            return File(bytes, "text/html", $"relatorio-codetrip-{DateTime.Now:yyyyMMdd}.html");
        }

        // ================= EXPORTAÇÃO EXCEL (NOVO) =================
        public IActionResult ExportarExcel()
        {
            var dados = _indicadoresRepositorio.ObterDadosParaExportacao();

            // Criar conteúdo CSV (simulando Excel)
            var csvContent = new StringBuilder();

            // Cabeçalho
            csvContent.AppendLine("Relatório CodeTrip - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            csvContent.AppendLine();

            // Resumo
            csvContent.AppendLine("RESUMO GERAL");
            csvContent.AppendLine($"Receita Total;R$ {dados.ReceitaTotal:N2}");
            csvContent.AppendLine($"Clientes Ativos;{dados.TotalClientesAtivos}");
            csvContent.AppendLine($"Clientes Inativos;{dados.TotalClientesInativos}");
            csvContent.AppendLine();

            // Viagens por Mês
            csvContent.AppendLine("VIAGENS POR MÊS");
            csvContent.AppendLine("Mês;Quantidade");
            foreach (var item in dados.ViagensMensais)
            {
                csvContent.AppendLine($"{item.Mes};{item.Quantidade}");
            }
            csvContent.AppendLine();

            // Top Destinos
            csvContent.AppendLine("TOP DESTINOS");
            csvContent.AppendLine("Destino;Vendas");
            foreach (var item in dados.TopDestinos)
            {
                csvContent.AppendLine($"{item.Destino};{item.Quantidade}");
            }
            csvContent.AppendLine();

            // Top Passeios
            csvContent.AppendLine("TOP PASSEIOS");
            csvContent.AppendLine("Passeio;Vendas");
            foreach (var item in dados.TopPasseios)
            {
                csvContent.AppendLine($"{item.Passeio};{item.Quantidade}");
            }

            var bytes = Encoding.UTF8.GetBytes(csvContent.ToString());
            return File(bytes, "text/csv", $"relatorio-codetrip-{DateTime.Now:yyyyMMdd}.csv");
        }
    }
}