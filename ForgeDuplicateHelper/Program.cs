namespace ForgeDuplicateHelper;

public static class Program
{
    private enum Option
    {
        FindDuplicates = 1,
        TransferFiles = 2
    }

    public static async Task Main(string[] args)
    {
        Console.WriteLine("Welcome to the Forge Duplicate Helper");
        Console.WriteLine("=====================================");
        Helper.PrintEmptyLine();

        Console.WriteLine("What do you want to do?");
        Helper.PrintEmptyLine();

        foreach (Option option in Enum.GetValues<Option>())
        {
            int optionNum = (int)option;
            string optionName = GetUserFriendlyOptionName(option);

            Console.WriteLine($"{optionNum}: {optionName}");
        }

        await ChooseOptionAsync();

        Console.Read();
    }

    private static async Task ChooseOptionAsync()
    {
        ConsoleKeyInfo keyPress;
        Option parsedOption;

        do
        {
            keyPress = Console.ReadKey();
        }

        while (!Enum.TryParse(keyPress.KeyChar.ToString(), out parsedOption) || !Enum.IsDefined(parsedOption));

        switch (parsedOption)
        {
            case Option.FindDuplicates:
            {
                await new DuplicateFinder().RunAsync();
                break;
            }

            case Option.TransferFiles:
            {
                await new FileTransferer().RunAsync();
                break;
            }
        }
    }

    private static string GetUserFriendlyOptionName(Option option)
    {
        switch (option)
        {
            default:
                return option.ToString();

            case Option.FindDuplicates:
                return "Find Duplicates";

            case Option.TransferFiles:
                return "Transfer Files";
        }
    }
}
