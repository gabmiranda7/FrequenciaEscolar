namespace FrequenciaEscolar.Models
{
    public class Frequencia
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public required Aluno Aluno { get; set; }  // <- Propriedade de navegação

        public int TurmaId { get; set; }
        public required Turma Turma { get; set; }  // <- Propriedade de navegação

        public DateTime Data { get; set; }
        public bool Presente { get; set; }
    }

}
