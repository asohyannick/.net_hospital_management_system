# 🏥 Hospital Management System — RESTful Web Services

> **A large-scale, production-grade Hospital Management System backend built with .NET Clean Architecture.**
> Designed and developed by **Asoh Yannick Anoh** — .NET Backend Developer

---

## 👨‍💻 Author

| Field | Details |
|---|---|
| **Name** | Asoh Yannick Anoh |
| **Role** | .NET Backend Developer |
| **Stack** | .NET 10 · C# · ASP.NET Core 10 · PostgreSQL · Docker · Stripe |
| **Architecture** | Clean Architecture (Ardalis template) |

---

## 📋 Table of Contents

- [Overview](#-overview)
- [Tech Stack](#-tech-stack)
- [Architecture](#-architecture)
- [Folder Structure](#-folder-structure)
- [Getting Started](#-getting-started)
- [Environment Variables](#-environment-variables)
- [Running with Docker](#-running-with-docker)
- [Database Migrations](#-database-migrations)
- [API Documentation](#-api-documentation)
- [Contributing](#-contributing)
- [License](#-license)

---

## 🌐 Overview

This project exposes a suite of **RESTful Web Services** powering a large-scale Hospital Management System. It handles:

- 🧑‍⚕️ Patient registration & management
- 📅 Appointment scheduling
- 🏨 Ward & bed management
- 💊 Pharmacy & prescription tracking
- 🧾 Billing & payment processing (via **Stripe**)
- 👨‍⚕️ Doctor & staff management
- 📊 Reporting & analytics

---

## 🛠️ Tech Stack

| Layer | Technology |
|---|---|
| **Language** | C# 13 |
| **Framework** | ASP.NET Core 10 |
| **Architecture** | Clean Architecture |
| **API Style** | RESTful (FastEndpoints) |
| **ORM** | Entity Framework Core 10 |
| **Database** | PostgreSQL |
| **Payments** | Stripe |
| **Containerization** | Docker & Docker Compose |
| **Orchestration** | .NET Aspire 13 |
| **Auth** | ASP.NET Core Identity + JWT |
| **Email** | MailKit 4.16+ |
| **Logging** | Serilog + OpenTelemetry |
| **Testing** | xUnit · NSubstitute · Shouldly · Testcontainers |

---

## 🏛️ Architecture

This project follows **Clean Architecture** as defined by Robert C. Martin (Uncle Bob), implemented using the [Ardalis Clean Architecture Template](https://github.com/ardalis/CleanArchitecture).

```
                        ┌─────────────────────────────┐
                        │          Web (API)           │
                        │   FastEndpoints · Swagger    │
                        └────────────┬────────────────┘
                                     │
                        ┌────────────▼────────────────┐
                        │         Use Cases            │
                        │   CQRS · Mediator · DTOs    │
                        └────────────┬────────────────┘
                                     │
              ┌──────────────────────▼─────────────────────┐
              │                   Core                      │
              │   Entities · Value Objects · Interfaces    │
              └──────────────────────┬─────────────────────┘
                                     │
                        ┌────────────▼────────────────┐
                        │       Infrastructure         │
                        │  EF Core · PostgreSQL · SMTP │
                        │  Stripe · External Services  │
                        └─────────────────────────────┘
```

### Layer Responsibilities

- **Core** — Domain entities, value objects, domain events, and repository interfaces. No dependencies on any other layer.
- **UseCases** — Application business logic, CQRS command/query handlers, and service interfaces.
- **Infrastructure** — Concrete implementations: database access (EF Core + PostgreSQL), Stripe payment integration, email (MailKit), and other external services.
- **Web** — ASP.NET Core API layer using FastEndpoints. Handles HTTP concerns, authentication, and API documentation.

---

## 📁 Folder Structure

```
hospital_ms/
│
├── 📂 src/
│   ├── 📂 hospital_ms.Core/
│   │   ├── 📂 Aggregates/
│   │   │   ├── 📂 PatientAggregate/
│   │   │   │   ├── Patient.cs
│   │   │   │   ├── PatientStatus.cs
│   │   │   │   └── Events/
│   │   │   │       └── PatientRegisteredEvent.cs
│   │   │   ├── 📂 AppointmentAggregate/
│   │   │   ├── 📂 BillingAggregate/
│   │   │   └── 📂 StaffAggregate/
│   │   ├── 📂 Interfaces/
│   │   │   ├── IPatientRepository.cs
│   │   │   └── IEmailSender.cs
│   │   └── 📂 ValueObjects/
│   │       ├── MedicalRecordNumber.cs
│   │       └── Money.cs
│   │
│   ├── 📂 hospital_ms.UseCases/
│   │   ├── 📂 Patients/
│   │   │   ├── 📂 Create/
│   │   │   │   ├── CreatePatientCommand.cs
│   │   │   │   └── CreatePatientHandler.cs
│   │   │   ├── 📂 Get/
│   │   │   └── 📂 List/
│   │   ├── 📂 Appointments/
│   │   ├── 📂 Billing/
│   │   └── 📂 Staff/
│   │
│   ├── 📂 hospital_ms.Infrastructure/
│   │   ├── 📂 Data/
│   │   │   ├── AppDbContext.cs
│   │   │   ├── 📂 Config/
│   │   │   │   └── PatientConfiguration.cs
│   │   │   └── 📂 Migrations/
│   │   ├── 📂 Email/
│   │   │   └── MailKitEmailSender.cs
│   │   ├── 📂 Payments/
│   │   │   └── StripePaymentService.cs
│   │   └── 📂 Repositories/
│   │       └── PatientRepository.cs
│   │
│   ├── 📂 hospital_ms.Web/
│   │   ├── 📂 Endpoints/
│   │   │   ├── 📂 Patients/
│   │   │   │   ├── CreatePatientEndpoint.cs
│   │   │   │   ├── GetPatientEndpoint.cs
│   │   │   │   └── ListPatientsEndpoint.cs
│   │   │   ├── 📂 Appointments/
│   │   │   └── 📂 Billing/
│   │   ├── 📂 Middleware/
│   │   ├── Program.cs
│   │   └── appsettings.json
│   │
│   ├── 📂 hospital_ms.ServiceDefaults/
│   │   └── Extensions.cs
│   │
│   └── 📂 hospital_ms.AspireHost/
│       └── Program.cs
│
├── 📂 tests/
│   ├── 📂 hospital_ms.UnitTests/
│   │   ├── 📂 Core/
│   │   └── 📂 UseCases/
│   ├── 📂 hospital_ms.IntegrationTests/
│   │   └── 📂 Endpoints/
│   └── 📂 hospital_ms.FunctionalTests/
│
├── 📂 .aspire/
├── 🐋 docker-compose.yml
├── 📄 Directory.Build.props
├── 📄 Directory.Packages.props
├── 📄 global.json
├── 📄 nuget.config
├── 📄 hospital_ms.slnx
└── 📄 README.md
```

---

## 🚀 Getting Started

### Prerequisites

Make sure you have the following installed:

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [PostgreSQL](https://www.postgresql.org/download/) (or use the Docker Compose setup)
- [Git](https://git-scm.com/)

### Clone the Repository

```sh
git clone  https://github.com/asohyannick/.net_hospital_management_system.git
cd .net_hospital_management_system
```

### Build the Solution

```sh
dotnet build
```

### Run the Application

**Option 1 — Via Aspire (recommended, starts all services):**
```sh
dotnet run --project src/hospital_ms.AspireHost
```

**Option 2 — Web API only:**
```sh
dotnet run --project src/hospital_ms.Web
```

---

## 🔐 Environment Variables

Create an `appsettings.Development.json` inside `src/hospital_ms.Web/` or set the following environment variables:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=hospital_ms;Username=postgres;Password=yourpassword"
  },
  "Stripe": {
    "SecretKey": "sk_test_...",
    "WebhookSecret": "whsec_..."
  },
  "Email": {
    "Host": "smtp.yourprovider.com",
    "Port": 587,
    "Username": "your@email.com",
    "Password": "yourpassword"
  },
  "Jwt": {
    "Key": "your-super-secret-key",
    "Issuer": "hospital_ms",
    "Audience": "hospital_ms_clients"
  }
}
```

> ⚠️ **Never commit secrets to version control.** Use environment variables or a secrets manager in production.

---

## 🐋 Running with Docker

```sh
# Start PostgreSQL and all services
docker-compose up -d

# Stop all services
docker-compose down
```

A `docker-compose.yml` is provided at the root with PostgreSQL pre-configured.

---

## 🗃️ Database Migrations

```sh
# Add a new migration
dotnet ef migrations add <MigrationName> \
  -c AppDbContext \
  -p src/hospital_ms.Infrastructure/hospital_ms.Infrastructure.csproj \
  -s src/hospital_ms.Web/hospital_ms.Web.csproj

# Apply migrations
dotnet ef database update \
  -c AppDbContext \
  -p src/hospital_ms.Infrastructure/hospital_ms.Infrastructure.csproj \
  -s src/hospital_ms.Web/hospital_ms.Web.csproj
```

---

## 📖 API Documentation

Once the application is running, API docs are available at:

- **Scalar UI:** `https://localhost:{port}/scalar`
- **Swagger UI:** `https://localhost:{port}/swagger`

---

## 🧪 Running Tests

```sh
# Run all tests
dotnet test

# Run with coverage report
dotnet test --collect:"XPlat Code Coverage"
reportgenerator -reports:**/coverage.cobertura.xml -targetdir:coverage-report
```

---

## 🤝 Contributing

Contributions are welcome and appreciated! Here's how to get involved:

### 1. 🍴 Fork & Clone

```sh
git clone https://github.com/your-username/hospital_ms.git
cd hospital_ms
```

### 2. 🌿 Create a Feature Branch

Always branch off from `main` or `develop`. Use a descriptive name:

```sh
git checkout -b feature/add-appointment-scheduling
git checkout -b fix/patient-registration-bug
git checkout -b chore/update-opentelemetry-packages
```

**Branch naming conventions:**
| Prefix | Use for |
|---|---|
| `feature/` | New features |
| `fix/` | Bug fixes |
| `chore/` | Dependency updates, refactoring |
| `docs/` | Documentation changes |
| `test/` | Adding or updating tests |

### 3. ✅ Follow the Architecture

- **Domain logic** belongs in `Core` — no framework dependencies
- **Application logic** belongs in `UseCases` — use CQRS handlers
- **Database/external services** belong in `Infrastructure`
- **HTTP concerns** belong in `Web` endpoints
- Every new feature must have **unit tests** in `hospital_ms.UnitTests`
- Integration-touching features need tests in `hospital_ms.IntegrationTests`

### 4. 🧹 Code Standards

- Follow existing naming conventions (PascalCase for classes, camelCase for locals)
- Use `Ardalis.GuardClauses` for input validation in handlers
- Use `Ardalis.Result` for returning results from use case handlers — never throw exceptions for business failures
- Keep endpoints thin — business logic goes in handlers, not endpoints
- Run `dotnet build` and `dotnet test` before committing — both must pass with 0 errors

### 5. 💬 Commit Messages

Use clear, imperative commit messages:

```
✅ Add appointment scheduling use case
🐛 Fix patient record not found returning 500
♻️ Refactor billing handler to use Result pattern
📦 Update MailKit to 4.16.0
```

### 6. 📬 Open a Pull Request

- Target the `develop` branch (not `main` directly)
- Fill in the PR template with a clear description of what changed and why
- Link any related issues
- Request a review from at least one other contributor
- Ensure CI checks pass before merging

### 7. 🚫 What NOT to Do

- ❌ Don't commit secrets, API keys, or connection strings
- ❌ Don't put business logic in endpoints or controllers
- ❌ Don't bypass the Result pattern by throwing exceptions
- ❌ Don't skip tests for new features
- ❌ Don't push directly to `main`

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).

---

<div align="center">

Built with ❤️ by **Asoh Yannick Anoh** · .NET Backend Developer

⭐ If this project helped you, consider giving it a star!

</div>
