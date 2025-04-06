using FrequenciaEscolar.Models;

namespace FrequenciaEscolar.ViewModel
{
    public class AlunoDetalhesViewModel
    {
        public int AlunoId { get; set; }
        public string Nome { get; set; }
        public int Matricula { get; set; }

        public List<Frequencia> Frequencias { get; set; }
    }

}
