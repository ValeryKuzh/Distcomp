using BlogService.Application.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Регистрация приложения
builder.Services.AddApplication<long>();

// Регистрация контроллеров
builder.Services.AddControllers();

builder.Services.AddHttpClient("CommentsClient", client =>
{
    client.BaseAddress = new Uri("http://localhost:24130/");
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BlogService API",
        Version = "1.0",
        Description = "Микросервис для работы с блогом"
    });
});

var app = builder.Build();

// Настройка Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlogService API V1");
        c.RoutePrefix = string.Empty;
    });
}

if (app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
    app.UseAuthorization();
}
        
app.MapControllers();

app.Run();