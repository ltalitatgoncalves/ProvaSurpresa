using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using prova_surpresa_mvc.Models;

namespace prova_surpresa_mvc.Services
{
    public class CursoService : ICursoService
    {
        private readonly ContextoEscola _context;
        private readonly ILogger<CursoService> _logger;

        public CursoService(ContextoEscola context, ILogger<CursoService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Curso> GetAllCursos()
        {
            return _context.Cursos.Include(c => c.Professor).ToList(); // Incluindo Professor na consulta
        }

        public Curso GetCursoById(int id)
        {
            return _context.Cursos.Include(c => c.Professor) // Incluindo Professor na consulta
                            .FirstOrDefault(c => c.CursoId == id);
        }

        public void AddCurso(Curso curso)
        {
            try
            {
                // Verificar se o professor já está associado a um curso
                var cursoExistente = _context.Cursos.FirstOrDefault(c => c.ProfessorId == curso.ProfessorId);
                if (cursoExistente != null)
                {
                    throw new Exception("Este professor já está associado a outro curso.");
                }

                _context.Cursos.Add(curso);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdateCurso(Curso curso)
        {
            try
            {
                // Verificar se o professor está associado a outro curso diferente do atual
                var cursoExistente = _context.Cursos.FirstOrDefault(c => c.ProfessorId == curso.ProfessorId && c.CursoId != curso.CursoId);
                if (cursoExistente != null)
                {
                    throw new Exception("Este professor já está associado a outro curso.");
                }

                _context.Cursos.Update(curso);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public void DeleteCurso(int id)
        {
            try
            {
                var curso = _context.Cursos.Find(id);
                if (curso != null)
                {
                    _context.Cursos.Remove(curso);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

    public interface ICursoService
    {
        IEnumerable<Curso> GetAllCursos();
        Curso GetCursoById(int id);
        void AddCurso(Curso curso);
        void UpdateCurso(Curso curso);
        void DeleteCurso(int id);
    }
}
