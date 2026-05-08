
namespace ConsoleMenu;

public delegate bool BusinessFunctionNoInput(out string result);
public delegate bool BusinessFunction(string input, out string result);
public delegate bool BusinessFunctionMultiple(string[] input, out string result);

public abstract class Menu(string? message, string? prompt)
{

    readonly private string? welcomeMessage = message;
    public string? WelcomeMessage 
    {
        get { return welcomeMessage; }
    }

    readonly private string? commandPrompt = prompt;
    public string? CommandPrompt 
    {
        get { return commandPrompt; }
    }

    public abstract void Run(IInputService input, IOutputService output, IMenuClient client);
}