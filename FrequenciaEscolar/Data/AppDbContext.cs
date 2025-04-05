using FrequenciaEscolar.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FrequenciaEscolar.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Frequencia> Frequencias { get; set; }
        public DbSet<Prof> Professores { get; set; }
        public DbSet<Turma> Turmas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlunoTurma>()
                .HasKey(at => new { at.AlunoId, at.TurmaId });

            modelBuilder.Entity<AlunoTurma>()
                .HasOne(at => at.Aluno)
                .WithMany(a => a.AlunosTurmas)
                .HasForeignKey(at => at.AlunoId);

            modelBuilder.Entity<AlunoTurma>()
                .HasOne(at => at.Turma)
                .WithMany(t => t.AlunosTurmas)
                .HasForeignKey(at => at.TurmaId);

            modelBuilder.Entity<Frequencia>()
                .HasOne(f => f.Aluno)
                .WithMany(a => a.Frequencias)
                .HasForeignKey(f => f.AlunoId);

            modelBuilder.Entity<Frequencia>()
                .HasOne(f => f.Turma)
                .WithMany(t => t.Frequencias)
                .HasForeignKey(f => f.TurmaId);

            modelBuilder.Entity<Turma>()
                .HasOne(t => t.Professor)
                .WithMany(p => p.Turmas)
                .HasForeignKey(t => t.ProfessorId);

        }
    }
}
