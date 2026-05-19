# System Architecture

## Architectural Pattern

The Robot Management System follows a Layered Architecture implemented using ASP.NET Core Web API. The architecture separates responsibilities into distinct layers to ensure maintainability, security, and scalability.

The system is composed of:

- Presentation Layer (Swagger / Future Web Dashboard)
- API Layer (Controllers)
- Business Logic Layer (Services)
- Data Access Layer (Entity Framework Core + SQL Server)
- External Integration Layer (Virtual Robot REST API Client)

This separation ensures loose coupling between authentication, command handling, logging, and robot communication.

---

## Layer Responsibilities

### 1. Presentation Layer
Provides API documentation and testing interface via Swagger. Future UI clients communicate using HTTPS and JWT tokens.

### 2. API Layer (Controllers)
Handles HTTP requests and responses. Responsible for:
- Token validation
- Role-Based Access Control (RBAC)
- Routing commands to services

### 3. Business Logic Layer (Services)
Implements core application logic:
- Authentication
- Mission command validation
- Robot communication coordination
- Audit logging

### 4. Data Access Layer
Uses Entity Framework Core to interact with SQL Server for:
- User storage
- Mission logs
- Audit trails

### 5. External Integration Layer
Encapsulates communication with the Virtual Robot Docker container via REST API.

---

## Architectural Diagram

```mermaid
flowchart LR

Client[Swagger / Web Client]
Controller[API Controllers]
Service[Business Logic Services]
DAL[EF Core + SQL Server]
RobotClient[Robot API Client]
Robot[(Virtual Robot Container)]

Client --> Controller
Controller --> Service
Service --> DAL
Service --> RobotClient
RobotClient --> Robot
