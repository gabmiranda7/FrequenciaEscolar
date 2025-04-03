namespace FrequenciaEscolar.Models
{
    public class Turma
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Ano { get; set; }
        public int ProfessorId { get; set; }
        public required Professor Professor { get; set; }

        public ICollection<AlunoTurma> AlunosTurmas { get; set; } = new List<AlunoTurma>();

    }

}
