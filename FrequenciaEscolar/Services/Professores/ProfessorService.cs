using FrequenciaEscolar.Data;
using FrequenciaEscolar.Dto;
using FrequenciaEscolar.Models;
using FrequenciaEscolar.Services.Professores;
using Microsoft.EntityFrameworkCore;

namespace FrequenciaEscolar.Services.Professores
{
    public class ProfessorService : IProfessorInterface
    {
        private readonly AppDbContext _context;
        public ProfessorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Prof> CriarProfessor(ProfessorCriacaoDto professorCriacaoDto)
        {
            try
            {
                var professor = new Prof
                {
                    Nome = professorCriacaoDto.Nome,
                    Disciplina = professorCriacaoDto.Disciplina,
                    Turmas = new List<Turma>()
                };

                _context.Add(professor);
                await _context.SaveChangesAsync();

                return professor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<Prof> EditarProfessor(Prof professor)
        {
            try
            {
                var professorBanco = await _context.Professores.AsNoTracking().FirstOrDefaultAsync(professorDB => professorDB.Id == professor.Id);


                professorBanco.Nome = professor.Nome;
                professorBanco.Disciplina = professor.Disciplina;

                _context.Update(professorBanco);
                await _context.SaveChangesAsync();

                return professor;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Prof> GetProfessorPorId(int id)
        {
            try
            {

                return await _context.Professores.FirstOrDefaultAsync(professor => professor.Id == id);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Prof>> GetProfessores()
        {
            try
            {
                return await _context.Professores.ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Prof>> GetProfessoresFiltro(string? pesquisar)
        {
            try
            {
                var professores = await _context.Professores.Where(professorBanco => professorBanco.Nome.Contains(pesquisar)).ToListAsync();
                return professores;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Prof> RemoverProfessor(int id)
        {
            try
            {
                var professor = await _context.Professores.FirstOrDefaultAsync(professorDB => professorDB.Id == id);

                _context.Remove(professor);
                await _context.SaveChangesAsync();

                return professor;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
