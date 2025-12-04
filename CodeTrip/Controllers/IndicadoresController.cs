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
            var dashboard = _indicadoresRepositorio.ObterTodosIndicadores();
            return View(dashboard);
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
        public JsonResult ObterVendasPorOperador()
        {
            var vendas = _indicadoresRepositorio.ObterVendasPorOperador(5);
            return Json(vendas);
        }

        [HttpGet]
        public JsonResult ObterResumo()
        {
            var resumo = new
            {
                ReceitaTotal = _indicadoresRepositorio.ObterReceitaTotal(),
                ClientesAtivos = _indicadoresRepositorio.ObterClientesAtivos(),
                ClientesInativos = _indicadoresRepositorio.ObterClientesInativos(),
                TotalViagensMesAtual = _indicadoresRepositorio.ObterTotalViagensMesAtual(),
                TotalViagensHoje = _indicadoresRepositorio.ObterTotalViagensHoje()
            };
            return Json(resumo);
        }

        // ================= EXPORTAÇÃO PDF =================
        public IActionResult ExportarPDF()
        {
            var dados = _indicadoresRepositorio.ObterDadosParaExportacao();

            var htmlContent = new StringBuilder();
            htmlContent.Append(@"
                <html>
                <head>
                    <style>
                        body { font-family: Arial, sans-serif; margin: 20px; }
                        .header { text-align: center; color: #2c3e50; border-bottom: 2px solid #3498db; padding-bottom: 20px; margin-bottom: 30px; }
                        .section { margin: 20px 0; }
                        .table { width: 100%; border-collapse: collapse; margin: 10px 0; }
                        .table th, .table td { border: 1px solid #ddd; padding: 10px; text-align: left; }
                        .table th { background-color: #f2f2f2; color: #333; }
                        .card { border: 1px solid #ddd; padding: 15px; margin: 10px 0; border-radius: 5px; background-color: #f9f9f9; }
                        .total { font-weight: bold; color: #2c3e50; }
                        .grafico-section { page-break-inside: avoid; }
                    </style>
                </head>
                <body>
                    <div class='header'>
                        <h1>Relatório CodeTrip</h1>
                        <h3>Dashboard de Indicadores</h3>
                        <p>Gerado em: " + dados.DataGeracao.ToString("dd/MM/yyyy HH:mm") + @"</p>
                    </div>");

            // Resumo
            htmlContent.Append(@"
                <div class='section'>
                    <h2>Resumo Geral</h2>
                    <div class='card'>
                        <strong>Receita Total:</strong> R$ " + dados.ReceitaTotal.ToString("N2") + @"<br>
                        <strong>Clientes Ativos:</strong> " + dados.TotalClientesAtivos + @"<br>
                        <strong>Clientes Inativos:</strong> " + dados.TotalClientesInativos + @"<br>
                        <strong>Total de Viagens (Mês Atual):</strong> " + dados.ViagensMensais.Where(v => v.Mes == DateTime.Now.ToString("yyyy-MM")).Select(v => v.Quantidade).FirstOrDefault() + @"
                    </div>
                </div>");

            // Vendas por Operador
            htmlContent.Append(@"
                <div class='section'>
                    <h2>Vendas por Operador (Top 10)</h2>
                    <table class='table'>
                        <tr><th>Operador</th><th>Tipo</th><th>Quantidade de Vendas</th><th>Valor Total</th></tr>");
            foreach (var item in dados.VendasOperadores)
            {
                htmlContent.Append($"<tr><td>{item.Operador}</td><td>{item.Role}</td><td>{item.QuantidadeVendas}</td><td>R$ {item.ValorTotal:N2}</td></tr>");
            }
            htmlContent.Append("</table></div>");

            // Viagens por Mês
            htmlContent.Append(@"
                <div class='section'>
                    <h2>Viagens por Mês (Últimos 12 meses)</h2>
                    <p class='total'>Total Geral: " + dados.ViagensMensais.Sum(v => v.Quantidade) + @" viagens</p>
                    <table class='table'>
                        <tr><th>Mês</th><th>Quantidade</th></tr>");
            foreach (var item in dados.ViagensMensais)
            {
                htmlContent.Append($"<tr><td>{item.Mes}</td><td>{item.Quantidade}</td></tr>");
            }
            htmlContent.Append("</table></div>");

            // Viagens por Dia
            htmlContent.Append(@"
                <div class='section'>
                    <h2>Viagens por Dia (Últimos 30 dias)</h2>
                    <p class='total'>Total no Período: " + dados.ViagensDiarias.Sum(v => v.Quantidade) + @" viagens</p>
                    <table class='table'>
                        <tr><th>Data</th><th>Quantidade</th></tr>");
            foreach (var item in dados.ViagensDiarias)
            {
                htmlContent.Append($"<tr><td>{item.Data}</td><td>{item.Quantidade}</td></tr>");
            }
            htmlContent.Append("</table></div>");

            // Top Destinos
            htmlContent.Append(@"
                <div class='section'>
                    <h2>Top 10 Destinos</h2>
                    <table class='table'>
                        <tr><th>Destino</th><th>Vendas</th></tr>");
            foreach (var item in dados.TopDestinos)
            {
                htmlContent.Append($"<tr><td>{item.Destino}</td><td>{item.Quantidade}</td></tr>");
            }
            htmlContent.Append("</table></div>");

            // Top Passeios
            htmlContent.Append(@"
                <div class='section'>
                    <h2>Top 10 Passeios</h2>
                    <table class='table'>
                        <tr><th>Passeio</th><th>Vendas</th></tr>");
            foreach (var item in dados.TopPasseios)
            {
                htmlContent.Append($"<tr><td>{item.Passeio}</td><td>{item.Quantidade}</td></tr>");
            }
            htmlContent.Append("</table></div>");

            htmlContent.Append("</body></html>");

            var bytes = Encoding.UTF8.GetBytes(htmlContent.ToString());
            return File(bytes, "text/html", $"relatorio-codetrip-{DateTime.Now:yyyyMMdd}.html");
        }

        // ================= EXPORTAÇÃO EXCEL =================
        public IActionResult ExportarExcel()
        {
            var dados = _indicadoresRepositorio.ObterDadosParaExportacao();

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

            // Vendas por Operador
            csvContent.AppendLine("VENDAS POR OPERADOR");
            csvContent.AppendLine("Operador;Tipo;Quantidade de Vendas;Valor Total");
            foreach (var item in dados.VendasOperadores)
            {
                csvContent.AppendLine($"{item.Operador};{item.Role};{item.QuantidadeVendas};R$ {item.ValorTotal:N2}");
            }
            csvContent.AppendLine();

            // Viagens por Mês
            csvContent.AppendLine("VIAGENS POR MÊS");
            csvContent.AppendLine($"Total Geral;{dados.ViagensMensais.Sum(v => v.Quantidade)}");
            csvContent.AppendLine("Mês;Quantidade");
            foreach (var item in dados.ViagensMensais)
            {
                csvContent.AppendLine($"{item.Mes};{item.Quantidade}");
            }
            csvContent.AppendLine();

            // Viagens por Dia
            csvContent.AppendLine("VIAGENS POR DIA (Últimos 30 dias)");
            csvContent.AppendLine($"Total no Período;{dados.ViagensDiarias.Sum(v => v.Quantidade)}");
            csvContent.AppendLine("Data;Quantidade");
            foreach (var item in dados.ViagensDiarias)
            {
                csvContent.AppendLine($"{item.Data};{item.Quantidade}");
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