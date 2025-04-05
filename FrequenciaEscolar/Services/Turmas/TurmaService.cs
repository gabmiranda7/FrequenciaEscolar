using FrequenciaEscolar.Data;
using FrequenciaEscolar.Dto;
using FrequenciaEscolar.Models;
using FrequenciaEscolar.Services.Turmas;
using Microsoft.EntityFrameworkCore;

namespace FrequenciaEscolar.Services.Turmas
{
    public class TurmaService : ITurmaInterface
    {
        private readonly AppDbContext _context;
        private readonly string _sistema;
        public TurmaService(AppDbContext context, IWebHostEnvironment sistema)
        {
            _context = context;
            _sistema = sistema.WebRootPath;
        }
        public async Task<IEnumerable<Turma>> GetTurmasPaginadas(int page, int pageSize)
        {
            return await _context.Turmas
                .OrderBy(t => t.Nome) // ou qualquer outro critério
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<int> GetTotalTurmas()
        {
            return await _context.Turmas.CountAsync();
        }

        public async Task<Turma> CriarTurma(TurmaCriacaoDto turmaCriacaoDto)
        {
            try
            {
                // Buscar o professor no banco de dados
                var professor = await _context.Professores.FindAsync(turmaCriacaoDto.ProfessorId);
                if (professor == null)
                {
                    throw new Exception("Professor não encontrado.");
                }

                var turma = new Turma
                {
                    Nome = turmaCriacaoDto.Nome,
                    Ano = turmaCriacaoDto.Ano,
                    ProfessorId = turmaCriacaoDto.ProfessorId,
                    Professor = professor,
                    AlunosTurmas = new List<AlunoTurma>()
                };

                _context.Add(turma);
                await _context.SaveChangesAsync();

                return turma;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Turma> EditarTurma(Turma turma)
        {
            try
            {
                var turmaBanco = await _context.Turmas.AsNoTracking().FirstOrDefaultAsync(turmaDB => turmaDB.Id == turma.Id);

                turmaBanco.Nome = turma.Nome;
                turmaBanco.Ano = turma.Ano;

                _context.Update(turmaBanco);
                await _context.SaveChangesAsync();

                return turma;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Turma> GetTurmaPorId(int id)
        {
            try
            {
                return await _context.Turmas.FirstOrDefaultAsync(turma => turma.Id == id);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Turma>> GetTurmas()
        {
            try
            {
                return await _context.Turmas.ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Turma>> GetTurmasFiltro(string? pesquisar)
        {
            try
            {
                var turma = await _context.Turmas.Where(turmaBanco => turmaBanco.Nome.Contains(pesquisar)).ToListAsync();
                return turma;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Turma> RemoverTurma(int id)
        {
            try
            {
                var turma = await _context.Turmas.FirstOrDefaultAsync(turmaDB => turmaDB.Id == id);

                _context.Remove(turma);
                await _context.SaveChangesAsync();

                return turma;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
