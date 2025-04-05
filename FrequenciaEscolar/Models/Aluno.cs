namespace FrequenciaEscolar.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Matricula { get; set; }

        // Um aluno pode estar em várias turmas
        public ICollection<AlunoTurma> AlunosTurmas { get; set; } = new List<AlunoTurma>();

        // Um aluno pode ter várias frequências
        public List<Frequencia> Frequencias { get; set; } = new List<Frequencia>();

    }
}
