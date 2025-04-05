namespace FrequenciaEscolar.Models
{
    public class Turma
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Ano { get; set; }
        public int ProfessorId { get; set; }
        public required Prof Professor { get; set; }

        // Uma turma pode ter vários alunos
        public ICollection<AlunoTurma> AlunosTurmas { get; set; } = new List<AlunoTurma>();

        // Uma turma pode ter várias frequências
        public List<Frequencia> Frequencias { get; set; } = new List<Frequencia>();


    }

}
