namespace prova_surpresa_mvc.Models
{
    public class Aluno
    {
        public int AlunoId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
    }

}
