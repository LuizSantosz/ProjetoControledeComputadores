using Projeto_Controle_de_Computadores.Models;
using System.Collections.Generic;

namespace Projeto_Controle_de_Computadores.Services {
    public class GrupoService {
        private List<Grupo> grupos = new List<Grupo>();

        public void AdicionarGrupo(Grupo grupo) {
            grupos.Add(grupo);
        }

        public List<Grupo> ListarGrupos() {
            return grupos;
        }
    }
}
