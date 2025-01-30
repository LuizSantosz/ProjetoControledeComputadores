using Projeto_Controle_de_Computadores.Models;
using System.Collections.Generic;
using System.Linq;

namespace Projeto_Controle_de_Computadores.Services {
    public class GrupoService {
        private List<Grupo> grupos = new List<Grupo>();

        public void AdicionarGrupo(Grupo grupo) {
            if (grupo == null) {
                throw new ArgumentNullException(nameof(grupo), "O grupo não pode ser nulo.");
            }

            if (grupos.Any(g => g.Nome == grupo.Nome)) {
                throw new InvalidOperationException($"O grupo com o nome '{grupo.Nome}' já existe.");
            }

            grupos.Add(grupo);
        }

        public void AtualizarGrupo(Grupo grupo) {
            var grupoExistente = grupos.FirstOrDefault(g => g.Id == grupo.Id);
            if (grupoExistente == null) {
                throw new InvalidOperationException("Grupo não Encontrado.");
            }
            grupoExistente.Nome = grupo.Nome;
        }

        public void ExcluirGrupo(int id) {
            var grupo = grupos.FirstOrDefault(g => g.Id == id);
            if (grupo == null) {
                throw new InvalidOperationException("Grupo não encontrado.");

            }
            grupos.Remove(grupo);
        }

        public List<Grupo> ListarGrupos() {
            return grupos;
        }
    }
}
