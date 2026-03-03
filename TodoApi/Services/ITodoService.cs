using TodoApi.Models;

namespace TodoApi.Services;

public interface ITodoService
{
    List<Todo> GetAll();
    Todo? GetById(int id);
    Todo Create(string title);
    Todo? Update(int id, Todo updatedTodo);
    bool Delete(int id);
}
