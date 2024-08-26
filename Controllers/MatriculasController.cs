using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using prova_surpresa_mvc.Models;
using prova_surpresa_mvc.Services;
using System;

namespace prova_surpresa_mvc.Controllers
{
    public class MatriculasController : Controller
    {
        private readonly IMatriculaService _matriculaService;
        private readonly IAlunoService _alunoService;
        private readonly ICursoService _cursoService;

        public MatriculasController(IMatriculaService matriculaService, IAlunoService alunoService, ICursoService cursoService)
        {
            _matriculaService = matriculaService;
            _alunoService = alunoService;
            _cursoService = cursoService;
        }

        public IActionResult Index()
        {
            var matriculas = _matriculaService.GetAllMatriculas();
            return View(matriculas);
        }

        public IActionResult Details(int id)
        {
            var matricula = _matriculaService.GetMatriculaById(id);
            if (matricula == null)
            {
                return NotFound();
            }
            return View(matricula);
        }

        public IActionResult Create()
        {
            ViewBag.Alunos = new SelectList(_alunoService.GetAllAlunos(), "AlunoId", "Nome");
            ViewBag.Cursos = new SelectList(_cursoService.GetAllCursos(), "CursoId", "Titulo");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Matricula matricula)
        {
            // Verificar se os IDs são válidos
            if (matricula.AlunoId == 0)
            {
                ModelState.AddModelError("AlunoId", "Por favor, selecione um aluno.");
            }
            if (matricula.CursoId == 0)
            {
                ModelState.AddModelError("CursoId", "Por favor, selecione um curso.");
            }

            if (ModelState.IsValid)
            {
                // Carregar os objetos Aluno e Curso com base nos IDs fornecidos
                matricula.Aluno = _alunoService.GetAlunoById(matricula.AlunoId);
                matricula.Curso = _cursoService.GetCursoById(matricula.CursoId);

                // Verificar se os objetos são válidos
                if (matricula.Aluno == null)
                {
                    ModelState.AddModelError("Aluno", "O aluno selecionado é inválido.");
                }
                if (matricula.Curso == null)
                {
                    ModelState.AddModelError("Curso", "O curso selecionado é inválido.");
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _matriculaService.AddMatricula(matricula);
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "Ocorreu um erro ao criar a matrícula.");
                    }
                }
            }

            // Recarregar as listas de alunos e cursos em caso de erro
            ViewBag.Alunos = new SelectList(_alunoService.GetAllAlunos(), "AlunoId", "Nome");
            ViewBag.Cursos = new SelectList(_cursoService.GetAllCursos(), "CursoId", "Titulo");
            return View(matricula);
        }

        public IActionResult Edit(int id)
        {
            var matricula = _matriculaService.GetMatriculaById(id);
            if (matricula == null)
            {
                return NotFound();
            }
            ViewBag.Alunos = new SelectList(_alunoService.GetAllAlunos(), "AlunoId", "Nome", matricula.AlunoId);
            ViewBag.Cursos = new SelectList(_cursoService.GetAllCursos(), "CursoId", "Titulo", matricula.CursoId);
            return View(matricula);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Matricula matricula)
        {
            if (id != matricula.MatriculaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _matriculaService.UpdateMatricula(matricula);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao atualizar a matrícula.");
                }
            }
            ViewBag.Alunos = new SelectList(_alunoService.GetAllAlunos(), "AlunoId", "Nome", matricula.AlunoId);
            ViewBag.Cursos = new SelectList(_cursoService.GetAllCursos(), "CursoId", "Titulo", matricula.CursoId);
            return View(matricula);
        }

        public IActionResult Delete(int id)
        {
            var matricula = _matriculaService.GetMatriculaById(id);
            if (matricula == null)
            {
                return NotFound();
            }
            return View(matricula);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _matriculaService.DeleteMatricula(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao excluir a matrícula.");
                return View();
            }
        }
    }
}

