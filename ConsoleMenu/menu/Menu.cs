
namespace ConsoleMenu.Menu;

public delegate bool BusinessFunctionNoInput(out string result);
public delegate bool BusinessFunction(string input, out string result);
public delegate bool BusinessFunctionMultipleInput(string[] input, out string result);

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


    public static void Close(IInputService input, IOutputService output, IMenuClient client)
    {
        client.CloseMenu();
    }

    public static Action<IInputService, IOutputService, IMenuClient> CreateOpenSubmenu(Menu submenu)
    {
        return (input, output, client) => client.QueueMenu(submenu);
    }
    public static Action<IInputService, IOutputService, IMenuClient> CreateOutputCommand(BusinessFunctionNoInput logic)
    {
        return (input, output, client) =>
        {
            if (logic(out string message)) 
                output.PrintMessage(message);
        };
    }
}