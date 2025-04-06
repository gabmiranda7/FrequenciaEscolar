using FrequenciaEscolar.Models;
using System.Collections.Generic;

namespace FrequenciaEscolar.ViewModel
{
    public class PesquisaViewModel
    {
        public string Termo { get; set; }
        public List<Aluno> Alunos { get; set; }
        public List<Prof> Professores { get; set; }
        public List<Turma> Turmas { get; set; }
    }
}
