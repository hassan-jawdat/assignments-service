# Assignments Service

ASP.NET Core Web API for managing student assignments in the Shiko LMS.

## Endpoints

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/api/assignments/{userId}` | Get all assignments for a student |
| GET | `/api/assignments/{userId}/courses/{courseId}` | Get a specific assignment |
| POST | `/api/assignments` | Assign a course to a student |
| PATCH | `/api/assignments/{userId}/courses/{courseId}/status` | Update assignment status |

## Run locally

```bash
dotnet run --project src/Shiko.API
```

API runs on `http://localhost:5121`

## API Documentation

Swagger UI available at `http://localhost:5121/swagger`

## Tech Stack

- .NET 9
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server LocalDB
- Clean Architecture (API / Domain / Infrastructure)
