using Microsoft.EntityFrameworkCore;
using prova_surpresa_mvc.Models;
using prova_surpresa_mvc.Services;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurando o DbContext para usar MySQL
builder.Services.AddDbContext<ContextoEscola>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection),
        new MySqlServerVersion(new Version(8, 0, 39)) // Substitua pela versão do seu MySQL
    ));

// Injeção de Dependências
builder.Services.AddScoped(typeof(IRepositorio<>), typeof(Repositorio<>));
builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<ICursoService, CursoService>();
builder.Services.AddScoped<IProfessorService, ProfessorService>();
builder.Services.AddScoped<IMatriculaService, MatriculaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
