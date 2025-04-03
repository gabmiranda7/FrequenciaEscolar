using FrequenciaEscolar.Data;
using FrequenciaEscolar.Dto;
using FrequenciaEscolar.Models;
using Microsoft.EntityFrameworkCore;

namespace FrequenciaEscolar.Services.FrequenciaEscolar
{
    public class AlunoService : IAlunoInterface
    {
        private readonly AppDbContext _context;
        private readonly string _sistema;
        public AlunoService(AppDbContext context, IWebHostEnvironment sistema)
        {
            _context = context;
            _sistema = sistema.WebRootPath;
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
                var alunoBanco = await _context.Alunos.AsNoTracking().FirstOrDefaultAsync(alunoDB => alunoDB.Id == aluno.Id);


                alunoBanco.Nome = aluno.Nome;
                alunoBanco.Matricula = aluno.Matricula;

                _context.Update(alunoBanco);
                await _context.SaveChangesAsync();

                return aluno;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
