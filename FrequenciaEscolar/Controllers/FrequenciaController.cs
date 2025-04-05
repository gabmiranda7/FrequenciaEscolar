using Microsoft.AspNetCore.Mvc;
using FrequenciaEscolar.Dto;
using FrequenciaEscolar.Models;
using FrequenciaEscolar.Services.Frequencias;
using System.Threading.Tasks;
using FrequenciaEscolar.Services.Alunos;
using Microsoft.AspNetCore.Mvc.Rendering;
using FrequenciaEscolar.Services.Turmas;
using System.Linq;
using FrequenciaEscolar.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace FrequenciaEscolar.Controllers
{
    public class FrequenciaController : Controller
    {
        private readonly IFrequenciaInterface _frequenciaInterface;
        private readonly IAlunoInterface _alunoInterface;
        private readonly ITurmaInterface _turmaInterface;
        private readonly AppDbContext _context;

        public FrequenciaController(IFrequenciaInterface frequenciaInterface, IAlunoInterface alunoInterface, ITurmaInterface turmaInterface, AppDbContext context)
        {
            _frequenciaInterface = frequenciaInterface ?? throw new ArgumentNullException(nameof(frequenciaInterface));
            _alunoInterface = alunoInterface ?? throw new ArgumentNullException(nameof(alunoInterface));
            _turmaInterface = turmaInterface ?? throw new ArgumentNullException(nameof(turmaInterface));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IActionResult> Index()
        {
            var frequencias = await _frequenciaInterface.ObterTodos();
            return View(frequencias);
        }

        public async Task<IActionResult> Registrar()
        {
            var alunos = await _alunoInterface.GetAlunos() ?? new List<Aluno>();
            ViewBag.Alunos = alunos.Select(a => new { a.Id, a.Nome, a.Matricula }).ToList();

            var turmas = await _turmaInterface.GetTurmas() ?? new List<Turma>();
            ViewBag.Turmas = new SelectList(turmas, "Id", "Nome");

            return View();
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var frequencia = await _frequenciaInterface.ObterPorId(id);
            if (frequencia == null)
            {
                return NotFound();
            }
            return View("Detalhes", frequencia);
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(FrequenciaCriacaoDto frequenciaDto)
        {
            if (!ModelState.IsValid)
            {
                var alunos = await _alunoInterface.GetAlunos() ?? new List<Aluno>();
                ViewBag.Alunos = alunos.Select(a => new { a.Id, a.Nome, a.Matricula }).ToList();

                var turmas = await _turmaInterface.GetTurmas() ?? new List<Turma>();
                ViewBag.Turmas = new SelectList(turmas, "Id", "Nome");

                return View(frequenciaDto);
            }

            await _frequenciaInterface.Criar(frequenciaDto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, [FromBody] FrequenciaCriacaoDto frequenciaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _frequenciaInterface.Atualizar(id, frequenciaDto);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Deletar(int id)
        {
            await _frequenciaInterface.Remover(id);
            return Ok();
        }

    }
}
