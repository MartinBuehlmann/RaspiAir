namespace RaspiAir;

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

internal static class DirectoryProvider
{
    public static string ResolveContentDirectory()
    {
        if (Debugger.IsAttached)
        {
            return SearchWebDirectory(Directory.GetCurrentDirectory());
        }

        return ResolveSettingsDirectory();
    }

    private static string ResolveSettingsDirectory()
    {
        string? location = Assembly.GetEntryAssembly()?.Location;
        string? contentDirectory = Path.GetDirectoryName(location);
        if (!string.IsNullOrEmpty(contentDirectory) && Directory.GetDirectories(contentDirectory).Any(x => x.EndsWith("wwwroot", StringComparison.InvariantCulture)))
        {
            return contentDirectory;
        }

        return Directory.GetCurrentDirectory();
    }

    private static string SearchWebDirectory(string currentDirectory)
    {
        string directory = currentDirectory;

        while (true)
        {
            string[] directories = Directory.GetDirectories(directory);
            string? webDir = directories.SingleOrDefault(x => x.EndsWith("RaspiAir.Web", StringComparison.InvariantCulture));
            if (!string.IsNullOrEmpty(webDir))
            {
                return webDir;
            }

            directory = Directory.GetParent(directory)?.FullName!;
        }
    }
}