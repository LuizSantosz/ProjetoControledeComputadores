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

        //Grupo

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

        //Computador

        [HttpGet]
        public IActionResult CadastroComputador() {
            var grupos = _grupoService.ListarGrupos();
            if (grupos == null || !grupos.Any()) {
                ViewBag.ErrorMessage = "Não há grupos disponíveis. Por favor, cadastre um grupo primeiro.";
                return RedirectToAction("ListarGrupos"); // Corrigido para listar os grupos
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

        //Editar Grupo

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

        //Excluir Grupo

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

        //Editar Computador

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

        //Excluir Computador

        [HttpPost]
        public IActionResult ExcluirComputador(int id) {
            try {
                _computadorService.ExcluirComputador(id);
                return RedirectToAction("ListarComputadores");
            } catch (Exception ex) {
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("ListarComputadores");
            }
        }


        //Listar

        [HttpGet]
        public IActionResult ListarComputadores() {
            var computadores = _computadorService.ListarComputadores();
            return View(computadores);
        }

        [HttpGet]
        public IActionResult ListarGrupos() {
            var grupos = _grupoService.ListarGrupos() ?? new List<Grupo>();
            return View(grupos);
        }
    }
}
