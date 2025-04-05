using FrequenciaEscolar.Data;
using FrequenciaEscolar.Dto;
using FrequenciaEscolar.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrequenciaEscolar.Services.Frequencias
{
    public class FrequenciaService : IFrequenciaInterface
    {
        private readonly AppDbContext _context;

        public FrequenciaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Frequencia> Criar(FrequenciaCriacaoDto dto)
        {
            var frequencia = new Frequencia
            {
                AlunoId = dto.AlunoId,
                TurmaId = dto.TurmaId,
                Data = dto.Data,
                Presente = dto.Presente,
                Aluno = null!,
                Turma = null!
            };

            _context.Frequencias.Add(frequencia);
            await _context.SaveChangesAsync();
            return frequencia;
        }

        public async Task<Frequencia> Editar(Frequencia frequencia)
        {
            _context.Frequencias.Update(frequencia);
            await _context.SaveChangesAsync();
            return frequencia;
        }

        public async Task<Frequencia?> ObterPorId(int id)
        {
            return await _context.Frequencias
                .Include(f => f.Aluno)
                .Include(f => f.Turma)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<List<Frequencia>> ObterTodos()
        {
            return await _context.Frequencias
                .Include(f => f.Aluno)
                .Include(f => f.Turma)
                .ToListAsync();
        }

        public async Task<List<Frequencia>> ObterPorAluno(int alunoId)
        {
            return await _context.Frequencias
                .Where(f => f.AlunoId == alunoId)
                .Include(f => f.Turma)
                .ToListAsync();
        }

        public async Task<List<Frequencia>> ObterPorTurma(int turmaId)
        {
            return await _context.Frequencias
                .Where(f => f.TurmaId == turmaId)
                .Include(f => f.Aluno)
                .ToListAsync();
        }

        public async Task Atualizar(int id, FrequenciaCriacaoDto frequenciaDto)
        {
            var frequencia = await _context.Frequencias.FindAsync(id);
            if (frequencia == null)
                throw new Exception("Frequência não encontrada.");

            frequencia.Data = frequenciaDto.Data;
            frequencia.Presente = frequenciaDto.Presente;
            frequencia.TurmaId = frequenciaDto.TurmaId;
            frequencia.AlunoId = frequenciaDto.AlunoId;

            _context.Frequencias.Update(frequencia);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Remover(int id)
        {
            var frequencia = await _context.Frequencias.FirstOrDefaultAsync(f => f.Id == id);
            if (frequencia == null) return false;

            _context.Frequencias.Remove(frequencia);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
