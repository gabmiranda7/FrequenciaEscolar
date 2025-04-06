using Microsoft.AspNetCore.Mvc;
using FrequenciaEscolar.Dto;
using FrequenciaEscolar.Models;
using FrequenciaEscolar.Services.Alunos;
using FrequenciaEscolar.ViewModel;
using Microsoft.EntityFrameworkCore;
using FrequenciaEscolar.Data;


namespace FrequenciaEscolar.Controllers
{
    public class AlunoController : Controller
    {
        private readonly IAlunoInterface _alunoInterface;
        private readonly AppDbContext _context;
        public AlunoController(AppDbContext context, IAlunoInterface alunoInterface)
        {
            _context = context;
            _alunoInterface = alunoInterface;
        }


        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 10;
            var totalAlunos = await _alunoInterface.GetTotalAlunos();
            var alunos = await _alunoInterface.GetAlunosPaginados(page, pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalAlunos / pageSize);

            return View(alunos);
        }


        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(AlunoCriacaoDto alunoCriacaoDto)
        {
            if (ModelState.IsValid)
            {
                var alunos = await _alunoInterface.CriarAluno(alunoCriacaoDto);
                return RedirectToAction("Index", "Aluno");
            }
            else
            {
                return View(alunoCriacaoDto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Aluno alunoModel)
        {
            if (ModelState.IsValid)
            {
                var aluno = await _alunoInterface.EditarAluno(alunoModel);
                return RedirectToAction("Index", "Aluno");
            }
            else
            {
                return View(alunoModel);
            }
        }

        public async Task<IActionResult> Editar(int id)
        {
            var aluno = await _alunoInterface.GetAlunoPorId(id);

            return View(aluno);
        }

        public async Task<IActionResult> Remover(int id)
        {
            var aluno = await _alunoInterface.RemoverAluno(id);
            return RedirectToAction("Index", "Aluno");
        }
        public IActionResult Detalhes(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);
            if (aluno == null)
            {
                return NotFound();
            }

            var frequencias = _context.Frequencias
                .Include(f => f.Turma)
                .Where(f => f.AlunoId == id)
                .OrderByDescending(f => f.Data)
                .ToList();

            var viewModel = new AlunoDetalhesViewModel
            {
                AlunoId = aluno.Id,
                Nome = aluno.Nome,
                Matricula = aluno.Matricula,
                Frequencias = frequencias
            };

            return View(viewModel);
        }

    }
}
