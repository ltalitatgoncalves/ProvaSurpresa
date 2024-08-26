

using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using prova_surpresa_mvc.Models;

namespace prova_surpresa_mvc.Services
{
    public class ProfessorService : IProfessorService
    {
        private readonly IRepositorio<Professor> _repositorio;
        private readonly ILogger<ProfessorService> _logger;

        public ProfessorService(IRepositorio<Professor> repositorio, ILogger<ProfessorService> logger)
        {
            _repositorio = repositorio;
            _logger = logger;
        }

        public IEnumerable<Professor> GetAllProfessores()
        {
            return _repositorio.GetAll();
        }

        public Professor GetProfessorById(int id)
        {
            return _repositorio.GetById(id);
        }

        public void AddProfessor(Professor professor)
        {
             _repositorio.Add(professor);
        }

        public void UpdateProfessor(Professor professor)
        {
             _repositorio.Update(professor);

        }

        public void DeleteProfessor(int id)
        {
             _repositorio.Delete(id);
        }
    }

    public interface IProfessorService
    {
        IEnumerable<Professor> GetAllProfessores();
        Professor GetProfessorById(int id);
        void AddProfessor(Professor professor);
        void UpdateProfessor(Professor professor);
        void DeleteProfessor(int id);
    }
}

