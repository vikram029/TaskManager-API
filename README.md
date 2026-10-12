# Task Manager API

A simple REST API for managing tasks, built with **ASP.NET Core 8 Web API**, **Entity Framework Core** and **SQL Server**. Includes Swagger UI for testing.

## Features
- Full CRUD for tasks (create, read, update, delete)
- Filter tasks by completion status (`?completed=true`)
- Input validation with data annotations
- Separate input model so clients cannot set `Id` or `CreatedAt`
- Swagger / OpenAPI documentation

## Tech Stack
C# · ASP.NET Core 8 Web API · Entity Framework Core 8 · SQL Server (LocalDB) · Swagger

## Endpoints
| Method | URL | Description |
|--------|-----|-------------|
| GET | `/api/tasks` | List tasks (optional `?completed=true/false`) |
| GET | `/api/tasks/{id}` | Get one task |
| POST | `/api/tasks` | Create a task |
| PUT | `/api/tasks/{id}` | Update a task |
| DELETE | `/api/tasks/{id}` | Delete a task |

Example request body for POST/PUT:
```json
{
  "title": "Finish API documentation",
  "description": "Add examples to the README",
  "isCompleted": false,
  "dueDate": "2026-11-01T00:00:00"
}
```

## How to Run
1. Install the [.NET 8 SDK](https://dotnet.microsoft.com/download) and SQL Server (or LocalDB, which comes with Visual Studio).
2. Clone the repo and open the folder.
3. Check the connection string in `appsettings.json`.
4. Run:
   ```bash
   dotnet run
   ```
5. Open the Swagger URL shown in the console (for example `https://localhost:7xxx/swagger`).

The database and table are created automatically on first run.

## Possible Improvements
- Switch from `EnsureCreated` to EF Core migrations
- Add pagination and sorting
- Add authentication (see the JWT Auth API project)
- Add unit tests with xUnit
