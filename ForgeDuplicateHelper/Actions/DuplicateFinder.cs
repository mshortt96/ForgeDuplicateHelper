namespace ForgeDuplicateHelper;

public class DuplicateFinder : IAction
{
    public async Task RunAsync()
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        string[] currentDirectoryFiles = Directory.GetFiles(currentDirectory);

        Dictionary<string, int> fileCounts = new();

        foreach(string file in currentDirectoryFiles)
        {
            if(!Helper.FileValid(file))
            {
                continue;
            }

            string fileWithoutId = Helper.GetFileNameWithoutId(file);
            if (fileCounts.ContainsKey(fileWithoutId))
            {
                int count = fileCounts[fileWithoutId];
                fileCounts[fileWithoutId] = count + 1;
            }

            else
            {
                fileCounts.Add(fileWithoutId, 1);
            }
        }

        Helper.PrintEmptyLine();

        Dictionary<string, int> duplicates = fileCounts.Where(x => x.Value > 1).ToDictionary(x => x.Key, x => x.Value);
        if (duplicates.Any())
        {
            foreach(KeyValuePair<string, int> duplicate in duplicates)
            {
                string nameWithoutPath = Path.GetFileName(duplicate.Key);
                Console.WriteLine($"{nameWithoutPath}: {duplicate.Value} files");
            }
        }

        else
        {
            Console.WriteLine("No duplicates found.");
        }

        await Task.CompletedTask;
    }
}
