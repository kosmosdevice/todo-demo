import { Component, inject, OnInit } from '@angular/core';
import { TodoService } from './todo';
import { TodoItem } from './todo-item/todo-item';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  imports: [FormsModule, TodoItem],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  todoService = inject(TodoService);
  newTodoTitle = '';
  errorMessage = '';

  ngOnInit() {
    this.todoService.loadTodos();
  }

  addTodo() {
    if (this.newTodoTitle.trim()) {
      this.todoService.addTodo(this.newTodoTitle);
      this.newTodoTitle = '';
      this.errorMessage = '';
    }
    else {
      this.errorMessage = "Title required";
    }
  }
}
