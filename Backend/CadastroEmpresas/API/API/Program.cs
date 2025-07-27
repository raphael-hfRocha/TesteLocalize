using System.Text;
using Application.Services;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using static Security.JWT.JwtConfig;
using Security.JWT;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<TokenService>();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
var confsJWT = new JWT();
builder.Configuration.GetSection("JWT").Bind(confsJWT);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = confsJWT.ValidAudience,
        ValidIssuer = confsJWT.ValidIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(confsJWT.Secret)),
    };
});

builder.Services.AddAuthorization();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "CadastroEmpresas API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header usando o esquema Bearer. Exemplo: 'Bearer {token}'",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var connectionString = builder.Configuration.GetConnectionString("ApplicationDbConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("A string de conexão 'ApplicationDbConnection' não foi encontrada em appsettings.json.");
}
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("ApplicationDbConnection"),
    new MySqlServerVersion(new Version(8, 0, 25))));

builder.Services.AddHttpClient();

// Registre seus repositórios e serviços para injeção de dependência
builder.Services.AddScoped<EmpresaRepository>();
builder.Services.AddScoped<EmpresaService>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<UsuarioService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

///*app.MapGet("/", (TokenService service)
//    => service.Generate(null));*/

app.MapPost("/api/auth/token", (TokenService service) =>
{
    // Para testes, retorna um token sem validar usuário
    return service.Generate(null);
}).AllowAnonymous();

app.Run();
