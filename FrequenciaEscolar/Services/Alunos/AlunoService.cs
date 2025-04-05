using FrequenciaEscolar.Data;
using FrequenciaEscolar.Dto;
using FrequenciaEscolar.Models;
using FrequenciaEscolar.Services.Alunos;
using Microsoft.EntityFrameworkCore;

namespace FrequenciaEscolar.Services.Alunos
{
    public class AlunoService : IAlunoInterface
    {
        private readonly AppDbContext _context;
        public AlunoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Aluno> CriarAluno(AlunoCriacaoDto alunoCriacaoDto)
        {
            try
            {
                var aluno = new Aluno
                {
                    Nome = alunoCriacaoDto.Nome,
                    Matricula = alunoCriacaoDto.Matricula,
                };

                _context.Add(aluno);
                await _context.SaveChangesAsync();

                return aluno;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Aluno> EditarAluno(Aluno aluno)
        {
            try
            {
                var alunoBanco = await _context.Alunos.FirstOrDefaultAsync(alunoDB => alunoDB.Id == aluno.Id);

                if (alunoBanco == null)
                {
                    throw new Exception("Aluno não encontrado.");
                }

                alunoBanco.Nome = aluno.Nome;
                alunoBanco.Matricula = aluno.Matricula;

                _context.Update(alunoBanco);
                await _context.SaveChangesAsync();

                return alunoBanco; // Retornar a entidade atualizada
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao editar aluno: " + ex.Message);
            }
        }
        public async Task<IEnumerable<Aluno>> GetAlunosPaginados(int pageNumber, int pageSize)
        {
            return await _context.Alunos
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalAlunos()
        {
            return await _context.Alunos.CountAsync();
        }


        public async Task<Aluno> GetAlunoPorId(int id)
        {
            try
            {

                return await _context.Alunos.FirstOrDefaultAsync(aluno => aluno.Id == id);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Aluno>> GetAlunos()
        {
            try
            {
                return await _context.Alunos.ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Aluno>> GetAlunosFiltro(string? pesquisar)
        {
            try
            {
                var alunos = await _context.Alunos.Where(alunoBanco => alunoBanco.Nome.Contains(pesquisar)).ToListAsync();
                return alunos;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Aluno> RemoverAluno(int id)
        {
            try
            {
                var aluno = await _context.Alunos.FirstOrDefaultAsync(alunoDB => alunoDB.Id == id);

                _context.Remove(aluno);
                await _context.SaveChangesAsync();

                return aluno;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
