# Code style

The repository's `.editorconfig` and `Directory.Build.props` are authoritative.
The build treats compiler and recommended .NET analyzer warnings as errors.

## C# conventions

- Use C# 14, nullable reference types, and implicit global usings.
- Prefer file-scoped namespaces and four-space indentation.
- Use PascalCase for types, members, and constants; use camelCase for parameters
  and local variables.
- Keep methods and types focused. Prefer explicit domain types over loosely
  related primitive values.
- Use exceptions for exceptional failures, not ordinary solver or validation
  outcomes.
- Document public APIs when their contract is not self-evident.

## Architecture and tests

- Core code cannot reference Windows Forms, UI controls, dialogs, or UI timers.
- Operations must not mutate caller-owned inputs unless their contract explicitly
  represents mutable game state.
- Normal solver and generator behavior must be deterministic for a given input
  and seed.
- Give tests descriptive PascalCase names and keep them independent of execution
  order and machine speed.
