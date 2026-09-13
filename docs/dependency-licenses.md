# Dependency license process

Every direct or transitive dependency and bundled asset must have a compatible,
recorded license before it enters a release.

When changing dependencies:

1. Prefer the .NET platform and avoid a package when built-in functionality is
   sufficient. Runtime dependencies must work fully offline.
2. Pin the direct package version in `Directory.Packages.props`.
3. Restore and inspect the complete graph:

   ```powershell
   dotnet restore .\sudoku-windows.slnx
   dotnet list .\sudoku-windows.slnx package --include-transitive
   ```

4. Verify each package's license expression and repository against the package's
   `.nuspec` metadata and authoritative source. Do not rely only on a search result.
5. Confirm the license is compatible with the owner-approved project license and
   with distribution of the intended x64 package. Escalate unclear, custom,
   copyleft, or missing terms to the owner; do not guess.
6. Update `THIRD-PARTY-NOTICES.md` with name, exact version, license identifier,
   purpose, and source link. Include required notice or license text when the
   dependency's terms require it.
7. Run restore, build, and tests. For release work, compare this record with the
   published output so no dependency is omitted.

Assets, fonts, icons, copied code, and build tools follow the same review even
when they are not NuGet packages.
