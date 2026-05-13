
namespace ConsoleMenu.Menu;

/// <summary>
/// Record class that encases a single menu action behind a command. 
/// 
/// Use AddOption on an OptionsMenu to add a command to the list of recognized 
/// options. This will allow the menu to detect when the user enters the 
/// corresponding key and trigger the encased menu actions.
/// 
/// Menu actions perform tasks such as close the current menu, open a submenu,
/// or running business logic which does not require input.
/// 
/// The Menu class containes static methods for creating common MenuActions.
/// 
/// </summary>
/// <param name="Key">Text the user must enter to select this command. </param>
/// <param name="Action">Action to perform, such as launch new menu or close current menu. </param>
/// <param name="Description">Description of the command showed to the user. </param>
public record MenuOption(string Key, MenuAction Action, string Description) : IDisplayableCommand
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