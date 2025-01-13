using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class MonitorController : ControllerBase {
    private readonly List<string> computadores;

    public MonitorController(IOptions<MonitorOptions> options) {
        computadores = options.Value.Computadores ?? new List<string>();
        Console.WriteLine("MonitorController: Computadores inicializados.");
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<string>>> Get() {
        var resultados = new List<string>();
        Ping ping = new Ping();

        Console.WriteLine("Número de IPs lidos: " + computadores.Count);
        foreach (var ip in computadores) {
            Console.WriteLine("Lendo IP: " + ip);
        }

        foreach (var ip in computadores) {
            if (IsValidIP(ip)) {
                try {
                    var resposta = await ping.SendPingAsync(ip);
                    Console.WriteLine($"Ping para {ip}: {resposta.Status}");
                    resultados.Add($"{ip}: {(resposta.Status == IPStatus.Success ? "Online" : "Offline")}");
                } catch (PingException ex) {
                    Console.WriteLine($"Erro no ping para {ip}: {ex.Message}");
                    resultados.Add($"{ip}: Erro - {ex.Message}");
                } catch (Exception ex) {
                    Console.WriteLine($"Erro geral para {ip}: {ex.Message}");
                    resultados.Add($"{ip}: Erro - {ex.Message}");
                }
            } else {
                Console.WriteLine($"IP Inválido: {ip}");
                resultados.Add($"{ip}: IP Inválido");
            }
        }

        return resultados;
    }

    private bool IsValidIP(string ip) {
        return IPAddress.TryParse(ip, out _);
    }
}
