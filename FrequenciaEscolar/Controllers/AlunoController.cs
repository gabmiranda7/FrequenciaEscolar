using Microsoft.AspNetCore.Mvc;
using FrequenciaEscolar.Dto;
using FrequenciaEscolar.Models;
using FrequenciaEscolar.Services.Alunos;

namespace FrequenciaEscolar.Controllers
{
    public class AlunoController : Controller
    {
        private readonly IAlunoInterface _alunoInterface;

        public AlunoController(IAlunoInterface alunoInterface)
        {
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

        public async Task<IActionResult> Detalhes(int id)
        {
            var alunos = await _alunoInterface.GetAlunoPorId(id);
            return View(alunos);
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
    }
}
