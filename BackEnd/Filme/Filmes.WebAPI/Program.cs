using Filmes.WebAPI.BdContextFilme;
using Filmes.WebAPI.Interfaces;
using Filmes.WebAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o contexto do banco de dados (exemplo SQL Server)
builder.Services.AddDbContext<FilmeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Adiciona o repositório ao container de injeção de dependência
builder.Services.AddScoped<IFilmeRepository, FilmeRepository>();
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

//adiciona serviço de autenticação JWT Bearer (forma de autenticação)
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";
})
    .AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true, //Valida quem está solicitando o token
        ValidateAudience = true, //Valida quem está recebendo o token
        ValidateLifetime = true, //define se o tempo de expiração do token deve ser validado
        IssuerSigningKey = new SymmetricSecurityKey (System.Text.Encoding.UTF8.GetBytes("filmes-chave-autenticacao-webapi-dev")), //forma de criptografia e valida a chave de autenticação
        ClockSkew = TimeSpan.FromMinutes(5), //Valida o tempo de expiração do token
        ValidIssuer = "api_filmes", //nome do issuer (de onte está vindo)
        ValidAudience = "api_filmes" //nome do audience (para onde está indo)
    };
});

   builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
options.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
{
    Version = "v1",
    Title = "Filmes API",
    Description = "Uma API com um catálogo de filmes",
    TermsOfService = new Uri("https://example.com/terms"),
    Contact = new Microsoft.OpenApi.OpenApiContact
    {
        Name = "felipedev",
        Url = new Uri("https://exemple.com/felipedev")
    },
    License = new Microsoft.OpenApi.OpenApiLicense
    {
        Name = "Exemple License",
        Url = new Uri("https://example.com/license")
    }

});

options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    Scheme = "Bearer",
    In = ParameterLocation.Header,
    Description = "Insira o Token JWT:"
});

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = Array.Empty<string>().ToList()
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Adiciona serviço de Controllers
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options => { });

    app.UseSwaggerUI(options => 
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}
app.UseCors("CorsPolicy");

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

// Adiciona o mapeamento de Controllers
app.MapControllers();

app.Run();
