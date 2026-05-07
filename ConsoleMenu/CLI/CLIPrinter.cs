using System;
using System.Text;

namespace ConsoleMenu.CLI;

public class CLIPrinter : IOutputService
{
    public void PrintOptions(OptionsMenu menu)
    {
        var builder = new StringBuilder();

        if (menu.WelcomeMessage != null)
        {
            builder.Append('\n');
            builder.Append(menu.WelcomeMessage);
        }

        builder.Append('\n');
        foreach (MenuOption command in menu.Options)
        {
            builder.Append($"{command.key}: {command.description}\n");
        }

        Console.WriteLine(builder.ToString());
    }
    public void PrintCommandPrompt(Menu menu)
    {
        if (menu.CommandPrompt == null) return;

        var builder = new StringBuilder();
        builder.Append(menu.CommandPrompt);
        Console.Write(builder.ToString());
    }

    public void PrintMessage(string message)
    {
        var builder = new StringBuilder();
        builder.Append('\n');
        builder.Append(message);
        builder.Append('\n');
        Console.Write(builder.ToString());
    }
}