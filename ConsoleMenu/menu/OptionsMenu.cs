
namespace ConsoleMenu.Menu;
public class OptionsMenu(string? Message, string? Prompt) : Menu(Message, Prompt)
{
    protected List<MenuOption> _options = [];
    public List<MenuOption> Options
    {
        get { return _options; }
    }


    public void AddOption(string key, string description, Action<IInputService, IOutputService, IMenuClient> action)
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