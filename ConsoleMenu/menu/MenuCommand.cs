namespace ConsoleMenu.Menu;

/// <summary>
/// Abstract parent class for Menu options that directly invoke business logic. 
/// Use AddCommand on an ActionsMenu to add a command to the list of recognized 
/// options. This will allow the menu to detect when the user enters the 
/// corresponding key and trigger the encased business logic.
/// 
/// Two concrete versions exist, one for encasing business logic accepting 
/// a single string as input, and one for business logic that requires an
/// array of strings.
///  
/// Disambiguation between the different concrete types is handled by ActionsMenu.
/// </summary>
/// <param name="Key">Text the user must enter to select this command. </param>
/// <param name="Description">Description of the command showed to the user. </param>
public abstract record MenuCommand(string Key, string Description) : IDisplayableCommand
{
    public string GetDescription()
    {
        return Description;
    }

    public string GetKey()
    {
        return Key;
    }
}

/// <summary>
/// Single input version of MenuCommand.
/// This version can encase and run any business function that accepts a single string
/// as input.
/// </summary>
/// <param name="Key">Text the user must enter to select this command. </param>
/// <param name="Description">Description of the command showed to the user. </param>
/// <param name="Action">Business logic to launch. </param>
public record MenuCommandSingleInput(string Key, string Description, BusinessFunction Action) : MenuCommand(Key, Description)
{
    public void Invoke(string argument, IOutputService output)
    {
        Action.Invoke(argument, out string result);
        output.PrintMessage(result);
    }
}

/// <summary>
/// Multiple input version of MenuCommand.
/// This version can encase and run any business function that accepts an array of 
/// strings as input.
/// </summary>
/// <param name="Key">Text the user must enter to select this command. </param>
/// <param name="Description">Description of the command showed to the user. </param>
/// <param name="Action">Business logic to launch. </param>
public record MenuCommandMultipleInput(string Key, string Description, BusinessFunctionMultipleInput Action) : MenuCommand(Key, Description)
{
    public void Invoke(string[] argument, IOutputService output)
    {
        Action.Invoke(argument, out string result);
        output.PrintMessage(result);
    }
}