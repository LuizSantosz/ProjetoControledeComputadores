using Projeto_Controle_de_Computadores.Models;
using System.Collections.Generic;

namespace Projeto_Controle_de_Computadores.Services {
    public class ComputadorService {
        private List<Computador> computadores = new List<Computador>();

        public void AdicionarComputador(Computador computador) {
            computadores.Add(computador);
        }

        public List<Computador> ListarComputadores() {
            return computadores;
        }
    }
}
