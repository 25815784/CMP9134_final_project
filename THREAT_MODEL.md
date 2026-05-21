# THREAT_MODEL.md

## STRIDE Threat Model for `POST /api/Robot/move`

This threat model was carried out for the robot movement endpoint because it is one of the most safety-critical parts of the system.

### Spoofing
An attacker could pretend to be an authorised user and attempt to send robot movement commands.

**Current position:** Full backend authentication is not yet complete. Role selection currently exists in the dashboard UI, but this is not sufficient security on its own.

**Mitigation:** Implement JWT-based authentication and verify user identity on the API side.

---

### Tampering
An attacker could alter X and Y movement values while data is being transmitted.

**Current position:** This risk exists if communication is not protected properly.

**Mitigation:** Use HTTPS/TLS so requests cannot be easily modified in transit.

---

### Repudiation
A user could send a dangerous robot command and later deny they performed the action.

**Current position:** Audit events are visible in the UI, but persistent audit logging is not yet fully implemented.

**Mitigation:** Store audit logs in a database with user ID, timestamp, action, and target endpoint.

---

### Information Disclosure
An attacker could inspect API responses to learn robot state, position, or system behaviour.

**Current position:** The API exposes robot status information required by the dashboard.

**Mitigation:** Limit returned data to only what is necessary and protect endpoints with authentication and HTTPS.

---

### Denial of Service
An attacker could send a very large number of requests to `/api/Robot/move`, affecting availability.

**Current position:** No rate limiting is currently implemented.

**Mitigation:** Add ASP.NET Core rate limiting middleware and monitor repeated command traffic.

---

### Elevation of Privilege
A low-privilege user, such as a Viewer, could try to access movement functionality intended for a Commander.

**Current position:** Role behaviour exists in the frontend, but backend enforcement still needs strengthening.

**Mitigation:** Apply backend role-based authorization checks so only authorised roles can access command endpoints.

---

## Summary of Key Mitigations
The two most important security improvements identified are:

1. Implement backend authentication and role-based authorization.
2. Persist audit logs and protect API traffic using HTTPS.

## Reflection
This analysis shows that robot control is not only a technical feature but also a security and safety issue. Because the system can issue movement commands, access control, accountability, and service availability are essential.