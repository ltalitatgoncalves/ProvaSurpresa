using Microsoft.Extensions.Logging;
using prova_surpresa_mvc.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace prova_surpresa_mvc.Services
{
    public interface IMatriculaService
    {
        IEnumerable<Matricula> GetAllMatriculas();
        Matricula GetMatriculaById(int id);
        void AddMatricula(Matricula matricula);
        void UpdateMatricula(Matricula matricula);
        void DeleteMatricula(int id);
    }

    public class MatriculaService : IMatriculaService
    {
        private readonly ContextoEscola _context;
        private readonly ILogger<MatriculaService> _logger;

        public MatriculaService(ContextoEscola context, ILogger<MatriculaService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Matricula> GetAllMatriculas()
        {
            return _context.Matriculas.Include(m => m.Aluno).Include(m => m.Curso).ToList();
        }

        public Matricula GetMatriculaById(int id)
        {
            return _context.Matriculas.Include(m => m.Aluno).Include(m => m.Curso).FirstOrDefault(m => m.MatriculaId == id);
        }

        public void AddMatricula(Matricula matricula)
        {
            var curso = _context.Cursos.Find(matricula.CursoId);
            if (curso != null && curso.VagasDisponiveis > 0)
            {
                curso.VagasDisponiveis--;
                _context.Matriculas.Add(matricula);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Não há vagas disponíveis neste curso.");
            }

        }

        public void UpdateMatricula(Matricula matricula)
        {
            _context.Matriculas.Update(matricula);
            _context.SaveChanges();
        }

        public void DeleteMatricula(int id)
        {
            var matricula = _context.Matriculas.Include(m => m.Curso).FirstOrDefault(m => m.MatriculaId == id);
            if (matricula != null)
            {
                var curso = matricula.Curso;
                if (curso != null)
                {
                    curso.VagasDisponiveis++;
                }

                _context.Matriculas.Remove(matricula);
                _context.SaveChanges();
            }
        }
    }
}
