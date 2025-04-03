using FrequenciaEscolar.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrequenciaEscolar.Models
{
    public class AlunoTurma
    {
        public int AlunoId { get; set; }
        public required Aluno Aluno { get; set; }

        public int TurmaId { get; set; }
        public required Turma Turma { get; set; }
    }
}
