using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class MonitorController : ControllerBase {

    private readonly List<string> computadores = new List<string> { "192.168.183.64", "192.168.201.01", "192.168.201.90" };

    [HttpGet]
    public async Task<ActionResult<IEnumerable<string>>> Get() {

        var resultados = new List<string>();
        var ping = new Ping();

        foreach (var ip in computadores) {
            try {
                var resposta = await ping.SendPingAsync(ip);
                resultados.Add($"{ip}: {(resposta.Status == IPStatus.Success ? "Online" : "Offline")}");
            } catch (PingException ex) {
                resultados.Add($"{ip}: Erro - {ex.Message}");
            } catch (Exception ex) {
                resultados.Add($"{ip}: Erro - {ex.Message}");
            }
        }

        return resultados;
    }
}
