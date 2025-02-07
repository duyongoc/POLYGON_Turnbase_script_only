using System.IO;

public static partial class Extensions
{
    /// <summary>
    ///     A FileInfo extension method that renames.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="newName">Name of the new.</param>
    /// ###
    /// <returns>.</returns>
    public static void Rename(this FileInfo file, string newName)
    {
        string filePath = Path.Combine(file.Directory.FullName, newName);
        file.MoveTo(filePath);
    }
}