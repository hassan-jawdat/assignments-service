# Assignments Service

ASP.NET Core Web API for managing student assignments in the Shiko LMS.

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- SQL Server LocalDB (included with Visual Studio, or install [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads))

## Getting Started

```bash
# 1. Clone the repo
git clone https://github.com/hassan-jawdat/assignments-service
cd assignments-service

# 2. Apply database migrations
dotnet ef database update --project src/Shiko.Infrastructure --startup-project src/Shiko.API

# 3. Start the API
dotnet run --project src/Shiko.API
```

API runs on `http://localhost:5092`

## API Documentation

Swagger UI available at `http://localhost:5092/swagger`

## Endpoints

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/api/assignments/{userId}` | Get all assignments for a student |
| GET | `/api/assignments/{userId}/courses/{courseId}` | Get a specific assignment |
| POST | `/api/assignments` | Assign a course to a student |
| PATCH | `/api/assignments/{userId}/courses/{courseId}/status` | Update assignment status |

## Tech Stack

- .NET 9
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server LocalDB
- Clean Architecture (API / Domain / Infrastructure)
