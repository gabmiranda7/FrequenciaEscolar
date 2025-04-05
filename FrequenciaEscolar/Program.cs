using FrequenciaEscolar.Data;
using Microsoft.EntityFrameworkCore;
using FrequenciaEscolar.Services.Alunos;
using System;
using FrequenciaEscolar.Services.Frequencias;
using FrequenciaEscolar.Services.Turmas;
using FrequenciaEscolar.Services.Professores;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IAlunoInterface, AlunoService>();
builder.Services.AddScoped<IFrequenciaInterface, FrequenciaService>();
builder.Services.AddScoped<IProfessorInterface, ProfessorService>();
builder.Services.AddScoped<ITurmaInterface, TurmaService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
