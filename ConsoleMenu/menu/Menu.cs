
namespace ConsoleMenu.Menu;

public delegate bool BusinessFunctionNoInput(out string result);
public delegate bool BusinessFunction(string input, out string result);
public delegate bool BusinessFunctionMultipleInput(string[] input, out string result);

public abstract class Menu(string? Message, string? Prompt)
{

    protected int _failedAttempts;
    protected const int MaxFailedAttempts = 3;

    readonly private string? _welcomeMessage = Message;
    public string? WelcomeMessage 
    {
        get { return _welcomeMessage; }
    }

    readonly private string? _commandPrompt = Prompt;
    public string? CommandPrompt 
    {
        get { return _commandPrompt; }
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

    protected void CheckFailedAttempts(IMenuClient client)
    {
        _failedAttempts ++;

        if (_failedAttempts >= MaxFailedAttempts)
        {
            client.CloseMenu();
        }
    }
}