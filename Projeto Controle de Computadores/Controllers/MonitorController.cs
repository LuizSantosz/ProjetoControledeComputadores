using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Projeto_Controle_de_Computadores.Models;
using Projeto_Controle_de_Computadores.Services;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class MonitorController : ControllerBase {
    private readonly List<string> computadores;
    private readonly GrupoService grupoService;
    private readonly ComputadorService computadorService;

    public MonitorController(IOptions<MonitorOptions> options, GrupoService grupoService, ComputadorService computadorService) {
        computadores = options.Value.Computadores ?? new List<string>();
        this.grupoService = grupoService;
        this.computadorService = computadorService;
        Console.WriteLine("MonitorController: Computadores inicializados.");
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<string>>> Get() {
        var resultados = new List<string>();
        Ping ping = new Ping();

        foreach (var ip in computadores) {
            if (IsValidIP(ip)) {
                try {
                    var resposta = await ping.SendPingAsync(ip);
                    resultados.Add($"{ip}: {(resposta.Status == IPStatus.Success ? "Online" : "Offline")}");
                } catch (PingException ex) {
                    resultados.Add($"{ip}: Erro - {ex.Message}");
                } catch (Exception ex) {
                    resultados.Add($"{ip}: Erro - {ex.Message}");
                }
            } else {
                resultados.Add($"{ip}: IP Inválido");
            }
        }

        return Ok(resultados);
    }

    private bool IsValidIP(string ip) {
        return IPAddress.TryParse(ip, out _);
    }
}
