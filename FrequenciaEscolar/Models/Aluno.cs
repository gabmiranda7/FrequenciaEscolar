namespace FrequenciaEscolar.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Matricula { get; set; }

        public ICollection<Frequencia> Frequencias { get; set; } = new List<Frequencia>();
        public ICollection<AlunoTurma> AlunosTurmas { get; set; } = new List<AlunoTurma>();

    }

}
