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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

// In-memory storage
var todos = new List<Todo>();

app.MapGet("/todos", () => todos);

// Endpoints go here
app.MapPost("/todos", (string title) =>
{
    var todo = new Todo(todos.Count + 1, title, false);
    todos.Add(todo);
    return Results.Created($"/todos/{todo.Id}", todo);
});

app.MapPut("/todos/{id}", (int id) =>
{
    var todo = todos.FirstOrDefault(t => t.Id == id);
    if (todo is null) return Results.NotFound();

    todos.Remove(todo);
    todos.Add(todo with { IsCompleted = true });
    return Results.Ok(todo);
});

app.MapDelete("/todos/{id}", (int id) =>
{
    var todo = todos.FirstOrDefault(t => t.Id == id);
    if (todo is null) return Results.NotFound();
    todos.Remove(todo);
    return Results.NoContent();
});

app.Run();

record Todo(int Id, string Title, bool IsCompleted);
