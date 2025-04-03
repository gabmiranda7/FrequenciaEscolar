using Microsoft.AspNetCore.Mvc;
using FrequenciaEscolar.Dto;
using FrequenciaEscolar.Models;
using FrequenciaEscolar.Services.FrequenciaEscolar;

namespace FrequenciaEscolar.Controllers
{
    public class AlunoController : Controller
    {
        private readonly IAlunoInterface _alunoInterface;

        public AlunoController(IAlunoInterface alunoInterface)
        {
            _alunoInterface = alunoInterface;
        }

        public async Task<IActionResult> Index()
        {
            var alunos = await _alunoInterface.GetAlunos();
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
            var anotacao = await _alunoInterface.RemoverAluno(id);
            return RedirectToAction("Index", "Aluno");
        }
    }
}
