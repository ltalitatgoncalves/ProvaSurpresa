using System.Collections.Generic;
using prova_surpresa_mvc.Models;

namespace prova_surpresa_mvc.Services
{
    public interface IAlunoService
    {
        IEnumerable<Aluno> GetAllAlunos();
        Aluno GetAlunoById(int id);
        void AddAluno(Aluno aluno);
        void UpdateAluno(Aluno aluno);
        void DeleteAluno(int id);
    }
}
