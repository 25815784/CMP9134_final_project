# RELEASES.md

## Semantic Versioning Scenarios

Assume the current live version is **v1.2.3**.

### Scenario A
**Change:** Fix a bug where the robot crashed if the battery percentage dropped below zero.  
**Next version:** **v1.2.4**

**Why:** This is a bug fix that does not change the public API, so it is a **PATCH** update.

---

### Scenario B
**Change:** Add a new `/api/history` endpoint to retrieve past mission logs. All existing endpoints still work.  
**Next version:** **v1.3.0**

**Why:** This adds new functionality without breaking existing behaviour, so it is a **MINOR** update.

---

### Scenario C
**Change:** Completely redesign the JSON payload for `/api/move`. Old frontends will no longer work and will receive a validation error.  
**Next version:** **v2.0.0**

**Why:** This is a breaking API change, so it is a **MAJOR** update.