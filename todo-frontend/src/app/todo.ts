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

  completeTodo(id: number) {
    const url = `${this.apiUrl}/${id}`;
    console.log('URL:', url);
    this.http.put<Todo>(`${this.apiUrl}/${id}`, {}).subscribe(() => {
      this.loadTodos();
    });
  }

  deleteTodo(id: number) {
    this.http.delete<Todo>(`${this.apiUrl}/${id}`, {}).subscribe(() => {
      this.loadTodos();
    });
  }
}

