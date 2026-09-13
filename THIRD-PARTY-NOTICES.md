# Third-party notices

Sudoku for Windows currently has no third-party runtime dependencies. The test
project uses the following direct development-only NuGet packages:

| Package | Version | License | Use |
| --- | ---: | --- | --- |
| Microsoft.NET.Test.Sdk | 18.9.0 | MIT | Test discovery and execution |
| xunit | 2.9.3 | Apache-2.0 | Test framework |
| xunit.runner.visualstudio | 3.1.4 | Apache-2.0 | Visual Studio and `dotnet test` adapter |

Package license metadata and source repositories:

- [Microsoft.NET.Test.Sdk](https://www.nuget.org/packages/Microsoft.NET.Test.Sdk/18.9.0)
- [xunit](https://www.nuget.org/packages/xunit/2.9.3)
- [xunit.runner.visualstudio](https://www.nuget.org/packages/xunit.runner.visualstudio/3.1.4)

The restored graph also contains these transitive development dependencies:

| Package | Version | License | Source |
| --- | ---: | --- | --- |
| Microsoft.CodeCoverage | 18.9.0 | MIT | [microsoft/vstest](https://github.com/microsoft/vstest) |
| Microsoft.TestPlatform.ObjectModel | 18.9.0 | MIT | [microsoft/vstest](https://github.com/microsoft/vstest) |
| Microsoft.TestPlatform.TestHost | 18.9.0 | MIT | [microsoft/vstest](https://github.com/microsoft/vstest) |
| xunit.abstractions | 2.0.3 | Apache-2.0 | [xunit/xunit](https://github.com/xunit/xunit) |
| xunit.analyzers | 1.18.0 | Apache-2.0 | [xunit/xunit.analyzers](https://github.com/xunit/xunit.analyzers) |
| xunit.assert | 2.9.3 | Apache-2.0 | [xunit/xunit](https://github.com/xunit/xunit) |
| xunit.core | 2.9.3 | Apache-2.0 | [xunit/xunit](https://github.com/xunit/xunit) |
| xunit.extensibility.core | 2.9.3 | Apache-2.0 | [xunit/xunit](https://github.com/xunit/xunit) |
| xunit.extensibility.execution | 2.9.3 | Apache-2.0 | [xunit/xunit](https://github.com/xunit/xunit) |

These notices describe dependency licensing. Sudoku for Windows source code is
licensed separately under the repository's MIT License.
