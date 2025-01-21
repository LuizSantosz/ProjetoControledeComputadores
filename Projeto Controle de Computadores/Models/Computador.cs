namespace Projeto_Controle_de_Computadores.Models {
    public class Computador {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;  // Inicializa com valor padrão
        public string EnderecoIp { get; set; } = string.Empty;  // Inicializa com valor padrão
        public int GrupoId { get; set; }
        public Grupo Grupo { get; set; } = new Grupo();  // Inicializa a propriedade 'Grupo' com uma nova instância

        // Caso precise de um construtor adicional
        public Computador() {
            Nome = string.Empty;  // Define o valor padrão
            EnderecoIp = string.Empty;  // Define o valor padrão
            Grupo = new Grupo();  // Garante que 'Grupo' nunca seja nulo
        }
    }
}
