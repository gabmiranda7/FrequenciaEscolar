using Microsoft.AspNetCore.Mvc;
using FrequenciaEscolar.Dto;
using FrequenciaEscolar.Models;
using FrequenciaEscolar.Services.Frequencias;
using System.Threading.Tasks;
using FrequenciaEscolar.Services.Alunos;
using FrequenciaEscolar.Services.Turmas;
using System.Linq;
using FrequenciaEscolar.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace FrequenciaEscolar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FrequenciaApiController : ControllerBase
    {
        private readonly IFrequenciaInterface _frequenciaInterface;
        private readonly IAlunoInterface _alunoInterface;
        private readonly ITurmaInterface _turmaInterface;
        private readonly AppDbContext _context;

        public FrequenciaApiController(IFrequenciaInterface frequenciaInterface, IAlunoInterface alunoInterface, ITurmaInterface turmaInterface, AppDbContext context)
        {
            _frequenciaInterface = frequenciaInterface;
            _alunoInterface = alunoInterface;
            _turmaInterface = turmaInterface;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Frequencia>>> GetAll()
        {
            var frequencias = await _context.Frequencias
                .Include(f => f.Aluno)
                .Include(f => f.Turma)
                .OrderBy(f => f.Data)
                .ToListAsync();

            return Ok(frequencias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Frequencia>> GetById(int id)
        {
            var frequencia = await _frequenciaInterface.ObterPorId(id);
            if (frequencia == null)
                return NotFound();
            return Ok(frequencia);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FrequenciaCriacaoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _frequenciaInterface.Criar(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FrequenciaCriacaoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _frequenciaInterface.Atualizar(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _frequenciaInterface.Remover(id);
            return Ok();
        }

        [HttpGet("alunos")]
        public async Task<IActionResult> GetAlunos()
        {
            var alunos = await _alunoInterface.GetAlunos();
            return Ok(alunos);
        }

        [HttpGet("turmas")]
        public async Task<IActionResult> GetTurmas()
        {
            var turmas = await _turmaInterface.GetTurmas();
            return Ok(turmas);
        }
    }
}
