using FrequenciaEscolar.Dto;
using FrequenciaEscolar.Models;

namespace FrequenciaEscolar.Services.Professor
{
    public interface IProfessorInterface
    {
        Task<Professor> CriarProfessor(ProfessorCriacaoDto professorCriacaoDto);
        Task<List<Professor>> GetProfessores();
        Task<Professor> GetProfessorPorId(int id);
        Task<Professor> EditarProfessor(Professor professor);
        Task<Professor> RemoverProfessor(int id);
        Task<List<Professor>> GetProfessoresFiltro(string? pesquisar);
    }
}

