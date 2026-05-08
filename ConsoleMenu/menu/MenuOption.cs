using System;

namespace ConsoleMenu;
public record MenuOption(string key, Action<IInputService, IOutputService, IMenuClient> action, string description)
{
    public readonly Action<IInputService, IOutputService, IMenuClient>? action = action;
    public readonly string key = key;
    public readonly string description = description;
    
    public void Invoke(IInputService input, IOutputService output, IMenuClient client)
    {
        action?.Invoke(input, output, client);
    }

    public bool MatchesCommand(string input)
    {
        return key.Equals(input, StringComparison.OrdinalIgnoreCase);
    }

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