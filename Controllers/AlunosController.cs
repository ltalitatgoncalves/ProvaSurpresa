using Microsoft.AspNetCore.Mvc;
using prova_surpresa_mvc.Models;
using prova_surpresa_mvc.Services;

namespace prova_surpresa_mvc.Controllers
{
    public class AlunosController : Controller
    {
        private readonly IAlunoService _alunoService;

        public AlunosController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        public IActionResult Index()
        {
            var alunos = _alunoService.GetAllAlunos();
            return View(alunos);
        }

        public IActionResult Details(int id)
        {
            var aluno = _alunoService.GetAlunoById(id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                _alunoService.AddAluno(aluno);
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }

        public IActionResult Edit(int id)
        {
            var aluno = _alunoService.GetAlunoById(id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Aluno aluno)
        {
            if (id != aluno.AlunoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _alunoService.UpdateAluno(aluno);
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }

        public IActionResult Delete(int id)
        {
            var aluno = _alunoService.GetAlunoById(id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _alunoService.DeleteAluno(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
