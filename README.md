# 🚀 DigitalHub Backend Assessment – Senior .NET Backend Developer  

This repository contains my completed solution for the **Senior .NET Backend Developer Assessment** at **DigitalHub**.  
The project demonstrates backend development skills using **.NET 8, Clean Architecture, CQRS, EF Core, SignalR, Background Jobs, and best practices** for performance, scalability, and security.  

---

## 📂 Project Structure  

The solution follows **Clean Architecture + CQRS** principles:  

src/
├── DigitalHub.Api -> ASP.NET Core Web API (entry point, DI, middlewares, Swagger, versioning)
├── DigitalHub.Application -> Application layer (CQRS, MediatR, DTOs, Validation, Business logic)
├── DigitalHub.Domain -> Entities, Value Objects, Enums, Interfaces, Domain rules
├── DigitalHub.Infrastructure -> EF Core, Repositories, Persistence, Common utilities, constants
tests/
├── DigitalHub.UnitTests -> Unit tests (xUnit + Moq/FakeItEasy)




---

## ⚙️ Features Implemented  

- ✅ **Clean Architecture & CQRS** – Clear separation of concerns using MediatR.  
- ✅ **Entity Framework Core 8** – Data access via repository & unit of work patterns.  
- ✅ **SignalR** – Real-time notifications when specific events occur (e.g., product created).  
- ✅ **Background Jobs** – Implemented using `IHostedService` (and Hangfire optional for scheduling).  
- ✅ **Global Exception Handling & Logging** – Middleware + structured logging with Serilog.  
- ✅ **API Versioning** – Versioned routes with `/api/v1/...` convention.  
- ✅ **Validation & Security** – FluentValidation for inputs + JWT authentication & role-based access.  
- ✅ **Performance Optimizations** – Async EF queries, pagination, caching, compiled queries.  
- ✅ **Swagger & Postman** – Full API documentation via Swagger & provided Postman collection.  

---

## 🛠️ Tech Stack  

- **.NET 8** – ASP.NET Core Web API  
- **Entity Framework Core 8** – ORM for SQL Server  
- **MediatR** – CQRS and request/response pipeline  
- **FluentValidation** – Strongly-typed validation rules  
- **SignalR** – Real-time event broadcasting  
- **Serilog** – Logging and monitoring (console + file sink)  
- **Hangfire / Hosted Services** – Background job processing (optional)  
- **Docker / Docker Compose** – Containerization (optional)  

---

## 🚀 Getting Started  

### 1️⃣ Prerequisites  

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)  
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Postman](https://www.postman.com/)  

### 2️⃣ Clone the Repository  

```bash
git clone [https://github.com/Mina-Noshy/digital-hub.git](https://github.com/Mina-Noshy/digital-hub.git)
cd digital-hub


