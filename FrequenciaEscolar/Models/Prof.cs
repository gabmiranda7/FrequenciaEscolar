namespace FrequenciaEscolar.Models
{
    public class Prof
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Disciplina { get; set; } = string.Empty;

        // Um professor pode ter várias turmas
        public required List<Turma> Turmas { get; set; } = new List<Turma>();
    }

}
