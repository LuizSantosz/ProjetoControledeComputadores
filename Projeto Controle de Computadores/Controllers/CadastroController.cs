using Microsoft.AspNetCore.Mvc;
using Projeto_Controle_de_Computadores.Models;
using Projeto_Controle_de_Computadores.Services;
using System.Linq.Expressions;

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
            if (ModelState.IsValid) {
                _grupoService.AdicionarGrupo(grupo);
                return RedirectToAction("ListarGrupos");
            }
            return View(grupo);
        }

        [HttpGet]
        public IActionResult ListarGrupos() {
            var grupos = _grupoService.ListarGrupos();
            if (grupos == null) {
                grupos = new List<Grupo>();
            }

            return View(grupos);
        }

        [HttpGet]
        public IActionResult EditarGrupo(int id) {
            var grupo = _grupoService.ListarGrupos().FirstOrDefault(g => g.Id == id);
            if (grupo == null) {
                return NotFound();
            }
            return View(grupo);
        }

        [HttpPost]
        public IActionResult EditarGrupo(Grupo grupo) {
            if (ModelState.IsValid) {
                try {
                    _grupoService.AtualizarGrupo(grupo);
                    return RedirectToAction("ListarGrupos");
                } catch (InvalidOperationException ex) {
                    ViewBag.ErrorMessage = ex.Message;
                    return View(grupo);
                }
            }
            return View(grupo);
        }

        [HttpPost]
        public IActionResult ExcluirGrupo(int id) {
            try {
                _grupoService.ExcluirGrupo(id);
                return RedirectToAction("ListarGrupos");
            } catch (Exception ex) {
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("ListarGrupos");
            }
        }


        [HttpGet]
        public IActionResult CadastroComputador() {
            var grupos = _grupoService.ListarGrupos();
            if (grupos == null || !grupos.Any()) {
                ViewBag.ErrorMessage = "Não há grupos disponíveis. Por favor, cadastre um grupo primeiro.";
                return View("ListarGrupos");
            }

            ViewBag.Grupos = grupos;
            return View(new Computador());
        }

        [HttpPost]
        public IActionResult CadastroComputador(Computador computador) {
            if (ModelState.IsValid) {
                _computadorService.AdicionarComputador(computador);
                return RedirectToAction("ListarComputadores");
            }

            ViewBag.Grupos = _grupoService.ListarGrupos();
            return View(computador);
        }

        [HttpGet]
        public IActionResult EditarComputador(int id) {
            var computador = _computadorService.ListarComputadores().FirstOrDefault(c => c.Id == id);
            if (computador == null) {
                return NotFound();
            }

            ViewBag.Grupos = _grupoService.ListarGrupos();
            return View(computador);
        }

        [HttpPost]
        public IActionResult EditarComputador(Computador computador) {
            if (ModelState.IsValid) {
                _computadorService.AtualizarComputador(computador);
                return RedirectToAction("ListarComputadores");
            }
            ViewBag.Grupos = _grupoService.ListarGrupos();
            return View(computador);
        }

        [HttpPost]
        public IActionResult ExcluirComputador(int id) {
            try {
                _computadorService.ExcluirComputador(id);
                return RedirectToAction("Listarcomputadores");
            } catch (Exception ex) {
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("Listarcomputadores");
            }
        }

        [HttpGet]
        public IActionResult ListarComputadores() {
            var computadores = _computadorService.ListarComputadores();
            return View(computadores);
        }
    }
}
