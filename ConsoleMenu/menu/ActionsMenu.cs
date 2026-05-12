using System.Runtime.CompilerServices;

namespace ConsoleMenu.Menu;

public class ActionsMenu(string? Message, string? Prompt): Menu(Message, Prompt)
{
    protected List<MenuCommand> _commands = [];
    public List<MenuCommand> Commands
    {
        get { return _commands; }
    }


    public void AddCommand(string key, string description, BusinessFunctionMultipleInput action)
    {
        MenuCommandMultipleInput newCommand = new(key, description, action);
        Commands.Add(newCommand);
    }

    public void AddCommand(string key, string description, BusinessFunction action)
    {
        MenuCommandSingleInput newCommand = new(key, description, action);
        Commands.Add(newCommand);
    }

    public override void Run(IInputService input, IOutputService output, IMenuClient client)
    {
        output.PrintOptions(this);
        AwaitCommand(input, output, client);
    }

    protected void AwaitCommand(IInputService input, IOutputService output, IMenuClient client)
    {
        output.PrintCommandPrompt(this);
        if (!input.ParseStringMultiple(out string[] userInput))
        {
            return;
        }

        foreach (MenuCommand command in Commands)
        {
            if (!command.Key.Equals(userInput.First(), StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (command is MenuCommandSingleInput singleCommand)
            {
                singleCommand.Invoke(string.Join(" ", userInput), output);
            }
            else if (command is MenuCommandMultipleInput multipleCommand)
            {
                multipleCommand.Invoke(userInput, output);
            }
        }
    }
}