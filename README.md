# Todo App: A Journey of Unnecessary Complexity

Because a paper and pen was simply not good enough.

## What Is This?

A todo application built with a .NET backend and Angular frontend.

## Tech Stack

    - Backend: .NET - because C# is just Java but it went to therapy
    - Frontend: Angular - because plain HTML was too straightforward
    - Database: A List<Todo> in memory - gone when you restart,
    just like your motivation on a Monday morning

## Features

    - Add todos you will never complete
    - Complete todos (theoretically)
    - Delete todos and pretend they never existed
    - Survive a full HTTP round trip without crying

## Getting Started

### Prerequisites

    - .NET SDK (and the will to live)
    - Node.js & Angular CLI
    - A todo list of things to do that isn't this app

### Running the Backend

```bash
cd TodoApi
dotnet run
```

### Running the frontend

```bash
cd todo-frontend
ng serve
```

## API Endpoint

| Method    | Endpoint     |  Description   |
| ------- | ------------ | ------- |
| GET | /todos |  Get all todos (spoiler: it's empty)  |
| POST | /todos |  Add a todo you won't complete  |
| PUT | /todos{id} |  Pretend you did something  |
| DELETE | /todos{id} |  Revisionist history  |
