using System;
using System.Collections.Generic;

namespace ConsoleMenu;
public class OptionsMenu(string? message, string? prompt) : Menu(message, prompt)
{
    private List<MenuOption> _options = [];
    public List<MenuOption> Options
    {
        get { return _options; }
    }


    public void AddCommand(string key, string description, Action<IInputService, IOutputService, IMenuClient> action)
    {
        MenuOption newCommand = new(key, action, description);
        Options.Add(newCommand);
    }

    protected void AwaitOption(IInputService input, IOutputService output, IMenuClient client)
    {
        output.PrintCommandPrompt(this);
        if (input.ParseString(out string optionText))
        {
            foreach (MenuOption option in Options)
            {
                if (option.key.Equals(optionText, StringComparison.OrdinalIgnoreCase))
                {
                    option.Invoke(input, output, client);
                    break;
                }
            }
        }
        
    }

    public override void Run(IInputService input, IOutputService output, IMenuClient client)
    {
        output.PrintOptions(this);
        AwaitOption(input, output, client);
    }
}