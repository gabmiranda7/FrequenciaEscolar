using Microsoft.AspNetCore.Mvc;
using FrequenciaEscolar.Dto;
using FrequenciaEscolar.Models;
using FrequenciaEscolar.Services.Professores;
using Microsoft.EntityFrameworkCore;
using FrequenciaEscolar.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrequenciaEscolar.Controllers
{
    public class ProfessorController : Controller
    {
        private readonly IProfessorInterface _professorInterface;
        private readonly AppDbContext _context;

        public ProfessorController(IProfessorInterface professorInterface, AppDbContext context)
        {
            _professorInterface = professorInterface;
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 6;

            var query = _context.Professores.AsQueryable();

            int totalItems = await query.CountAsync();
            var professores = await query
                .OrderBy(p => p.Nome)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

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
        [HttpPost]
        public async Task<IActionResult> Editar(Prof professorModel)
        {
            if (ModelState.IsValid)
            {
                _context.Professores.Update(professorModel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Professor");
            }

            return View(professorModel);
        }


        public async Task<IActionResult> Editar(int id)
        {
            var professor = await _professorInterface.GetProfessorPorId(id);

            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }


        public async Task<IActionResult> Remover(int id)
        {
            var professor = await _professorInterface.RemoverProfessor(id);
            return RedirectToAction("Index", "Professor");
        }
    }
}
