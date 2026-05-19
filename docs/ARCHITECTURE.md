# System Architecture

## Architectural Pattern

The Robot Management System follows a Layered Architecture combined with MVC principles. This approach separates concerns into distinct layers to improve maintainability, scalability, and testability.

The system consists of:

- Presentation Layer (Web Dashboard / UI)
- Application Layer (Controllers & Business Logic)
- Data Access Layer (Database + Repository Pattern)
- External Integration Layer (Robot REST API Client)

This structure ensures loose coupling between components and enables independent evolution of the UI, backend logic, and external robot communication.

---

## Architectural Layer Responsibilities

### 1. Presentation Layer
Responsible for rendering the user interface and capturing user input. It communicates with the backend via HTTPS using JWT-secured requests.

### 2. Application Layer
Contains Controllers and Service classes. It processes HTTP requests, validates JWT tokens, enforces Role-Based Access Control (RBAC), and orchestrates communication between services.

### 3. Data Access Layer
Handles persistent storage of mission logs and user data using SQL Server. This layer abstracts database operations from business logic.

### 4. External Integration Layer
Implements a dedicated Robot API Client that communicates with the Virtual Robot container via REST calls.

---

## Architecture Diagram

```mermaid
flowchart TB

UI[Presentation Layer\n(Web Dashboard)]
Controller[Application Layer\n(Controllers & Services)]
DAL[Data Access Layer\n(Database Access)]
RobotClient[External Integration Layer\n(Robot API Client)]
Robot[(Virtual Robot\nDocker Container)]

UI --> Controller
Controller --> DAL
Controller --> RobotClient
RobotClient --> Robot
