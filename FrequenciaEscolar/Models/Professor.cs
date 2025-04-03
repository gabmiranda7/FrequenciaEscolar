namespace FrequenciaEscolar.Models
{
    public class Professor
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Disciplina { get; set; } = string.Empty;

        // Um professor pode ter várias turmas
        public required ICollection<Turma> Turmas { get; set; }
    }

}
