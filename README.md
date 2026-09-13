# Sudoku for Windows

Sudoku for Windows is an in-development, native Windows Sudoku application. The
Version 1 goal is an offline, ad-free, account-free, and telemetry-free game
that creates every puzzle locally.

The repository currently implements the **V1.1 foundation**. It opens a basic
Windows Forms shell and establishes the UI-independent core and automated test
projects. Sudoku gameplay arrives in later Version 1 milestones.

The source code is available under the [MIT License](LICENSE).

## Requirements

- Windows 10 or Windows 11 on an x64 computer
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

Runtime gameplay will not require a network connection. Package restore during
development is the only network-dependent build step.

## Build, test, and run

From the repository root in PowerShell:

```powershell
dotnet restore .\sudoku-windows.slnx
dotnet build .\sudoku-windows.slnx --no-restore
dotnet test .\sudoku-windows.slnx --no-build
dotnet run --project .\SudokuWindows.WinForms\SudokuWindows.WinForms.csproj
```

The traditional `sudoku-windows.sln` is also included for Visual Studio and
other tooling that does not yet support `.slnx` solution files.

## Repository structure

- `SudokuWindows.Core` — UI-independent domain and engine code.
- `SudokuWindows.WinForms` — the Windows Forms application; references Core.
- `SudokuWindows.Tests` — automated tests for Core and testable application
  behavior.

Dependencies flow from Tests and WinForms toward Core. Core must never reference
Windows Forms or other UI technology.

## Contributing and licenses

See [CONTRIBUTING.md](CONTRIBUTING.md) for the development workflow and
[docs/CODE_STYLE.md](docs/CODE_STYLE.md) for coding conventions. Direct
third-party dependencies and the update process are recorded in
[THIRD-PARTY-NOTICES.md](THIRD-PARTY-NOTICES.md) and
[docs/dependency-licenses.md](docs/dependency-licenses.md).
