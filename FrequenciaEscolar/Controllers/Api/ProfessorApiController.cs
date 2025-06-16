using FrequenciaEscolar.Data;
using FrequenciaEscolar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrequenciaEscolar.Controllers.Api
{
    [ApiController]
    [Route("api/professorapi")]
    public class ProfessorApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProfessorApiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/professor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prof>>> GetProfessores()
        {
            return await _context.Professores.ToListAsync();
        }

        // GET: api/professor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Prof>> GetProfessor(int id)
        {
            var professor = await _context.Professores.FindAsync(id);

            if (professor == null)
                return NotFound();

            return professor;
        }

        // POST: api/professor
        [HttpPost]
        public async Task<ActionResult<Prof>> PostProfessor(Prof professor)
        {
            _context.Professores.Add(professor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProfessor), new { id = professor.Id }, professor);
        }

        // PUT: api/professor/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfessor(int id, Prof professor)
        {
            if (id != professor.Id)
                return BadRequest();

            _context.Entry(professor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Professores.Any(p => p.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/professor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfessor(int id)
        {
            var professor = await _context.Professores.FindAsync(id);
            if (professor == null)
                return NotFound();

            _context.Professores.Remove(professor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
