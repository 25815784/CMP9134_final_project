# Ground Control Station – Low Fidelity Wireframe

## Main Dashboard Layout

```mermaid
flowchart TB

    Navbar[Navigation Bar<br/>- Ground Control<br/>- Role Selector]

    Dashboard[Main Dashboard]

    Status[Robot Status Panel<br/>
    - Battery %<br/>
    - Status<br/>
    - X/Y Position]

    Control[Move Robot Panel<br/>
    - Direction Arrows<br/>
    - Move Button<br/>
    - Emergency Stop]

    Log[Mission Audit Log<br/>
    - Timestamped Events]

    Navbar --> Dashboard
    Dashboard --> Status
    Dashboard --> Control
    Dashboard --> Log
```
## Deep Dive – Move Robot Interaction

```mermaid
sequenceDiagram
    participant User
    participant UI
    participant System

    User->>UI: Select Direction (↑ ↓ ← →)
    UI-->>User: Highlight Selected Direction
    User->>UI: Click "Move"
    UI->>System: Simulate Movement
    System-->>UI: Update Position
    UI-->>User: Show "Move Successful"
    UI-->>User: Update Status Badge
```
## Fitts’s Law Consideration

Primary action buttons ("Move" and "Emergency Stop") are large and centrally positioned to reduce movement time and improve accessibility.

The Emergency Stop button is visually distinct and easily reachable to support safety-critical operation.
