using FrequenciaEscolar.Dto;
using FrequenciaEscolar.Models;

namespace FrequenciaEscolar.Services.Alunos
{
    public interface IAlunoInterface
    {
        Task<Aluno> CriarAluno(AlunoCriacaoDto alunoCriacaoDto);
        Task<List<Aluno>> GetAlunos();
        Task<Aluno> GetAlunoPorId(int id);
        Task<Aluno> EditarAluno(Aluno aluno);
        Task<Aluno> RemoverAluno(int id);
        Task<List<Aluno>> GetAlunosFiltro(string? pesquisar);
    }
}
