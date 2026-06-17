
using LivrosBD.Interfaces;
using LivrosBD.LivrosBDConnect;
using LivrosBD.Repositories;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o contexto do banco de dados (exemplo com SQL Server)
builder.Services.AddDbContext<LivrosBDContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adiciona o repositório ao container de injeção de dependência
builder.Services.AddScoped<ILivrosRepository, LivrosRepositories>();
builder.Services.AddScoped<IGeneroRepository, GeneroRepositories>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepositories>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";
})

.AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        //valida quem está solicitando
        ValidateIssuer = true,

        //valida quem está recebendo
        ValidateAudience = true,

        //define se o tempo de expiração será validado
        ValidateLifetime = true,

        //forma de criptografia e valida a chave de autenticação
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("livros-webapi-chave-autenticacao-ef")),

        //valida o tempo de expiração do token
        ClockSkew = TimeSpan.FromMinutes(5),

        //nome do issuer (de onde está vindo)
        ValidIssuer = "api_livros",

        //nome do audience (para onde está indo)
        ValidAudience = "api_livros"
    };
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Livros API",
        Description = "Uma API com um catálogo de livros",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Estante-Digital",
            Url = new Uri("https://github.com/marcaumdev")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
});

// Adiciona serviço de Controllers
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseAuthentication();

app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options => { });

    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseCors("CorsPolicy");

// Adicione mapeamento de Controllers
app.MapControllers();

app.Run();