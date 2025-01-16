using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using Microsoft.Extensions.Hosting;
using Projeto_Controle_de_Computadores.Services;
using System.Collections.Generic;

public class Startup {
    public Startup(IConfiguration configuration) {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services) {
        services.Configure<MonitorOptions>(Configuration.GetSection("MonitorOptions"));
        services.AddSingleton<GrupoService>();
        services.AddSingleton<ComputadorService>();
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
        if (env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
        } else {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { 
            endpoints.MapControllers(); 
            endpoints.MapControllerRoute(
                name: "default", 
                pattern: "{controller=Cadastro}/{action=ListarGrupos}/{id?}");
        });
    }
}

public class MonitorOptions {
    public List<string> Computadores { get; set; }
}
