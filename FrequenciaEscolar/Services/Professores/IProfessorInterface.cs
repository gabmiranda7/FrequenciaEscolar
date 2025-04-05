using FrequenciaEscolar.Dto;
using FrequenciaEscolar.Models;

namespace FrequenciaEscolar.Services.Professores
{
    public interface IProfessorInterface
    {
        Task<Prof> CriarProfessor(ProfessorCriacaoDto professorCriacaoDto);
        Task<List<Prof>> GetProfessores();
        Task<Prof> GetProfessorPorId(int id);
        Task<Prof> EditarProfessor(Prof professor);
        Task<Prof> RemoverProfessor(int id);
        Task<List<Prof>> GetProfessoresFiltro(string? pesquisar);
    }
}

