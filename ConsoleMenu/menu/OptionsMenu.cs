
namespace ConsoleMenu.Menu;

/// <summary>
/// Menu subclass for main menus that want to open submenus.
/// Stores options as MenuOptions that encase a MenuAction that gets
/// invoked when the corresponding command is entered. 
/// Use AddOption after instanciation to build up a complete menu.
/// </summary>
/// <param name="Message">Welcome message printed when the menu is displayed. </param>
/// <param name="Prompt">Prompt for user input. </param>
public class OptionsMenu(string? Message, string? Prompt) : Menu(Message, Prompt)
{
    protected List<MenuOption> _options = [];
    public List<MenuOption> Options
    {
        get { return _options; }
    }

    public void AddOption(string key, string description, MenuAction action)
    {
        MenuOption newOption = new(key, action, description);
        Options.Add(newOption);
    }

    protected void AwaitOption(IInputService input, IOutputService output, IMenuClient client)
    {
        output.PrintCommandPrompt(this);
        if (input.ParseString(out string optionText))
        {
            foreach (MenuOption option in Options)
            {
                if (option.Key.Equals(optionText, StringComparison.OrdinalIgnoreCase))
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