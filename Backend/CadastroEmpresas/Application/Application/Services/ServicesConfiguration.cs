using CadastroEmpresas.src.Security.JWT;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services
{
    public static class ServicesConfiguration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            //services.AddScoped<IUsuarioService, UsuarioService>();
            //services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<JwtService>();
            services.AddTransient<EmpresaService>();
            //services.AddScoped<EmpresaService>();
            //services.AddScoped<ReceitaWsService>();
            //services.AddScoped<EmpresaRepository>();
            //services.AddScoped<UsuarioRepository>();
            //services.AddScoped<EmpresaService>();
            //services.AddAutoMapper(typeof(MapperProfile));
        }
    }
}
