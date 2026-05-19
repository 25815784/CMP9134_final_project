# CMP9134 – Robot Management System

## Project Overview

This project implements a secure Robot Management System that allows authenticated users to control a virtual robot through a web-based dashboard.

The system includes:

- JWT-based authentication
- Role-based access control (Commander / Viewer)
- REST API backend
- Docker-based virtual robot
- Mission logging to database
- Swagger API documentation

---

## Technology Stack

- Backend: ASP.NET Core Web API
- Authentication: JWT Bearer Tokens
- Database: SQL Server / LocalDB
- Containerization: Docker
- Documentation: Swagger
- UML Diagrams: Mermaid (GitHub-rendered)

---

## System Architecture

```mermaid
flowchart LR

User[Commander / Viewer]
UI[Web Dashboard]
API[Backend API]
DB[(Database)]
Robot[Virtual Robot<br>(Docker Container)]

User --> UI
UI -->|HTTPS + JWT| API
API --> DB
API --> Robot
```

---

## UML Diagrams

All system diagrams are located in the `/docs` folder:

- Use Case Diagram
- Activity Diagram
- Class Diagram
- Sequence Diagram

---

## Security Features

- JWT Authentication
- Role-Based Authorization
- Token Validation Middleware
- Secure API Endpoints

---

## Key Features

- Move robot to coordinates (X,Y)
- Reset robot position
- View robot status
- Log all robot missions
- Restrict command execution by role

---

## Author

Student ID: 25815784  
Module: CMP9134
