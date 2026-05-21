# LICENSE_AUDIT.md

## Dependency License Audit

This project uses ASP.NET Core / .NET packages from NuGet.

### Audit Approach
I reviewed the direct and transitive NuGet packages used by:
- `RobotDashboard/RobotDashboard.csproj`
- `RobotDashboard.Tests/RobotDashboard.Tests.csproj`

The package audit was carried out using:

```powershell
dotnet list RobotDashboard/RobotDashboard.csproj package --include-transitive
dotnet list RobotDashboard.Tests/RobotDashboard.Tests.csproj package --include-transitive
I then reviewed the dependency list to identify whether any strong copyleft licenses, such as GPL, were present.

Findings
The project mainly depends on standard Microsoft and well-established .NET ecosystem libraries, including:

Microsoft.AspNetCore.OpenApi
Swashbuckle.AspNetCore
Microsoft.AspNetCore.Mvc.Testing
Microsoft.NET.Test.Sdk
Moq
xunit
Newtonsoft.Json
Castle.Core
coverlet.collector
These dependencies are commonly distributed under permissive licenses, primarily MIT and Apache 2.0.

No GPL or other strong copyleft dependencies were identified in the reviewed package set.

Risk Assessment
Based on the reviewed dependency list, no major licensing conflict has been identified. The project’s dependency profile appears legally suitable for academic use and would also be broadly acceptable for commercial-style distribution, subject to final legal review of all third-party packages.

This is important because strong copyleft dependencies could impose source code disclosure obligations, which would be inappropriate for a proprietary or restricted-distribution system.

Own Project License
A repository license should be added so that the legal terms for reuse of this code are explicit.

Recommended option: MIT License

This is appropriate because:

it is simple and widely understood
it aligns well with the permissive nature of the project’s dependencies
it is suitable for an academic portfolio/public GitHub repository
Conclusion
The dependency audit did not identify any immediate licensing red flags. Most dependencies appear to be permissive and low-risk from a legal compliance perspective. However, dependency license review should remain part of the project’s release and maintenance process.