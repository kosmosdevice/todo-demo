import { Component, input, output } from '@angular/core';
import { Todo } from '../todo';

@Component({
  selector: 'app-todo-item',
  imports: [],
  templateUrl: './todo-item.html',
  styleUrl: './todo-item.css',
})

export class TodoItem {
  todo = input.required<Todo>();
  complete = output<Todo>();
  delete = output<number>();
}
