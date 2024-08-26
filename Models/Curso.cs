

using System.Collections.Generic;

namespace prova_surpresa_mvc.Models
{
    public class Curso
    {
        public int CursoId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int VagasDisponiveis { get; set; } = 30;

        // Este campo já é validado pelo ProfessorId
        public int ProfessorId { get; set; }
        public Professor? Professor { get; set; }

      
        public ICollection<Matricula>? Matriculas { get; set; }
    }
}
