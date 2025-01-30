namespace Projeto_Controle_de_Computadores.Models {
    public class Grupo {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public List<Computador> Computadores { get; set; } = new List<Computador>();
    }
}
