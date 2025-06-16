using Microsoft.AspNetCore.Mvc;
using FrequenciaEscolar.Data;
using FrequenciaEscolar.Models;
using Microsoft.EntityFrameworkCore;

namespace FrequenciaEscolar.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurmaApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TurmaApiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/turmaapi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Turma>>> GetTurmas()
        {
            try
            {
                var turmas = await _context.Turmas
                    .Include(t => t.Professor)
                    .ToListAsync();

                return Ok(turmas);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }


        // GET: api/turmaapi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Turma>> GetTurma(int id)
        {
            var turma = await _context.Turmas
                .Include(t => t.Professor)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (turma == null)
                return NotFound();

            return turma;
        }

        // POST: api/turmaapi
        [HttpPost]
        public async Task<ActionResult<Turma>> PostTurma(Turma turma)
        {
            // Verifica se o Professor existe
            var professor = await _context.Professores.FindAsync(turma.ProfessorId);
            if (professor == null)
                return BadRequest("ProfessorId inválido.");

            // Não inclua o objeto Professor no Add, apenas o ID
            _context.Turmas.Add(new Turma
            {
                Nome = turma.Nome,
                Ano = turma.Ano,
                ProfessorId = turma.ProfessorId
            });

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTurma), new { id = turma.Id }, turma);
        }


        // PUT: api/turmaapi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTurma(int id, Turma turma)
        {
            if (id != turma.Id)
                return BadRequest();

            _context.Entry(turma).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TurmaExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/turmaapi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTurma(int id)
        {
            var turma = await _context.Turmas.FindAsync(id);
            if (turma == null)
                return NotFound();

            _context.Turmas.Remove(turma);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TurmaExists(int id)
        {
            return _context.Turmas.Any(e => e.Id == id);
        }
    }
}
