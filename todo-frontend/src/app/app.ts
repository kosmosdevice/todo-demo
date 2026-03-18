import { Component, inject, OnInit, computed, signal } from '@angular/core';
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

  filter = signal<'all' | 'active' | 'completed'>('all');
  filteredTodos = computed(() => {
    const todos = this.todoService.todos();
    const currentFilter = this.filter();
    switch (currentFilter) {
      case 'active':
        return todos.filter(t => t.isCompleted == false);
      case 'completed':
        return todos.filter(t => t.isCompleted == true);
      default:
        return todos;
    }
  });
}
