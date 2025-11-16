# .NET 10 Upgrade Plan

## Execution Steps

Execute steps below sequentially one by one in the order they are listed.

1. Validate that an .NET 10 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET 10 upgrade.
3. Upgrade SpanJson\SpanJson.csproj
4. Upgrade SpanJson.Benchmarks\SpanJson.Benchmarks.csproj
5. Upgrade SpanJson.Tests\SpanJson.Tests.csproj
6. Upgrade SpanJson.WebBenchmark\SpanJson.WebBenchmark.csproj
7. Upgrade SpanJson.Shared\SpanJson.Shared.csproj
8. Upgrade SpanJson.AspNetCore.Formatter\SpanJson.AspNetCore.Formatter.csproj
9. Upgrade SpanJson.AspNetCore.Formatter.Tests\SpanJson.AspNetCore.Formatter.Tests.csproj
10. Run unit tests to validate upgrade in the projects listed below:
  - SpanJson.Tests\SpanJson.Tests.csproj
  - SpanJson.AspNetCore.Formatter.Tests\SpanJson.AspNetCore.Formatter.Tests.csproj

## Settings

This section contains settings and data used by execution steps.

### Excluded projects

Table below contains projects that do belong to the dependency graph for selected projects and should not be included in the upgrade.

| Project name                                   | Description                 |
|:-----------------------------------------------|:---------------------------:|

### Aggregate NuGet packages modifications across all projects

NuGet packages used across all selected projects or their dependencies that need version update in projects that reference them.

| Package Name                 | Current Version                  | New Version | Description                                   |
|:-----------------------------|:--------------------------------:|:-----------:|:----------------------------------------------|
| Microsoft.AspNetCore.TestHost| 6.0;8.0.5                        | 10.0.0      | Recommended for .NET 10                       |
| Newtonsoft.Json              | 13.0.3                           | 13.0.4      | Recommended for .NET 10                       |
| System.Collections.Immutable | 8.0.0                            | 10.0.0      | Recommended for .NET 10                       |
| System.Memory                | 4.6.0-preview1-26717-04          |             | Functionality included with framework         |

### Project upgrade details
This section contains details about each project upgrade and modifications that need to be done in the project.

#### SpanJson\SpanJson.csproj

Project properties changes:
  - Target frameworks should be changed from `net6.0;net8.0` to `net6.0;net8.0;net10.0`

NuGet packages changes:
  - NuGet package source mappings are recommended in NuGet.config

Other changes:
  - None

#### SpanJson.Benchmarks\SpanJson.Benchmarks.csproj

Project properties changes:
  - Target frameworks should be changed from `net6.0;net8.0` to `net6.0;net8.0;net10.0`

NuGet packages changes:
  - Newtonsoft.Json should be updated from `13.0.3` to `13.0.4` (recommended for .NET 10)

Other changes:
  - None

#### SpanJson.Tests\SpanJson.Tests.csproj

Project properties changes:
  - Target frameworks should be changed from `net6.0;net8.0` to `net6.0;net8.0;net10.0`

NuGet packages changes:
  - System.Collections.Immutable should be updated from `8.0.0` to `10.0.0` (recommended for .NET 10)

Other changes:
  - None

#### SpanJson.WebBenchmark\SpanJson.WebBenchmark.csproj

Project properties changes:
  - Target frameworks should be changed from `net6.0;net8.0` to `net6.0;net8.0;net10.0`

NuGet packages changes:
  - System.Memory should be removed (functionality included with framework)

Other changes:
  - None

#### SpanJson.Shared\SpanJson.Shared.csproj

Project properties changes:
  - Target frameworks should be changed from `netstandard2.0;net8.0` to `netstandard2.0;net8.0;net10.0`

NuGet packages changes:
  - NuGet package source mappings are recommended in NuGet.config

Other changes:
  - None

#### SpanJson.AspNetCore.Formatter\SpanJson.AspNetCore.Formatter.csproj

Project properties changes:
  - Target frameworks should be changed from `net6.0;net8.0` to `net6.0;net8.0;net10.0`

NuGet packages changes:
  - NuGet package source mappings are recommended in NuGet.config

Other changes:
  - None

#### SpanJson.AspNetCore.Formatter.Tests\SpanJson.AspNetCore.Formatter.Tests.csproj

Project properties changes:
  - Target frameworks should be changed from `net6.0;net8.0` to `net6.0;net8.0;net10.0`

NuGet packages changes:
  - Microsoft.AspNetCore.TestHost should be updated from `6.0;8.0.5` to `10.0.0` (recommended for .NET 10)

Other changes:
  - None
