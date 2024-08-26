using Microsoft.AspNetCore.Mvc;
using prova_surpresa_mvc.Models;
using prova_surpresa_mvc.Services;

namespace prova_surpresa_mvc.Controllers
{
    public class ProfessoresController : Controller
    {
        private readonly IProfessorService _professorService;

        public ProfessoresController(IProfessorService professorService)
        {
            _professorService = professorService;
        }

        public IActionResult Index()
        {
            var professores = _professorService.GetAllProfessores();
            return View(professores);
        }

        public IActionResult Details(int id)
        {
            var professor = _professorService.GetProfessorById(id);
            if (professor == null)
            {
                return NotFound();
            }
            return View(professor);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Professor professor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _professorService.AddProfessor(professor);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao criar o professor.");
                }
            }
            return View(professor);
        }

        public IActionResult Edit(int id)
        {
            var professor = _professorService.GetProfessorById(id);
            if (professor == null)
            {
                return NotFound();
            }
            return View(professor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Professor professor)
        {
            if (id != professor.ProfessorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _professorService.UpdateProfessor(professor);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao atualizar o professor.");
                }
            }
            return View(professor);
        }

        public IActionResult Delete(int id)
        {
            var professor = _professorService.GetProfessorById(id);
            if (professor == null)
            {
                return NotFound();
            }
            return View(professor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _professorService.DeleteProfessor(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao excluir o professor.");
                return View();
            }
        }
    }
}
