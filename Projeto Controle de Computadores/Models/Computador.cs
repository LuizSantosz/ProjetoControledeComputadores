using Projeto_Controle_de_Computadores.Models;

public class Computador {
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty; // Inicializa com valor padrão
    public string EnderecoIp { get; set; } = string.Empty; // Inicializa com valor padrão
    public int GrupoId { get; set; }
    public Grupo? Grupo { get; set; } // Declara como anulável
}
