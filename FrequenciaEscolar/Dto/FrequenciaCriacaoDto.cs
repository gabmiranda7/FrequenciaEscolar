using FrequenciaEscolar.Models;

namespace FrequenciaEscolar.Dto
{
    public class FrequenciaCriacaoDto
    {
        public int AlunoId { get; set; }
        public int TurmaId { get; set; }

        public DateTime Data { get; set; }
        public bool Presente { get; set; }
    }
}
