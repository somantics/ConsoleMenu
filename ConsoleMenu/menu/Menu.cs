
namespace ConsoleMenu.Menu;

public delegate bool BusinessFunctionNoInput(out string result);
public delegate bool BusinessFunction(string input, out string result);
public delegate bool BusinessFunctionMultipleInput(string[] input, out string result);
public delegate void MenuAction(IInputService input, IOutputService output, IMenuClient client);

/// <summary>
/// Abstract parent class for all menus. 
/// Provides static helper functions for creating menu actions such as
/// open new submenu or close current menu.
/// </summary>
/// <param name="Message">Welcome message printed when the menu is displayed. </param>
/// <param name="Prompt">Prompt for user input. </param>
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

    public static MenuAction CreateOpenSubmenu(Menu submenu)
    {
        return (input, output, client) => client.QueueMenu(submenu);
    }
    public static MenuAction CreateOutputCommand(BusinessFunctionNoInput logic)
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