# Privacy Policy – Robot Management System

## 1. Personally Identifiable Information (PII) Collected

The Robot Management System collects the following data:

- Username (for authentication and audit purposes)
- User role (Viewer or Commander)
- Timestamps of commands issued
- Command type and movement coordinates
- System-generated logs of robot status updates

No unnecessary personal information such as real names, addresses, or payment details are collected.

---

## 2. Purpose of Data Collection

The collected data is required to:

- Authenticate users securely
- Enforce Role-Based Access Control (RBAC)
- Maintain an audit trail of robot commands for safety and accountability
- Investigate misuse or unauthorized robot control

This supports operational safety and compliance with system auditing requirements.

---

## 3. Data Minimisation & Storage Limitation

Only data strictly necessary for authentication and mission logging is stored.

Audit logs are retained only for academic demonstration and system validation purposes and would be periodically reviewed and deleted in a production environment.

---

## 4. Data Security Measures

- Passwords are hashed using secure cryptographic algorithms.
- Access to audit logs is restricted via RBAC.
- The database is stored locally and not publicly exposed.
- No hardcoded credentials are used in the system.

This system follows a "Privacy by Design" approach in line with GDPR principles.