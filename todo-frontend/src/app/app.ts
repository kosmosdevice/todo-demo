import { Component, inject, OnInit } from '@angular/core';
import { TodoService } from './todo';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  imports: [FormsModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  todoService = inject(TodoService);
  newTodoTitle = '';

  ngOnInit() {
    this.todoService.loadTodos();
  }

  addTodo() {
    if (this.newTodoTitle.trim()) {
      this.todoService.addTodo(this.newTodoTitle);
      this.newTodoTitle = '';
    }
  }
}

