# ELearning Platform API

A RESTful API built with ASP.NET Core for managing course enrollments with an approval workflow, designed for government e-learning systems.

---

## Table of Contents

- [How to Run the Project](#how-to-run-the-project)
- [Database Setup](#database-setup)
- [Architecture Choices](#architecture-choices)
- [Assumptions](#assumptions)
- [API Examples](#api-examples)

---

## How to Run the Project

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (or SQL Server LocalDB — ships with Visual Studio)
- Visual Studio 2022+ **or** any editor with the .NET CLI

### Steps

1. **Clone the repository**

   ```bash
   git clone <your-repo-url>
   cd ELearningPlatform-master
   ```

2. **Configure the connection string**

   Open `ELearningPlatform/appsettings.json` and update the `DefaultConnection` value to match your SQL Server instance:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\ProjectModels;Database=ELearningPlatformDb;Trusted_Connection=True;"
     }
   }
   ```

3. **Apply database migrations**

   ```bash
   cd ELearningPlatform
   dotnet ef database update
   ```

4. **Run the project**

   ```bash
   dotnet run
   ```

5. **Open Swagger UI**

   Navigate to `https://localhost:{port}/swagger` in your browser to explore and test all endpoints interactively.

---

## Database Setup

The project uses **Entity Framework Core** with a **SQL Server** database. All schema changes are managed via EF Core migrations.

### Apply Migrations

```bash
dotnet ef database update
```

This will create the database and apply all existing migrations automatically.

### Create a New Migration (if you modify entities)

```bash
dotnet ef migrations add <MigrationName>
dotnet ef database update
```

### Database Schema

| Table        | Description                                      |
|--------------|--------------------------------------------------|
| `Courses`    | Stores course information and approval settings  |
| `Learners`   | Stores learner profiles and contact details      |
| `Enrollments`| Links learners to courses with status tracking   |
| `Decisions`  | Records approval/rejection decisions per enrollment |

---

## Architecture Choices

### Project Structure

```
ELearningPlatform/
├── Abstractions/          # Result<T> pattern and Error types
├── Contracts/             # Request/Response DTOs and FluentValidation validators
│   ├── Course/
│   ├── Enrollment/
│   └── Learner/
├── Controllers/           # API layer — thin, delegates to services
├── Entities/              # EF Core domain entities
├── Enums/                 # Enrollment status enum
├── Errors/                # Centralized static error definitions per domain
├── Migrations/            # EF Core migration history
├── Persistence/           # DbContext and entity configurations (Fluent API)
└── Services/              # Business logic layer (service + interface per domain)
```

### Key Decisions

**Result Pattern**
All service methods return `Result<T>` or `Result` instead of throwing exceptions. This makes error handling explicit, keeps controllers thin, and avoids relying on exceptions for control flow. Errors are converted to RFC 7807 `ProblemDetails` responses via the `ToProblem()` extension.

**Mapster for Mapping**
[Mapster](https://github.com/MapsterMapper/Mapster) is used for mapping between entities and DTOs (`Adapt<T>()`). It is faster than AutoMapper and requires zero configuration for simple mappings.

**FluentValidation**
Input validation is handled by FluentValidation with `AddFluentValidationAutoValidation()`, so validation runs automatically before controllers are hit. Each request DTO has its own validator class co-located in the `Contracts` folder.

**Service Layer**
Business logic lives entirely in the service layer (`ICourseService`, `ILearnerService`, `IEnrollmentService`). Controllers only call services and map results to HTTP responses — they contain no business logic.

**EF Core Fluent API**
Entity configuration is done via `IEntityTypeConfiguration<T>` classes in `Persistence/EntitiesConfigurations/`, keeping entity classes clean and separation of database concerns clear.

---

## Assumptions

- **Authentication is not implemented.** The API is open. Role-based behavior (from the bonus requirements) was not simulated via request headers in this submission.
- **Pagination is not implemented.** All list endpoints return the full result set.
- **Soft delete is not used.** `DELETE /api/courses/{id}` performs a hard delete. If a course has existing enrollments, this will fail at the database level due to foreign key constraints — a business rule can be added to guard this.
- **Decision values** accepted by `POST /api/enrollments/{id}/decision` are `"Approve"` and `"Reject"` (not `"Approved"`/`"Rejected"`). The stored status after a decision is `Approved` or `Rejected`.
- **`EnrolledAt`** is set automatically by the database/EF at the time of enrollment creation.
- **Inactive courses** block new enrollments but do not affect existing ones.
- **Duplicate enrollment** is checked per `(LearnerId, CourseId)` pair regardless of enrollment status.

---

## API Examples

### Courses

#### Get all courses
```http
GET /api/courses
```

#### Get course by ID
```http
GET /api/courses/1
```

#### Create a course
```http
POST /api/courses
Content-Type: application/json

{
  "title": "Introduction to Cybersecurity",
  "description": "Foundational course covering network security and best practices.",
  "durationHours": 20,
  "requiresApproval": true,
  "isActive": true
}
```

#### Update a course
```http
PUT /api/courses/1
Content-Type: application/json

{
  "title": "Advanced Cybersecurity",
  "description": "Deep dive into penetration testing and incident response.",
  "durationHours": 40,
  "requiresApproval": true,
  "isActive": true
}
```

#### Delete a course
```http
DELETE /api/courses/1
```

---

### Learners

#### Get all learners
```http
GET /api/learners
```

#### Get learner by ID
```http
GET /api/learners/1
```

#### Create a learner
```http
POST /api/learners
Content-Type: application/json

{
  "fullName": "Ahmed Al-Rashidi",
  "email": "ahmed.rashidi@gov.eg",
  "nationalId": "29901011234567",
  "department": "IT Security"
}
```

---

### Enrollments

#### Enroll a learner in a course
```http
POST /api/enrollments
Content-Type: application/json

{
  "learnerId": 1,
  "courseId": 3
}
```

**Behavior:**
- If the course has `requiresApproval: true` → status is `PendingApproval`
- If the course has `requiresApproval: false` → status is `Approved`

#### Make an approval decision
```http
POST /api/enrollments/5/decision
Content-Type: application/json

{
  "decision": "Approve",
  "reason": "Employee meets all prerequisites"
}
```

```http
POST /api/enrollments/5/decision
Content-Type: application/json

{
  "decision": "Reject",
  "reason": "Missing required department clearance"
}
```

**Rules:**
- Only `PendingApproval` enrollments can be acted on
- `Reject` requires a reason

#### Get enrollments with optional filters
```http
GET /api/enrollments
GET /api/enrollments?learnerId=1
GET /api/enrollments?courseId=2
GET /api/enrollments?status=Approved
GET /api/enrollments?fromDate=2026-01-01&toDate=2026-06-30
GET /api/enrollments?learnerId=1&status=PendingApproval
```

---

### Error Response Format

All errors follow RFC 7807 ProblemDetails:

```json
{
  "status": 404,
  "title": "Not Found",
  "extensions": {
    "errors": [
      "CourseErrors.NotFound",
      "No course was found with the given ID"
    ]
  }
}
```

---

## Tech Stack

| Technology              | Version  | Purpose                        |
|-------------------------|----------|--------------------------------|
| ASP.NET Core Web API    | .NET 8   | API framework                  |
| Entity Framework Core   | 8.0.28   | ORM and database migrations    |
| SQL Server / LocalDB    | —        | Relational database            |
| Mapster                 | 10.0.8   | Object mapping                 |
| FluentValidation        | 12.1.1   | Request validation             |
| Swashbuckle (Swagger)   | 6.6.2    | API documentation and testing  |
