using System.Text;
using ConsoleMenu.Menu;
namespace ConsoleMenu.CLI;

public class CLIPrinter : IOutputService
{

    private void AddWelcomeMessage(StringBuilder builder, Menu.Menu menu)
    {
        if (menu.WelcomeMessage != null)
        {
            builder.Append('\n');
            builder.AppendLine(menu.WelcomeMessage);
        }
    }

    private void AddMenuOptions(StringBuilder builder, List<IDisplayableCommand> options)
    {
        foreach (IDisplayableCommand command in options)
        {
            builder.Append($"{command.GetKey()}: {command.GetDescription()}\n");
        }
    }

    public void PrintOptions(OptionsMenu menu)
    {
        var builder = new StringBuilder();
        AddWelcomeMessage(builder, menu);
        builder.Append('\n');
        AddMenuOptions(builder, [.. menu.Options]);

        Console.WriteLine(builder.ToString());
    }
    public void PrintOptions(ActionsMenu menu)
    {
        var builder = new StringBuilder();
        AddWelcomeMessage(builder, menu);
        builder.Append('\n');
        AddMenuOptions(builder, [.. menu.Commands]);

        Console.WriteLine(builder.ToString());
    }

    public void PrintCommandPrompt(string message)
    {
        Console.WriteLine(message);
    }

    public void PrintCommandPrompt(Menu.Menu menu)
    {
        if (menu.CommandPrompt == null) return;

        var builder = new StringBuilder();
        builder.Append(menu.CommandPrompt);
        PrintCommandPrompt(builder.ToString());
    }

    public void PrintMessage(string message)
    {
        var builder = new StringBuilder();
        builder.Append('\n');
        builder.Append(message);
        Console.WriteLine(builder.ToString());
    }
}