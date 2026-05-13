using System;

namespace ConsoleMenu.CLI;

/// <summary>
/// Concrete implementation of IInputService to go with CLIClient class.
/// Responsible for parsing raw input into strings with minimal cleanup,
/// not for type validation. 
/// </summary>
public class CLIParser: IInputService
{
    
    public bool ParseAmount(out int amount)
    {
        string? rawInput = Console.ReadLine();

        if (int.TryParse(rawInput, out int result))
        {
            amount = result;
            return true;
        }

        amount = 0;
        return false;
    }

    public bool ParseString(out string input)
    {
        string? rawInput = Console.ReadLine();

        if (rawInput is not null)
        {
            input = rawInput;
            return true;
        }

        input = "";
        return false;
    }

    public bool ParseStringMultiple(out string[] input)
    {
        string? rawInput = Console.ReadLine();

        if (rawInput is not null)
        {
            input = rawInput.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            return true;
        }

        input = [];
        return false;
    }
}