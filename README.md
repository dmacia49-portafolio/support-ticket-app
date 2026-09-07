# Support Ticket Application

A full-stack practice application for managing customer support tickets. The project demonstrates a layered C#/.NET architecture from a Blazor WebAssembly frontend through an ASP.NET Core Web API to SQL Server.

## Technology stack

- C# and .NET
- Blazor WebAssembly
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Bootstrap
- xUnit

## Architecture

```text
Blazor WebAssembly
        |
        | HTTP / JSON
        v
ASP.NET Core Controllers
        |
        v
Service Layer
        |
        v
Entity Framework Core
        |
        v
SQL Server (DBapp1)
```

## Features

- View all support tickets
- View ticket details
- Create tickets with automatically assigned IDs
- Edit existing tickets
- Delete tickets with confirmation
- Select customers, statuses, and technicians from database-backed dropdowns
- Assign or leave tickets unassigned
- Validate form input
- Display loading and API error states
- Test backend services with xUnit and an EF Core in-memory database

## Solution structure

```text
SupportTicketApp
├── SupportTicketApp.Api
│   ├── Controllers
│   ├── Data
│   ├── DTOs
│   ├── Models
│   └── Services
├── SupportTicketApp.Client
│   ├── Layout
│   ├── Models
│   ├── Pages
│   ├── Services
│   └── wwwroot
└── SupportTicketApp.Api.Tests
    └── Services
```

## API endpoints

| Method | Endpoint | Purpose |
|---|---|---|
| GET | `/api/tickets` | Return all tickets |
| GET | `/api/tickets/{ticketId}` | Return one ticket |
| POST | `/api/tickets` | Create a ticket |
| PUT | `/api/tickets/{ticketId}` | Update a ticket |
| DELETE | `/api/tickets/{ticketId}` | Delete a ticket |
| GET | `/api/lookups/customers` | Return customer options |
| GET | `/api/lookups/statuses` | Return status options |
| GET | `/api/lookups/technicians` | Return technician options |

## Local setup

### Prerequisites

- Visual Studio with the ASP.NET and web development workload
- A compatible .NET SDK
- SQL Server
- SQL Server Management Studio

### Database

1. Create the `DBapp1` database.
2. Run the included SQL setup script.
3. Confirm that the Customers, Technicians, TicketStatuses, and Tickets tables contain the sample data.

### Database connection

Store the real connection string in .NET User Secrets. Do not commit database credentials.

Example secret structure:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=<Server>;Database=<DB>;User ID=YOUR_USER;Password=YOUR_PASSWORD;Encrypt=True;TrustServerCertificate=True;"
  }
}
```

### Client API address

Configure the API address in `SupportTicketApp.Client/wwwroot/appsettings.json`:

```json
{
  "ApiBaseUrl": "https://localhost:YOUR_API_PORT/"
}
```

The API CORS policy must allow the Blazor client's HTTPS origin.

## Running the application

1. Configure `SupportTicketApp.Api` and `SupportTicketApp.Client` as multiple startup projects.
2. Start both projects.
3. Open the Blazor client and navigate to `/tickets`.

## Running the tests

Run the tests from Visual Studio Test Explorer or from the solution directory:

```powershell
dotnet test
```

## Security notes

- Database credentials are excluded from source control.
- Local credentials belong in .NET User Secrets.
- Production credentials should be supplied through secure environment configuration.
- CORS should allow only the intended frontend origin.
- HTTPS should be used for the frontend and API.

## Project purpose

This project was created for hands-on practice with full-stack C# development, REST APIs, dependency injection, Entity Framework Core, SQL relationships, Blazor forms, validation, and automated testing.
