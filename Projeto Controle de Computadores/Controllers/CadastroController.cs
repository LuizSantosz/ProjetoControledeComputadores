using Microsoft.AspNetCore.Mvc;
using Projeto_Controle_de_Computadores.Models;
using Projeto_Controle_de_Computadores.Services;

namespace Projeto_Controle_de_Computadores.Controllers {
    public class CadastroController : Controller {
        private readonly GrupoService _grupoService;
        private readonly ComputadorService _computadorService;
        public CadastroController(GrupoService grupoService, ComputadorService computadorService) {
            _grupoService = grupoService;
            _computadorService = computadorService;
        }

        [HttpGet]
        public IActionResult CadastroGrupo() {
            return View();
        }

        [HttpPost]
        public IActionResult CadastroGrupo(Grupo grupo) {
            _grupoService.AdicionarGrupo(grupo);
            return RedirectToAction("ListarGrupos");
        }

        [HttpGet]
        public IActionResult ListarGrupos() {
            var grupos = _grupoService.ListarGrupos();
            return View(grupos);
        }

        [HttpGet]
        public IActionResult CadastroComputador() {
            ViewBag.Grupos = _grupoService.ListarGrupos();
            return View();
        }

        [HttpPost]
        public IActionResult CadastroComputador(Computador computador) {
            _computadorService.AdicionarComputador(computador);
            return RedirectToAction("ListarComputadores");
        }

        [HttpGet]
        public IActionResult ListarComputadores() {
            var computadores = _computadorService.ListarComputadores();
            return View(computadores);
        }

    }
}
