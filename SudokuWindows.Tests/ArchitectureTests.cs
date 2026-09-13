using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using Xunit;

namespace SudokuWindows.Tests;

public sealed class ArchitectureTests
{
    [Fact]
    public void CoreAssemblyHasNoWindowsFormsDependency()
    {
        string coreAssemblyPath = Path.Combine(
            AppContext.BaseDirectory,
            "SudokuWindows.Core.dll");

        using FileStream stream = File.OpenRead(coreAssemblyPath);
        using PEReader portableExecutable = new(stream);
        MetadataReader metadata = portableExecutable.GetMetadataReader();

        string[] referencedAssemblies = metadata.AssemblyReferences
            .Select(handle => metadata.GetAssemblyReference(handle))
            .Select(reference => metadata.GetString(reference.Name))
            .ToArray();

        Assert.DoesNotContain("System.Windows.Forms", referencedAssemblies);
    }
}
