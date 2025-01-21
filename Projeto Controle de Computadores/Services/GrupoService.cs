﻿using Projeto_Controle_de_Computadores.Models;
using System.Collections.Generic;
using System.Linq;

namespace Projeto_Controle_de_Computadores.Services {
    public class GrupoService {
        private List<Grupo> grupos = new List<Grupo>();

        // Adiciona um grupo à lista, com verificação para evitar grupos nulos ou duplicados
        public void AdicionarGrupo(Grupo grupo) {
            if (grupo == null) {
                throw new ArgumentNullException(nameof(grupo), "O grupo não pode ser nulo.");
            }

            if (grupos.Any(g => g.Nome == grupo.Nome)) {
                throw new InvalidOperationException($"O grupo com o nome '{grupo.Nome}' já existe.");
            }

            grupos.Add(grupo);
        }

        // Retorna a lista de grupos
        public List<Grupo> ListarGrupos() {
            return grupos;
        }
    }
}
