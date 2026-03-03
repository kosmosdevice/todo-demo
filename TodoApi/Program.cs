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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.MapGet("/todos", (TodoDbContext db) => db.Todos.ToList());

app.MapPost("/todos", (TodoDbContext db, string title) =>
{
    var todo = new Todo{ Title = title, IsCompleted = false };
    db.Todos.Add(todo);
    db.SaveChanges();
    return Results.Created($"/todos/{todo.Id}", todo);
});

app.MapPut("/todos/{id}", (TodoDbContext db, int id) =>
{
    var todo = db.Todos.FirstOrDefault(t => t.Id == id);
    if (todo is null) return Results.NotFound();

    todo.IsCompleted=true;
    db.SaveChanges();
    return Results.Ok(todo);
});

app.MapDelete("/todos/{id}", (TodoDbContext db, int id) =>
{
    var todo = db.Todos.FirstOrDefault(t => t.Id == id);
    if (todo is null) return Results.NotFound();
    db.Todos.Remove(todo);
    db.SaveChanges();
    return Results.NoContent();
});

app.Run();

public class TodoDbContext : DbContext
{
    public TodoDbContext( DbContextOptions<TodoDbContext> options) : base(options) {}
    public required DbSet<Todo> Todos {get; set;}
}

public class Todo
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}
