# AGENTS.md

## Scope

Repository-wide: build an open-source Sudoku app for Windows 10/11 x64 with C#, .NET 10, and Windows Forms. It is offline, ad-free, account-free, telemetry-free, and generates puzzles locally.

The current repository is a minimal WinForms scaffold. Target:

- `SudokuWindows.Core`: board, validation, solving, generation, difficulty; no UI dependency.
- `SudokuWindows.WinForms`: forms, controls, and application services; references Core.
- `SudokuWindows.Tests`: automated tests for Core and testable game-state behavior.

Version 1 is active. Implement only requested work, follow the dependency order below, and do not add later-version features unless requested.

## Version 1 contract

- Standard 9x9 Sudoku with visually distinct, immutable clues and editable player values.
- Mouse and keyboard input: digits 1-9; Delete/Backspace and an on-screen Clear action.
- Generate Easy, Medium, and Hard puzzles locally; every presented puzzle must have exactly one solution.
- Difficulty must reflect documented logical techniques/effort, not clue count alone.
- Generation is asynchronous, cancellable, bounded, responsive, and never leaves partial state.
- Check marks only incorrect filled player cells and never exposes the solution. Meaning cannot rely on color alone.
- Completion requires a full valid board; then stop a monotonic elapsed-time clock and show difficulty and time.
- Confirm before discarding meaningful progress; failures must be clear and recoverable.
- UI must remain usable with keyboard navigation, resizing, and Windows scaling at 100%, 125%, 150%, and 200%.

## Engineering rules

- Keep Sudoku/domain logic independent of WinForms controls, dialogs, timers, and UI-thread concerns.
- Encapsulate boards safely. Accept digits 1-9, use an explicit empty value or documented sentinel, and reject malformed dimensions, other values, and contradictory clues. Validation and solving must not mutate inputs.
- Make solving deterministic unless randomization is explicitly requested. When testing uniqueness, stop after two solutions.
- Inject or seed randomness for reproducibility. Return only complete, valid, uniquely solvable packages containing the puzzle, solution, seed, difficulty, and useful rating metadata.
- Avoid shared mutable state between generation attempts. Marshal UI updates to the UI thread and dispose/cancel timers, tasks, events, and other resources promptly.
- Create the board programmatically as a reusable control; do not maintain 81 unrelated designer controls or put puzzle rules in the control.
- Use focused types and explicit invalid, contradictory, unsolvable, cancelled, timed-out, and retry-exhausted results.
- Keep nullable reference types enabled. Do not suppress warnings without a documented reason.
- Add no gameplay network dependency; record third-party code and asset licenses.
- If persistence is introduced, store only non-sensitive local data under `%LocalAppData%\sudoku-windows\`; corrupt or unwritable data must not prevent a new game.

## Work sequence

Implement and test in this order:

1. Solution/repository foundation and Core/WinForms/Tests separation.
2. Board model and validation.
3. Deterministic solver and solution counter.
4. Seedable, cancellable unique-puzzle generator.
5. Technique/effort-based difficulty analyzer.
6. Scalable, accessible 9x9 board control.
7. Player input and basic controls.
8. Atomic asynchronous new-game workflow.
9. Checking, completion, and timer.
10. Reliability, accessibility, documentation, packaging, and release validation.

Keep every milestone buildable and testable. Record later ideas separately rather than silently expanding scope.

## Verification

Run from the repository root:

```powershell
dotnet restore .\sudoku-windows.slnx
dotnet build .\sudoku-windows.slnx --no-restore
dotnet test .\sudoku-windows.slnx --no-build
dotnet run --project .\sudoku-windows\sudoku-windows.csproj
```

The run path is current; update it with any WinForms project move or rename.

No test project exists yet: a zero-test `dotnet test` exit is not verification. Once `SudokuWindows.Tests` exists, confirm discovery, run focused tests first, then the full suite when practical. Add tests with Core behavior, covering board/solver outcomes and non-mutation; generation uniqueness, seeds, cancellation, retries, and difficulty; clue protection, checking, completion, and timing.

Generator batch tests should use deterministic seeds, assert every sampled puzzle has exactly one solution, and emit the seed on failure. Keep routine tests bounded; place large stress batches in an explicitly named slow/stress category.

UI changes also require manual checks for mouse-only and keyboard-only play, focus/tab order, resize behavior, common DPI scales, non-color state cues, generation responsiveness, repeated new games, and safe shutdown during generation.

## Out of scope unless explicitly requested

Version 1 excludes notes, undo/redo, hints, pause/session recovery, statistics, dark or multiple themes, Expert difficulty, daily challenges, achievements/history, custom puzzle entry, import/export, online features, cloud sync, accounts, ads, telemetry, and databases.

Do not add a license choice without owner approval. Do not claim Windows/DPI/offline/manual validation unless it was actually performed; report unverified checks clearly.
