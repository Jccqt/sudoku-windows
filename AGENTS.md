# AGENTS.md

## Scope

Repository-wide: build an open-source Sudoku app for Windows 10/11 x64 with C#, .NET 10, and Windows Forms. The release is offline, ad-free, account-free, telemetry-free, and generates puzzles locally.

The current repository is a minimal WinForms scaffold. Target:

- `SudokuWindows.Core`: board, validation, solving, generation, difficulty; no UI dependency.
- `SudokuWindows.WinForms`: forms, reusable controls, application services; references Core.
- `SudokuWindows.Tests`: Core and testable game-state/service tests.

Version 1 is active. Implement only requested work in the dependency order below. Record later ideas separately; do not silently add Version 2/3 features.

## Version 1 contract

### Gameplay and UI

- Standard 9x9 Sudoku with clear 3x3 boxes; cells contain 1-9 or empty.
- Clues are immutable. Clues, player values, selection, focus, and mistakes must be distinguishable without color alone.
- Support mouse selection, keyboard and on-screen digits 1-9, Delete/Backspace, and on-screen Clear. Invalid input must not corrupt state; no path may edit a clue.
- Check marks only incorrect filled player cells, never fills cells, and never reveals the solution. Edits/rechecks must remove stale marks.
- Completion requires 81 filled cells and a valid board. Then stop a monotonic clock and show difficulty/time; never measure time by counting UI ticks.
- New Game offers Easy, Medium, and Hard. Progress is meaningful when an editable cell differs from the starting puzzle; confirm before replacing it or closing.
- Keep the board central. Label New Game, difficulty, digits, Clear, Check, timer, and status/busy/error controls.
- Use one programmatic reusable board control, not 81 designer controls; keep puzzle rules out of it.
- Support mouse-only and keyboard-only play, visible/predictable focus, resizing, a documented minimum size, and 100%, 125%, 150%, and 200% Windows scaling where available.

### Generation and difficulty

- Every playable puzzle is valid, uniquely solvable, complete as a package, and verified in the requested band.
- Rate documented logical techniques/effort, never clue count alone. Initial bands:
  - Easy: naked and hidden singles.
  - Medium: may add locked candidates and naked pairs.
  - Hard: may add hidden pairs, triples, X-Wing, or a documented equivalent effort model.
- Define/test scores, thresholds, tie-breaking, and unrateable behavior in Core. Keep representative and boundary puzzles. Retry unrateable/wrong-band candidates within bounds; never mislabel them.
- Generate asynchronously off the UI thread with cancellation and documented retry/time budgets. Typical generation should take seconds, but correctness tests must not use brittle machine-speed limits.
- Fully validate a candidate before atomically replacing board, solution, difficulty, timer, mistake, and completion state on the UI thread. Failed, cancelled, timed-out, exhausted, superseded, or late work must not damage/overwrite the active game.

### Offline, reliability, and release

