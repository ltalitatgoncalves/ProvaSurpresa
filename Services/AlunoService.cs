using System.Collections.Generic;
using prova_surpresa_mvc.Models;

namespace prova_surpresa_mvc.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly IRepositorio<Aluno> _repositorio;

        public AlunoService(IRepositorio<Aluno> repositorio)
        {
            _repositorio = repositorio;
        }

        public IEnumerable<Aluno> GetAllAlunos()
        {
            return _repositorio.GetAll();
        }

        public Aluno GetAlunoById(int id)
        {
            return _repositorio.GetById(id);
        }

        public void AddAluno(Aluno aluno)
        {
            _repositorio.Add(aluno);
        }

        public void UpdateAluno(Aluno aluno)
        {
            _repositorio.Update(aluno);
        }

        public void DeleteAluno(int id)
        {
            _repositorio.Delete(id);
        }
    }
}
