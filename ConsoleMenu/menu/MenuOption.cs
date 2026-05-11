
namespace ConsoleMenu.Menu;
public record MenuOption(string Key, Action<IInputService, IOutputService, IMenuClient> Action, string Description) : IDisplayableCommand
{
    public string GetDescription()
    {
        return Description;
    }

    public string GetKey()
    {
        return Key;
    }

    public void Invoke(IInputService input, IOutputService output, IMenuClient client)
    {
        Action?.Invoke(input, output, client);
    }
}