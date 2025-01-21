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
            if (ModelState.IsValid) {
                _grupoService.AdicionarGrupo(grupo);
                return RedirectToAction("ListarGrupos");
            }
            return View(grupo);  // Retorna a view com o modelo para exibir possíveis erros de validação
        }

        [HttpGet]
        public IActionResult ListarGrupos() {
            var grupos = _grupoService.ListarGrupos();

            // Certifique-se de que grupos nunca seja null
            if (grupos == null) {
                grupos = new List<Grupo>(); // Garante que seja uma lista vazia
            }

            return View(grupos);
        }


        [HttpGet]
        public IActionResult CadastroComputador() {
            var grupos = _grupoService.ListarGrupos();

            // Se não houver grupos cadastrados, avise o usuário e não carregue a página de cadastro
            if (grupos == null || !grupos.Any()) {
                ViewBag.ErrorMessage = "Não há grupos disponíveis. Por favor, cadastre um grupo primeiro.";
                return View("ListarGrupos");  // Pode redirecionar para a tela de ListarGrupos ou exibir uma mensagem
            }

            // Passa a lista de grupos para a view
            ViewBag.Grupos = grupos;
            return View(new Computador());  // Passa um novo Computador para a view
        }

        [HttpPost]
        public IActionResult CadastroComputador(Computador computador) {
            if (ModelState.IsValid) {
                // Adiciona o computador ao serviço
                _computadorService.AdicionarComputador(computador);
                return RedirectToAction("ListarComputadores");
            }

            // Caso haja erros de validação, devolve para a view com o computador e os grupos
            ViewBag.Grupos = _grupoService.ListarGrupos(); // Passa a lista de grupos para a view
            return View(computador);
        }

        [HttpGet]
        public IActionResult ListarComputadores() {
            var computadores = _computadorService.ListarComputadores();
            return View(computadores);
        }
    }
}
