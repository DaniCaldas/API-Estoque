using API_Estoque.Controllers;
using API_Estoque.Data;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Logging;
using API_Estoque.JWT;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http//*" + Environment.GetEnvironmentVariable("PORT") ?? "5000");
builder.Logging.AddConsole();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Database>();
builder.Services.AddTransient<Database>();
builder.Services.AddSingleton<JwtService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                         .AllowAnyHeader()
                         .AllowAnyMethod());
});
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddTransient<JwtBearerEventsHandler>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],

            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,

            RequireExpirationTime = true,
            RequireSignedTokens = true
        };

        var loggerFactory = builder.Services.BuildServiceProvider()
            .GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger<Program>();

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                logger.LogError(context.Exception, "Falha na autenticação JWT");
                return Task.CompletedTask;
            }
        };
    });

var app = builder.Build();

app.UseCors("AllowSpecificOrigin");

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.EndPointProdutos();

app.EndPointCategorias();

app.EndPointMovimentacoes();

app.EndPointUsuarios();

app.FiltroPesquisa();

app.FiltroPesquisaData();

app.FiltroProdutos();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
