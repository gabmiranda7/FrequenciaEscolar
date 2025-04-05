using Microsoft.AspNetCore.Mvc;
using FrequenciaEscolar.Dto;
using FrequenciaEscolar.Models;
using FrequenciaEscolar.Services.Turmas;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FrequenciaEscolar.Data;

namespace FrequenciaEscolar.Controllers
{
    public class TurmaController : Controller
    {
        private readonly ITurmaInterface _turmaInterface;
        private readonly AppDbContext _context;

        public TurmaController(ITurmaInterface turmaInterface, AppDbContext context)
        {
            _turmaInterface = turmaInterface;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var turmas = await _turmaInterface.GetTurmas();
            return View(turmas);
        }

        public async Task<IActionResult> Cadastrar()
        {
            var professores = await _context.Professores.ToListAsync();
            ViewBag.Professores = new SelectList(professores, "Id", "Nome");
            return View();
        }


        public async Task<IActionResult> Detalhes(int id)
        {
            var turmas = await _turmaInterface.GetTurmaPorId(id);
            return View(turmas);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(TurmaCriacaoDto turmaCriacaoDto)
        {
            if (ModelState.IsValid)
            {
                var turmas = await _turmaInterface.CriarTurma(turmaCriacaoDto);
                return RedirectToAction("Index", "Turma");
            }
            else
            {
                return View(turmaCriacaoDto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Turma turma)
        {
            ModelState.Remove("Professor");

            if (!ModelState.IsValid)
            {
                ViewBag.Professores = new SelectList(await _context.Professores.ToListAsync(), "Id", "Nome");
                return View(turma);
            }

            var turmaDb = await _context.Turmas.FindAsync(turma.Id);
            if (turmaDb == null) return NotFound();

            turmaDb.Nome = turma.Nome;
            turmaDb.Ano = turma.Ano;
            turmaDb.ProfessorId = turma.ProfessorId;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Editar(int id)
        {
            var turma = await _context.Turmas.FindAsync(id);

            if (turma == null)
            {
                return NotFound();
            }

            ViewBag.Professores = new SelectList(await _context.Professores.ToListAsync(), "Id", "Nome", turma.ProfessorId);

            return View(turma);
        }


        public async Task<IActionResult> Remover(int id)
        {
            var turma = await _turmaInterface.RemoverTurma(id);
            return RedirectToAction("Index", "Turma");
        }
    }
}
