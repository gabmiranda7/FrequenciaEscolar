using FrequenciaEscolar.Dto;
using FrequenciaEscolar.Models;

namespace FrequenciaEscolar.Services.Turmas
{
    public interface ITurmaInterface
    {
        Task<Turma> CriarTurma(TurmaCriacaoDto turmaCriacaoDto);
        Task<List<Turma>> GetTurmas();
        Task<Turma> GetTurmaPorId(int id);
        Task<Turma> EditarTurma(Turma turma);
        Task<Turma> RemoverTurma(int id);
        Task<List<Turma>> GetTurmasFiltro(string? pesquisar);
    }
}
