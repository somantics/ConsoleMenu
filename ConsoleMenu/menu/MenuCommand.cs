namespace ConsoleMenu.Menu;

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

public record MenuCommandSingleInput(string Key, string Description, BusinessFunction Action) : MenuCommand(Key, Description)
{
    public void Invoke(string argument, IOutputService output)
    {
        Action.Invoke(argument, out string result);
        output.PrintMessage(result);
    }
}

public record MenuCommandMultipleInput(string Key, string Description, BusinessFunctionMultipleInput Action) : MenuCommand(Key, Description)
{
    public void Invoke(string[] argument, IOutputService output)
    {
        Action.Invoke(argument, out string result);
        output.PrintMessage(result);
    }
}