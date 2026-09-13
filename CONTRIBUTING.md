# Contributing

Thank you for helping build Sudoku for Windows. Version 1 is developed in the
dependency order documented in `AGENTS.md`; keep changes within the active,
requested milestone and record later ideas separately.

## Development setup

Use Windows 10 or 11 x64 and the .NET 10 SDK. Before submitting a change, run
these commands from the repository root:

```powershell
dotnet restore .\sudoku-windows.slnx
dotnet build .\sudoku-windows.slnx --no-restore
dotnet test .\sudoku-windows.slnx --no-build
```

Tests must be discovered; a zero-test result is not a successful verification.
Add focused tests alongside behavior changes and keep routine tests deterministic.

## Design boundaries

- Keep puzzle and game-state rules in `SudokuWindows.Core` with no UI dependency.
- Keep Windows Forms, dialogs, timers, and UI-thread coordination in
  `SudokuWindows.WinForms`.
- Preserve offline, ad-free, account-free, and telemetry-free operation.
- Do not add runtime network dependencies or a database for Version 1.
- Enable nullable analysis, fix warnings, and document any necessary suppression.

Follow [docs/CODE_STYLE.md](docs/CODE_STYLE.md). When adding or updating a
package, also follow [docs/dependency-licenses.md](docs/dependency-licenses.md)
and update `THIRD-PARTY-NOTICES.md` in the same change.

## Pull requests

Describe the milestone requirement addressed, list the checks actually run, and
call out any manual validation that remains. Do not claim Windows, DPI,
accessibility, offline, packaging, or installation validation unless performed.

Contributions are submitted under the repository's [MIT License](LICENSE).
