using FrequenciaEscolar.Dto;
using FrequenciaEscolar.Models;

public interface IFrequenciaInterface
{
    Task<Frequencia> Criar(FrequenciaCriacaoDto dto);
    Task<Frequencia> Editar(Frequencia dto);
    Task<Frequencia?> ObterPorId(int id);
    Task<List<Frequencia>> ObterTodos();
    Task<List<Frequencia>> ObterPorAluno(int alunoId);
    Task<List<Frequencia>> ObterPorTurma(int turmaId);
    Task Atualizar(int id, FrequenciaCriacaoDto frequenciaDto);
    Task<bool> Remover(int id);
}
