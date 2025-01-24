using Projeto_Controle_de_Computadores.Models;
using System.Collections.Generic;
using System.Linq;

namespace Projeto_Controle_de_Computadores.Services {
    public class ComputadorService {
        private List<Computador> computadores = new List<Computador>();
        private readonly GrupoService _grupoService;

        public ComputadorService(GrupoService grupoService) {
            _grupoService = grupoService;
        }

        public void AdicionarComputador(Computador computador) {
            computadores.Add(computador);
        }

        public void AtualizarComputador(Computador computador) {
            var computadorExistente = computadores.FirstOrDefault(c => c.Id == computador.Id);
            if (computadorExistente == null) {
                throw new InvalidOperationException("Computador não encontrado.");
            }

            computadorExistente.Nome = computador.Nome;
            computadorExistente.Ip = computador.Ip;
            computadorExistente.GrupoId = computador.GrupoId;
        }

        public void ExcluirComputador(int id) {
            var computador = computadores.FirstOrDefault(c => c.Id == id);
            if (computador == null) {
                throw new InvalidOperationException("Computador não encontrado.");
            }

            computadores.Remove(computador);
        }
        public List<Computador> ListarComputadores() {
            foreach (var computador in computadores) {
                computador.Grupo = _grupoService.ListarGrupos()
                                                .FirstOrDefault(g => g.Id == computador.GrupoId);
            }
            return computadores;
        }
    }
}
