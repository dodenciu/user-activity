# User Activity

A .NET 8 Web API solution built with Clean Architecture principles for managing users and their transactions. 
The solution follows Domain-Driven Design (DDD) tactical patterns and implements a simplified form of CQRS using MediatR.

## 🏗️ Architecture

This solution implements **Clean Architecture** with the following layers:

### Layer Dependencies
- **Domain Layer**: No dependencies (core business logic)
- **Application Layer**: Depends only on Domain
- **Infrastructure Layer**: Depends on Domain and Application
- **Presentation Layer**: Depends on Application and Infrastructure

Architecture rules are enforced through automated tests using NetArchTest.

### Architectural Features
- **CQRS Pattern**: Command/Query separation using MediatR
- **Data Validation**: FluentValidation for input validation
- **Database Migrations**: Automatic EF Core migrations
- **API Documentation**: Swagger/OpenAPI integration
- **Error Handling**: Global exception handling with ProblemDetails

### 🏗️ Project Structure

```
src/
├── UserActivity.Api/              # Web API layer
├── UserActivity.Application/      # Application services & CQRS
├── UserActivity.Domain/           # Domain entities & business logic
└── UserActivity.Infrastructure/   # Data access & external services

tests/
├── UserActivity.Domain.UnitTests/    # Domain unit tests
├── UserActivity.IntegrationTests/   # API integration tests
└── UserActivity.ArchitectureTests/  # Architecture compliance tests
```

## 🚀 Features

### User Management
- **Create User**: Register new users in the system
- **Get User**: Retrieve user details by ID
- **Update User**: Modify user information
- **Delete User**: Remove users from the system
- **List Users**: Browse all users
- **Change Password**: Update user passwords

### Transaction Management
- **Create Transaction**: Record user transactions
- **List Transactions**: List transactions with optional filtering
- **Get Total Transaction Amount**: Get total amount of transactions based on type

## 🛠️ Technology Stack

### Core Framework
- **.NET 8** - Latest LTS version (Free for commercial use)
- **ASP.NET Core 8** - Web API framework (Free for commercial use)

### Database & ORM
- **Entity Framework Core 8** - ORM (Free for commercial use)
- **PostgreSQL** - Production database (Free for commercial use)
- **SQLite** - In-memory database for integration tests (Free for commercial use)
- **Npgsql** - PostgreSQL provider for EF Core (Free for commercial use)

### Architecture & Patterns
- **MediatR** - CQRS and Mediator pattern implementation (Apache 2.0 License - Free for commercial use)
- **FluentValidation** - Input validation (Apache 2.0 License - Free for commercial use)

### Testing
- **xUnit** - Unit testing framework (Apache 2.0 License - Free for commercial use)
- **NetArchTest** - Architecture testing (MIT License - Free for commercial use)
- **Microsoft.AspNetCore.Mvc.Testing** - Integration testing (MIT License - Free for commercial use)

### Development Tools
- **Swagger/Swashbuckle** - API documentation (MIT License - Free for commercial use)
- **Docker** - Containerization (Free for commercial use)

## 📋 Prerequisites

- .NET 8 SDK
- Docker and Docker Compose
- PostgreSQL (or use provided Docker setup)

## 🚀 Getting Started

### Prerequisites
- [Docker](https://www.docker.com/get-started) and Docker Compose
- Git

### Quick Start
```bash
# Clone the repository
git clone <repository-url>
cd user-activity

# Start everything with Docker Compose
docker-compose up -d
```

That's it! The application will automatically:
- Build the .NET 8 API
- Start PostgreSQL database
- Apply database migrations
- Start the API server

### Access the Application
- **API**: http://localhost:8000
- **Swagger UI**: http://localhost:8000/swagger
- **Database**: localhost:7432 (PostgreSQL)

### Required Ports
Make sure these ports are available on your machine:
- **8000** - API application
- **7432** - PostgreSQL database

## 🐳 Docker Services

The Docker Compose setup includes:

| Service | Container Name | Port | Description |
|---------|----------------|------|-------------|
| **user-activity.api** | user-activity-api | 8000 | .NET 8 Web API |
| **user-activity-db** | user-activity-db | 7432 | PostgreSQL 17.5 |


## 📄 License Compliance

All packages used in this solution are **free for commercial use**:

- **.NET 8 & ASP.NET Core**: MIT License
- **Entity Framework Core**: MIT License
- **MediatR**: Apache 2.0 License
- **FluentValidation**: Apache 2.0 License
- **xUnit**: Apache 2.0 License
- **NetArchTest**: MIT License
- **PostgreSQL**: PostgreSQL License (similar to MIT)
- **Docker**: Apache 2.0 License

## 🤝 Contributing

1. Follow the existing code style (enforced by `.editorconfig`)
2. Ensure all tests pass
3. Add tests for new features
4. Maintain architecture compliance
5. Update documentation as needed

## 📈 Development Guidelines

- Follow Clean Architecture principles
- Use CQRS for separating read/write operations
- Implement proper validation using FluentValidation
- Write comprehensive tests (unit, integration, architecture)
- Use meaningful commit messages
- Keep controllers thin - business logic belongs in the Application layer