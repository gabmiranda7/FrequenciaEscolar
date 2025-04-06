using System.Diagnostics;
using FrequenciaEscolar.Data;
using FrequenciaEscolar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FrequenciaEscolar.ViewModel;


public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }
    public IActionResult Index(string termo)
    {
        var viewModel = new PesquisaViewModel
        {
            Termo = termo,
            Alunos = string.IsNullOrEmpty(termo) ? new List<Aluno>() :
                     _context.Alunos.Where(a => a.Nome.Contains(termo)).ToList(),

            Professores = string.IsNullOrEmpty(termo) ? new List<Prof>() :
                          _context.Professores.Where(p => p.Nome.Contains(termo)).ToList(),

            Turmas = string.IsNullOrEmpty(termo) ? new List<Turma>() :
                     _context.Turmas.Where(t => t.Nome.Contains(termo)).ToList()
        };

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Pesquisar(string termo)
    {
        if (string.IsNullOrWhiteSpace(termo))
        {
            return RedirectToAction("Index");
        }

        var alunos = await _context.Alunos
            .Where(a => a.Nome.Contains(termo))
            .ToListAsync();

        var professores = await _context.Professores
            .Where(p => p.Nome.Contains(termo))
            .ToListAsync();

        var turmas = await _context.Turmas
            .Where(t => t.Nome.Contains(termo) || t.Ano.ToString().Contains(termo))
            .Include(t => t.Professor)
            .ToListAsync();

        var resultado = new PesquisaViewModel
        {
            Termo = termo,
            Alunos = alunos,
            Professores = professores,
            Turmas = turmas
        };

        return View("Resultado", resultado);
    }
}
