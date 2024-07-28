using System.Text.RegularExpressions;

namespace ForgeDuplicateHelper;

public static class Helper
{
    public static void PrintEmptyLine()
    {
        Console.WriteLine(string.Empty);
    }

    public static bool FileValid(string fileName)
    {
        return Regex.IsMatch(fileName, $"{Constant.FilePrefixPattern}.+");
    }

    public static string GetFileNameWithoutId(string fileName)
    {
        return Regex.Replace(fileName, Constant.FilePrefixPattern, string.Empty);
    }

    public static IEnumerable<string> GetValidFiles(string directory)
    {
        return Directory.GetFiles(directory).Where(x =>
        {
            string fileName = Path.GetFileName(x);
            return Regex.IsMatch(fileName, Constant.FilePrefixPattern);
        });
    }

    public static bool GetUserApproval()
    {
        bool? parsedInput = null;
        while (!parsedInput.HasValue)
        {
            string input = Console.ReadLine();
            if (input.Equals("y", StringComparison.OrdinalIgnoreCase) || input.Equals("yes", StringComparison.OrdinalIgnoreCase))
            {
                parsedInput = true;
            }

            else if (input.Equals("n", StringComparison.OrdinalIgnoreCase) || input.Equals("no", StringComparison.OrdinalIgnoreCase))
            {
                parsedInput = false;
            }
        }

        return parsedInput.Value;
    }

    public static string GetDirectoryLastChild(string path)
    {
        path = path.TrimEnd(Path.DirectorySeparatorChar);
        return Path.GetFileName(path);
    }
}
