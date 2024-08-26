using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using prova_surpresa_mvc.Models;
using prova_surpresa_mvc.Services;
using System;

namespace prova_surpresa_mvc.Controllers
{
    public class CursosController : Controller
    {
        private readonly ICursoService _cursoService;
        private readonly IProfessorService _professorService;

        public CursosController(ICursoService cursoService, IProfessorService professorService)
        {
            _cursoService = cursoService;
            _professorService = professorService;
        }

        public IActionResult Index()
        {
            var cursos = _cursoService.GetAllCursos();
            return View(cursos);
        }

        public IActionResult Details(int id)
        {
            var curso = _cursoService.GetCursoById(id);
            if (curso == null)
            {
                return NotFound();
            }
            return View(curso);
        }

        public IActionResult Create()
        {
            var professoresDisponiveis = _professorService.GetAllProfessores().Where(p => !_cursoService.GetAllCursos().Any(c => c.ProfessorId == p.ProfessorId));
            ViewBag.Professores = new SelectList(professoresDisponiveis, "ProfessorId", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Curso curso)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _cursoService.AddCurso(curso);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao criar o curso.");
                }
            }

            ViewBag.Professores = new SelectList(_professorService.GetAllProfessores(), "ProfessorId", "Nome", curso.ProfessorId);
            return View(curso);
        }

        public IActionResult Edit(int id)
        {
            var curso = _cursoService.GetCursoById(id);
            if (curso == null)
            {
                return NotFound();
            }

            var professoresDisponiveis = _professorService.GetAllProfessores().Where(p => !_cursoService.GetAllCursos().Any(c => c.ProfessorId == p.ProfessorId) || p.ProfessorId == curso.ProfessorId);
            ViewBag.Professores = new SelectList(professoresDisponiveis, "ProfessorId", "Nome", curso.ProfessorId);
            return View(curso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Curso curso)
        {
            if (id != curso.CursoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _cursoService.UpdateCurso(curso);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao atualizar o curso.");
                }
            }
            ViewBag.Professores = new SelectList(_professorService.GetAllProfessores(), "ProfessorId", "Nome", curso.ProfessorId);
            return View(curso);
        }

        public IActionResult Delete(int id)
        {
            var curso = _cursoService.GetCursoById(id);
            if (curso == null)
            {
                return NotFound();
            }
            return View(curso);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _cursoService.DeleteCurso(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao excluir o curso.");
                return View();
            }
        }
    }
}
