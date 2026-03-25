using Azure;
using Azure.AI.ContentSafety;
using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Repositories;
using EventPlus.WebAPI.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

//1. Configurar o Contexto do Banco de Dados 
builder.Services.AddDbContext<EventContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//2. Registrar as Repositories (Injeção de Dependência)
builder.Services.AddScoped<ITipoEventoRepository, TipoEventoRepository>();
builder.Services.AddScoped<ITipoUsuarioRepository, TipoUsuarioRepository>();
builder.Services.AddScoped<IInstituicaoRepository, InstituicaoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEventoRepository, EventoRepository>();
builder.Services.AddScoped<IComentarioEventoRepository, ComentarioEventoRepository>();
builder.Services.AddScoped<IPresencaRepository, PresencaRepository>();

//Configuração do Azure Content Safety
var endpoint = "https://moderatorservice-marcos.cognitiveservices.azure.com";
var apikey = "";

if (!string.IsNullOrWhiteSpace(apikey))
{
    var client = new ContentSafetyClient(new Uri(endpoint), new Azure.AzureKeyCredential(apikey));
    builder.Services.AddSingleton(client);
}

//Adiciona Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Version = "v1",
        Title = "Api de Eventos",
        Description = "Aplicação para gerenciamento de eventos",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        { 
            Name = "Felipe Salgado",
            Url = new Uri("https://github.com/FelipeSomeraSalgado")
        },
        License = new OpenApiLicense
        {
            Name = "Licensa de exemplo",
            Url = new Uri("https://example.com/license")
        }
        });

    //Usando a autenticação no Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Bota o token ai zé:"
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = Array.Empty<string>().ToList()
    });

});


builder.Services.AddControllers();

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
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("event-chave-autenticacao-webapi-dev")), //forma de criptografia e valida a chave de autenticação
            ClockSkew = TimeSpan.FromMinutes(5), //Valida o tempo de expiração do token
            ValidIssuer = "api_event", //nome do issuer (de onte está vindo)
            ValidAudience = "api_event" //nome do audience (para onde está indo)
        };
    });
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger(options => { });

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty; // Define a raiz para acessar o Swagger UI
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();

