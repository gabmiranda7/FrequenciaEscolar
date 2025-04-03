using Microsoft.AspNetCore.Mvc;
using FrequenciaEscolar.Dto;
using FrequenciaEscolar.Models;
using FrequenciaEscolar.Services.FrequenciaEscolar;
using FrequenciaEscolar.Services.Professor;

namespace FrequenciaEscolar.Controllers
{
    public class ProfessorController : Controller
    {
        private readonly IProfessorInterface _professorInterface;

        public ProfessorController(IProfessorInterface professorInterface)
        {
            _professorInterface = professorInterface;
        }

        public async Task<IActionResult> Index()
        {
            var professores = await _professorInterface.GetProfessores();
            return View(professores);
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var professores = await _professorInterface.GetProfessorPorId(id);
            return View(professores);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(ProfessorCriacaoDto professorCriacaoDto)
        {
            if (ModelState.IsValid)
            {
                var professores = await _professorInterface.CriarProfessor(professorCriacaoDto);
                return RedirectToAction("Index", "Professor");
            }
            else
            {
                return View(professorCriacaoDto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Professor professorModel)
        {
            if (ModelState.IsValid)
            {
                var professor = await _professorInterface.EditarProfessor(professorModel);
                return RedirectToAction("Index", "Professor");
            }
            else
            {
                return View(professorModel);
            }
        }

        public async Task<IActionResult> Editar(int id)
        {
            var professor = await _professorInterface.GetProfessorPorId(id);

            return View(professor);
        }

        public async Task<IActionResult> Remover(int id)
        {
            var professor = await _professorInterface.RemoverProfessor(id);
            return RedirectToAction("Index", "Professor");
        }
    }
}
