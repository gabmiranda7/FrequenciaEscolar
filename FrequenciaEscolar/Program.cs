using FrequenciaEscolar.Data;
using Microsoft.EntityFrameworkCore;
using FrequenciaEscolar.Services.Alunos;
using FrequenciaEscolar.Services.Frequencias;
using FrequenciaEscolar.Services.Turmas;
using FrequenciaEscolar.Services.Professores;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllersWithViews();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IAlunoInterface, AlunoService>();
builder.Services.AddScoped<IFrequenciaInterface, FrequenciaService>();
builder.Services.AddScoped<IProfessorInterface, ProfessorService>();
builder.Services.AddScoped<ITurmaInterface, TurmaService>();

var app = builder.Build();

// Middlewares
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// Serve os arquivos do React (precisa estar ANTES do routing!)
app.UseDefaultFiles(); // Lê o index.html
app.UseStaticFiles();  // Lê os arquivos em wwwroot/app

app.UseRouting();
app.UseAuthorization();

// Redireciona tudo que não é API para o React
app.MapFallbackToController("Index", "App");

// APIs ainda podem ser mapeadas normalmente
app.MapControllers();

app.Run();
