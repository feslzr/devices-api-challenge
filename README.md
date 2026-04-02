# Devices API

A REST API for persisting and managing device resources, built with .NET 10 and C# 13.

## Author

Felipe Salazar  
GitHub: [feslzr](https://github.com/feslzr)

---

## Tech Stack

- **C# 13 / .NET 10**
- **ASP.NET Core Web API**
- **Entity Framework Core 10** — ORM and database migrations
- **SQL Server 2022** — relational database
- **MediatR** — CQRS pattern via mediator
- **Swashbuckle (Swagger)** — API documentation
- **Docker / Docker Compose** — containerization
- **xUnit + Moq + AutoFixture** — unit testing

---

## Architecture

The project follows **Clean Architecture** principles, organized into the following layers:

- **Challenge.Domain** — entities, enums, and domain models
- **Challenge.Application** — use cases, handlers (MediatR), repository interfaces, and exceptions
- **Challenge.Infrastructure** — Swagger configuration and filters
- **Challenge.Infrastructure.Data** — EF Core DbContext, repository implementations, and migrations
- **Challenge.Api** — controllers, filters, and application entry point

---

## Domain

A `Device` resource contains the following fields:

| Field       | Type     | Description                              |
|-------------|----------|------------------------------------------|
| Id          | int      | Unique identifier, auto-generated        |
| Name        | string   | Name of the device                       |
| Brand       | string   | Brand of the device                      |
| State       | enum     | Device state: Available, Inuse, Inactive |
| CreatedAt   | datetime | Creation timestamp, set automatically    |

---

## API Endpoints

Base URL: `http://localhost:8080/v1/device`

| Method   | Endpoint         | Description                        | Status Code        |
|----------|------------------|------------------------------------|--------------------|
| POST     | /create          | Create a new device                | 201 Created        |
| PATCH    | /{id}            | Partially update an existing device| 200 OK             |
| GET      | /{id}            | Fetch a single device by ID        | 200 OK             |
| GET      | /all             | Fetch all devices (paginated)      | 200 OK             |
| GET      | /list            | Fetch devices by brand and/or state| 200 OK             |
| DELETE   | /{id}            | Delete a single device             | 204 No Content     |

### Domain Validations

- `CreatedAt` cannot be updated.
- `Name` and `Brand` cannot be updated if the device state is `Inuse`.
- Devices with state `Inuse` cannot be deleted.

---

## Getting Started

### Prerequisites

- [Docker](https://www.docker.com/) installed and running

### Running the application

1. Clone the repository:
```bash
git clone https://github.com/feslzr/devices-api-challenge.git
cd devices-api-challenge
```

2. Start the containers:
```bash
docker compose up --build
```

This command will:
- Build the API image
- Start a SQL Server 2022 container
- Apply database migrations automatically on startup
- Expose the API on port `8080`

3. Access the Swagger documentation:
```
http://localhost:8080/swagger/index.html
```

### Running without Docker (local development)

1. Make sure you have a SQL Server instance running locally.
2. Update the connection string in `src/Challenge.Api/appsettings.Development.json`.
3. Run the application:
```bash
dotnet run --project src/Challenge.Api
```

---

## Running Tests
```bash
dotnet test
```

The project includes unit tests covering all handlers and controllers, using xUnit, Moq, and AutoFixture.

---

## Future Improvements

- Add HTTP `400 Bad Request` responses for invalid input using a validation pipeline (e.g. FluentValidation with MediatR behaviors).
- Add integration tests with a real database to complement the existing unit test coverage.
- Implement a dedicated `PUT` endpoint for full device updates, alongside the existing `PATCH` for partial updates.
- Add structured logging (e.g. Serilog) for better observability in production environments.
- Add health check endpoints for container orchestration readiness/liveness probes.