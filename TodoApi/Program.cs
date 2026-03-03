using TodoApi.Models;
using TodoApi.Data;
using TodoApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddDbContext<TodoDbContext>(options =>
{
    options.UseSqlite("Data Source=todos.db");
});
builder.Services.AddScoped<ITodoService, TodoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.MapGet("/todos", (ITodoService service) => service.GetAll());

app.MapPost("/todos", (ITodoService service, string title) =>
{
    if (string.IsNullOrWhiteSpace(title))
        return Results.BadRequest("Title is required");
    var todo = service.Create(title);
    return Results.Created($"/todos/{todo.Id}", todo);
});

app.MapPut("/todos/{id}", (ITodoService service, int id, Todo updatedTodo) =>
{
    var updated = service.Update(id, updatedTodo);
    return updated is null ? Results.NotFound() : Results.Ok(updated);
});

app.MapDelete("/todos/{id}", (ITodoService service, int id) =>
{
    if (!service.Delete(id))
        return Results.NotFound();
    return Results.NoContent();
});

app.Run();
