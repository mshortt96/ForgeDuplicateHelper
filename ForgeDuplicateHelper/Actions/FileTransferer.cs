namespace ForgeDuplicateHelper;

public class FileTransferer : IAction
{
    public async Task RunAsync()
    {
        Helper.PrintEmptyLine();
        Console.WriteLine("Enter the directory of the new files:");

        string directory = Console.ReadLine();
        if (!DirectoryValid(directory))
        {
            Console.WriteLine("Directory path invalid or does not exist.");
            return;
        }

        IEnumerable<string> newFiles = Helper.GetValidFiles(directory);
        if (!newFiles.Any())
        {
            Console.WriteLine("No files found.");
            return;
        }

        string workingDirectory = Helper.GetDirectoryLastChild(Directory.GetCurrentDirectory());
        Console.WriteLine($"{newFiles.Count()} files found. Are you sure you want to add them to {workingDirectory}?");
        bool userApproves = Helper.GetUserApproval();
        if (!userApproves)
        {
            return;
        }

        Console.WriteLine("Transferring files...");

        await TransferMultipleFilesAsync(newFiles);
    }

    private bool DirectoryValid(string directory)
    {
        if (!Directory.Exists(directory) || directory.Equals(Directory.GetCurrentDirectory(), StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        else
        {
            return true;
        }
    }

    private async Task TransferMultipleFilesAsync(IEnumerable<string> filePaths)
    {
        string workingDirectory = Directory.GetCurrentDirectory();
        IEnumerable<string> workingDirectoryFiles = Helper.GetValidFiles(workingDirectory);

        List<Task> transferTasks = new();
        foreach (string file in filePaths) 
        {
            string fileName = Path.GetFileName(file);
            string fileNameWithoutId = Helper.GetFileNameWithoutId(fileName);

            IEnumerable<string> existingFiles = workingDirectoryFiles.Where(x =>
            {
                string existingFileName = Path.GetFileName(x);
                string existingFileNameWithoutId = Helper.GetFileNameWithoutId(existingFileName);

                return existingFileNameWithoutId.Equals(fileNameWithoutId, StringComparison.OrdinalIgnoreCase);
            });

            string newFileLocation = Path.Combine(workingDirectory, fileName);
            transferTasks.Add(Task.Run(() => TransferSingleFile(file, newFileLocation, existingFiles)));
        }

        await Task.WhenAll(transferTasks);

        Console.WriteLine("Files transferred successfully.");
    }

    private void TransferSingleFile(string filePath, string destinationPath, IEnumerable<string> filesToDelete = null)
    {
        if (filesToDelete != null && filesToDelete.Any())
        {
            foreach (string file in filesToDelete)
            {
                File.Delete(file);
            }
        }

        File.Copy(filePath, destinationPath, true);
    }
}
