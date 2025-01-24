namespace Projeto_Controle_de_Computadores.Models {
    public class Computador {
        public int Id { get; set; }
        public string Nome { get; set; } 
        public string Ip { get; set; } 
        public int GrupoId { get; set; } 
        public Grupo? Grupo { get; set; } 

        public Computador() {
            Nome = string.Empty;
            Ip = string.Empty;

        }
    }
}