- Normal use must not require or initiate network access for gameplay, generation, validation, configuration, analytics, accounts, ads, or remote collection. Build-time restore is exempt. Add no dependency needing network access at runtime.
- Version 1 uses no database. If persistence is explicitly requested, store only non-sensitive data under `%LocalAppData%\sudoku-windows\`; bad or unwritable data must not prevent a new game.
- Prevent overlapping generation; cancel superseded/shutdown work and promptly dispose timers, cancellation sources, events, controls, and other resources. Repeated games must not leak or apply stale callbacks.
- Failures must be clear and recoverable without restarting.
- Record all third-party licenses. Do not choose a project license without owner approval; an approved open-source license is required before a public open-source release.

## Engineering rules

- Keep domain/game-state logic independent of WinForms controls, dialogs, UI timers, and UI-thread concerns.
- Use focused types for immutable puzzles/clues, mutable player state, solved boards, and generated packages. Encapsulate storage; validate dimensions/values; document empties; expose no mutable internals.
- Reject malformed dimensions, out-of-range values, and contradictory clues. Player mistakes remain valid game state.
- Validation, solving, counting, rating, and generation never mutate caller inputs.
- Make normal solving deterministic. Distinguish invalid, contradictory, unsolvable, solved, and multiple-solution outcomes as applicable; stop uniqueness checks after two solutions.
- Use exceptions only for exceptional failures. Choose one documented/tested .NET cancellation convention.
- Generate a valid solution, remove clues in seeded random order, check each removal for uniqueness, and undo removals that lose it. Never return partial success.
- A playable package contains puzzle, solution, seed/identity, requested/measured difficulty, generator version, and useful metadata.
- Milestone 4 may expose an internal unique unrated package; after milestone 5 only the rated path serves the UI.
- Isolate mutable/random state per attempt. Put retry/time budgets in configuration or named constants and test them without wall-clock races.
- The same generator version, seed, difficulty, and options must reproduce puzzle, solution, and rating; timings may differ. Own or version PRNG behavior and report reproduction data on failures.
- Enable nullable references, address warnings, and document any suppression.

## Work sequence

Keep each milestone buildable and tested before dependent work:

1. Split Core/WinForms/Tests; configure .NET 10, nullable/warnings, x64 targeting, docs, CI, and license tracking.
2. Board model and validation.
3. Deterministic solver and solution counter.
4. Seeded, cancellable, bounded unique generator with all-or-nothing unrated results.
5. Technique/effort analyzer, reference puzzles, thresholds, and rated generation.
6. Scalable, accessible board control.
7. Player input, clue protection, controls, and testable game commands.
8. Atomic async new-game workflow, supersession, and recovery.
9. Checking, completion, and monotonic timer.
10. Reliability/accessibility audit, docs, x64 packaging, licensing, and release validation.

## Verification

Run from the repository root; update paths when projects move:

```powershell
dotnet restore .\sudoku-windows.slnx
dotnet build .\sudoku-windows.slnx --no-restore
dotnet test .\sudoku-windows.slnx --no-build
dotnet run --project .\sudoku-windows\sudoku-windows.csproj
```

No test project exists yet; a zero-test result is not verification. After the split, update the run path and confirm test discovery. Release validation must publish `win-x64` with the documented framework-dependent/self-contained model; AnyCPU build success is insufficient.

Add focused tests with each feature:

- Board/solver: valid/invalid/malformed/contradictory states; zero/one/multiple solutions; solved/empty boards; clue protection, cancellation/bounds, safe copies, and non-mutation.
- Generator/difficulty: valid solutions, uniqueness, seed reproduction/diversity, requested/reference/boundary ratings, unrateable cases, cancellation, timeout/retry exhaustion, and no partial success.
- Game state: input/clear, meaningful progress/confirmation, stale-result suppression, atomic replacement, Check, completion, and controllable timing.

Keep routine generator batches deterministic/bounded; assert one solution for every sample and report reproduction data. Mark large runs slow/stress.

For UI/release changes, report only checks performed: mouse-only/keyboard-only play; focus and non-color cues; clue/invalid/rapid input; minimum size, resizing, clipping, 3x3 borders, available DPI scales; all generation outcomes and shutdown; timer behavior; full play with networking disabled; and clean x64 package use on available Windows 10/11 systems.

Never claim Windows, DPI, accessibility, offline, packaging, install, or other manual validation unless performed.

## Release gate and exclusions

Version 1 requires the packaged x64 app to complete the full offline loop for all three correctly rated difficulties, with discovered passing tests, Core/UI separation, bounded reproducible generation, current user/contributor docs, third-party notices, an owner-approved license, and no release-critical defects.

Unless explicitly requested, exclude notes, undo/redo, smart highlighting, hints, pause/recovery, statistics, best times, dark/multiple themes, Expert, daily challenges, history, achievements, custom entry, import/export, expanded shortcuts, online/cloud features, accounts, leaderboards, ads/monetization, telemetry/analytics, remote configuration, databases, and non-Windows platforms.

Later-version work requires an explicit user request and must preserve Version 1 correctness, responsiveness, privacy, accessibility, and offline guarantees.
