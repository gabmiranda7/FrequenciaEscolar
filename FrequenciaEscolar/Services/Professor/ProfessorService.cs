using FrequenciaEscolar.Data;
using FrequenciaEscolar.Dto;
using FrequenciaEscolar.Models;
using FrequenciaEscolar.Services.Professor;
using Microsoft.EntityFrameworkCore;

namespace FrequenciaEscolar.Services.FrequenciaEscolar
{
    public class ProfessorService : IProfessorInterface
    {
        private readonly AppDbContext _context;
        private readonly string _sistema;
        public ProfessorService(AppDbContext context, IWebHostEnvironment sistema)
        {
            _context = context;
            _sistema = sistema.WebRootPath;
        }

        public async Task<Professor> CriarProfessor(ProfessorCriacaoDto professorCriacaoDto)
        {
            try
            {
                var professor = new Professor
                {
                    Nome = professorCriacaoDto.Nome,
                    Matricula =professorCriacaoDto.Disciplina,
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

        public async Task<Professor> EditarProfessor(Professor professor)
        {
            try
            {
                var professorBanco = await _context.Professores.AsNoTracking().FirstOrDefaultAsync(professorDB => professorDB.Id == professor.Id);


                professorBanco.Nome = professor.Nome;
                professorBanco.Matricula = professor.Disciplina;

                _context.Update(professorBanco);
                await _context.SaveChangesAsync();

                return professor;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Professor> GetProfessorPorId(int id)
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

        public async Task<List<Professor>> GetProfessores()
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

        public async Task<List<Professor>> GetProfessoresFiltro(string? pesquisar)
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

        public async Task<Professor> RemoverProfessor(int id)
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
