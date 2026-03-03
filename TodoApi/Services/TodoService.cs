using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Services;

public class TodoService : ITodoService
{
    private readonly TodoDbContext _db;

    public TodoService(TodoDbContext db)
    {
        _db = db;
    }

    public List<Todo> GetAll()
    {
        return _db.Todos.ToList();
    }

    public Todo? GetById(int id)
    {
        return _db.Todos.Find(id);
    }

    public Todo Create(string title)
    {
        var todo = new Todo { Title = title, IsCompleted = false };
        _db.Todos.Add(todo);
        _db.SaveChanges();
        return todo;
    }

    public Todo? Update(int id, Todo updatedTodo)
    {
        var todo = _db.Todos.Find(id);
        if (todo is null) return null;

        todo.Title = updatedTodo.Title;
        todo.IsCompleted = updatedTodo.IsCompleted;
        _db.SaveChanges();
        return todo;
    }

    public bool Delete(int id)
    {
        var todo = _db.Todos.Find(id);
        if (todo is null) return false;

        _db.Todos.Remove(todo);
        _db.SaveChanges();
        return true;
    }
}
