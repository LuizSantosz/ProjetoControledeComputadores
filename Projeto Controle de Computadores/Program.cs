using Projeto_Controle_de_Computadores.Services;
using Projeto_Controle_de_Computadores.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuração de MonitorOptions
builder.Services.Configure<MonitorOptions>(builder.Configuration.GetSection("MonitorOptions"));

// Adiciona suporte a Controllers com Views
builder.Services.AddControllersWithViews();

// Adiciona os serviços de GrupoService e ComputadorService
builder.Services.AddSingleton<GrupoService>();
builder.Services.AddSingleton<ComputadorService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Mapeamento de rotas padrão para Controller e Action
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Cadastro}/{action=ListarGrupos}/{id?}");

app.Run();
