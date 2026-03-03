import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';

export interface Todo {
  id: number;
  title: string;
  isCompleted: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class TodoService {
  private apiUrl = 'http://localhost:5052/todos';
  todos = signal<Todo[]>([]);

  constructor(private http: HttpClient) { }

  loadTodos() {
    this.http.get<Todo[]>(this.apiUrl).subscribe(todos => {
      this.todos.set(todos);
    });
  }

  addTodo(title: string) {
    this.http.post<Todo>(`${this.apiUrl}?title=${title}`, {}).subscribe(() => {
      this.loadTodos();
    });
  }

  updateTodo(todo: Todo) {
    const url = `${this.apiUrl}/${todo.id}`;
    console.log('URL:', url);
    const updatedTodo = { ...todo, isCompleted: !todo.isCompleted };
    this.http.put<Todo>(`${this.apiUrl}/${todo.id}`, updatedTodo).subscribe(() => {
      this.loadTodos();
    });
  }

  deleteTodo(id: number) {
    const currentTodos = this.todos();
    const todoToDelete = currentTodos.find(t => t.id == id);

    this.todos.update( todos => todos.filter(t => t.id != id));

    this.http.delete<Todo>(`${this.apiUrl}/${id}`, {}).subscribe({
      error: (err) => {
        if (todoToDelete) {
          this.todos.update(todos => [...todos, todoToDelete]);
          console.error('Delete failed, rolling back', err);
        }
      }
    });
  }
}

